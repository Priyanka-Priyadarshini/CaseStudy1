using NetMQ;
using NetMQ.Sockets;
using System;
using Console=Colorful.Console;

using System.Drawing;
using System.Threading;


namespace AlertSystemServer
{
    class Server
    { 
        public static void printJson(string pID,string time,string SpO2,string pulse,string temp)
        {
            string colorSPO2 = "Green", colorPulse = "Green";
            alertFuncClass alert1 = new alertFunctionality1();

            string Spo2stat = alert1.checkSPO2Status(SpO2, ref colorSPO2);        //returns status of Spo2
            string pulseStat = alert1.checkPulseStatus(pulse, ref colorPulse);    //returns status of Pulse

            string patientRecord = "{'PatientId':" + pID + ", 'TimeStamp':" + time + ", 'SPO2':" + SpO2 + ", 'Pulse_rate':" + pulse + ", 'Temperature':" + temp + ", 'Status':";
            Console.Write(patientRecord);
            string str2;

            if ((colorSPO2 == "Green" && colorPulse == "Green") || (colorSPO2 == "Red" && colorPulse == "Red"))
            {
                str2 = Spo2stat + " " + pulseStat;
                Console.Write(str2, Color.Green);
            }
            else
            {
                if (colorSPO2 == "Red")
                {
                    str2 = Spo2stat; Console.Write(str2, Color.Red);
                }
                else if (colorPulse == "Red")
                {
                    str2 = pulseStat; Console.Write(str2, Color.Red);
                }

            }
        }
        static void Main(string[] args)
        {
           // string colorSPO2 = "Green",colorPulse="Green";
            Console.WriteLine("SERVER...\n");
            //Server Connects..
            using (var server = new ResponseSocket())
            {
                server.Bind("tcp://localhost:8080");
                while (true)
                {
                    NetMQMessage message = server.ReceiveMultipartMessage();  //Receives patient data from client
                    printJson(message[0].ConvertToString(), message[1].ConvertToString(), message[2].ConvertToString(), message[3].ConvertToString(), message[4].ConvertToString());
                    Thread.Sleep(1000);
                    server.SendFrame("Data Read");   //Acknowledgement is sent to Client                                   
                 
                }
            }

        }
    }
    abstract class alertFuncClass
    {
        public string status;
        public abstract string checkSPO2Status(string str1,ref string color1);
        public abstract string checkPulseStatus(string str1,ref string color2);
    }
    class alertFunctionality1 : alertFuncClass
    {
        //SPO2 status check functionality
        public override string checkSPO2Status(string str1, ref string colorSPO2)
        {
           
            int SPO2Val = Int32.Parse(str1);// converting String to intger for SPO2
            //Status Checking for SPO2
            if (SPO2Val > 95)
            { status = "Normal healthy individual"; colorSPO2 = "Green"; }
            else if (SPO2Val >= 91 && SPO2Val <= 95)
            {  status = "Clinically acceptable, but low. Patient may be a smoker, or be unhealthy.";
                colorSPO2 = "Green";
            }
            else if (SPO2Val >= 70 && SPO2Val <= 90)
            {
                status = "Hypoxemia. Unhealthy and unsafe level.";
                colorSPO2 = "Red";
            }

            else
            {
                status = "Extreme lack of oxygen, ischemic diseases may occur.";
                colorSPO2 = "Red";
            }

            return status;

        }
        //Pulse_rate Functionality
        public override string checkPulseStatus(string str1,ref string colorPulse)
        {
            int PulseRate = Int32.Parse(str1);    // converting string to integer for pulse rate
            //string alertMessage;
            //Alert Condition Checking for PulseRate
            if (PulseRate < 40)
            {
                status = "Below healthy resting heart rates.";
                colorPulse = "Red";
            }
            else if (PulseRate >= 40 && PulseRate < 60)
            {
                status = "Resting heart rate for sleeping.";
                colorPulse = "Green"; }
            else if (PulseRate >= 60 && PulseRate < 100)
                {
                status = "Healthy adult resting heartrate.";
                colorPulse = "Green";
            }
            else if (PulseRate >= 100 && PulseRate < 220)
                    {
                status = "Acceptable if measured during exercise. Not acceptable if resting heartrate.";
                colorPulse = "Green";
            }
            else
            {
                status = "Abnormally high heart rate.";
                colorPulse = "Red";
            }

            return status;
        }
    }
}
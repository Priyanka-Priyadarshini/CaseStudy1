using NetMQ;
using NetMQ.Sockets;
using System;
using Console=Colorful.Console;
using System.Drawing;
using System.Threading;


namespace AlertSystemServer
{
    public class NurseStation
    {
        ResponseSocket socket;
        public NurseStation()
        {
            socket = new ResponseSocket();
            socket.Bind("tcp://localhost:8080");
        }
        
        public static void PrintJson(string pID,string time,string SPO2,string pulse,string temp)
        {
            string colorSPO2 = "Green", colorPulse = "Green",colorTemp="Green";
            AlertBase alert1 = new RuledBaseAlert();

            string Spo2stat = alert1.CheckSPO2Status(SPO2, ref colorSPO2);        //returns status of Spo2
            string pulseStat = alert1.CheckPulseStatus(pulse, ref colorPulse);    //returns status of Pulse
            string tempStat = alert1.CheckTempStatus(temp, ref colorTemp);        //returns status of Temp
            string patientRecord = "{'PatientId':" + pID + ", 'TimeStamp':" + time + ", 'SPO2':" + SPO2 + ", 'Pulse_rate':" + pulse + ", 'Temperature':" + temp + ", 'Status':";
            Console.Write(patientRecord);
            //string str2;

            if (colorSPO2 == "Green" && colorPulse == "Green" && colorTemp=="Green")
            {
                //str2 = Spo2stat + " " + ;
                Console.Write(Spo2stat, Color.Green);
            }
            else
            {
                if (colorSPO2 == "Red")
                    Console.Write(Spo2stat, Color.Red);
                if(colorPulse=="Red")
                    Console.Write(pulseStat, Color.Red);
                if (colorTemp == "Red")
                    Console.Write(tempStat, Color.Red);

            }
            Console.Write("}");
        }
        static void Main(string[] args)
        {
           
            Console.WriteLine("SERVER STARTS...\n");
            //Server Connects..
            NurseStation s1 = new NurseStation();
            try
            {
                while (true)
                {
                    NetMQMessage message = s1.socket.ReceiveMultipartMessage();  //Receives patient data from client
                    PrintJson(message[0].ConvertToString(), message[1].ConvertToString(), message[2].ConvertToString(), message[3].ConvertToString(), message[4].ConvertToString());
                    System.Console.WriteLine("\n");
                    Thread.Sleep(1000);
                    s1.socket.SendFrame("Data Read");   //Acknowledgement is sent to Client  
                }
            }
            finally
            {
                if (s1.socket != null)
                    ((IDisposable)s1.socket).Dispose();
            }
        }
    }
   public abstract class AlertBase
    {
        public string status;
        public abstract string CheckSPO2Status(string SPO2Val1,ref string color1);
        public abstract string CheckPulseStatus(string str1,ref string color2);
        public abstract string CheckTempStatus(string tempVal, ref string color3);
    }
    public class RuledBaseAlert : AlertBase
    {
        //SPO2 status check functionality
        public override string CheckSPO2Status(string SPO2Val1, ref string colorSPO2)
        {
           
           int SPO2Val = Int32.Parse(SPO2Val1);// converting String to intger for SPO2
            //Status Checking for SPO2
            if (SPO2Val > 95&& SPO2Val<=100)
            {
                status = "Normal healthy individual";
                colorSPO2 = "Green";
            }
            else if (SPO2Val >= 91 && SPO2Val <= 95)
            {
                status = "Clinically acceptable, but low. Patient may be a smoker, or be unhealthy.";
                colorSPO2 = "Green";
            }
            else if (SPO2Val >= 70 && SPO2Val <= 90)
            {
                status = "Hypoxemia. Unhealthy and unsafe level.";
                colorSPO2 = "Red";
            }
            else 
            {
                status = "Invalid SPO2 Value";
                colorSPO2 = "Red";
            }
            return status;

        }
        //Pulse_rate Functionality
        public override string CheckPulseStatus(string pulseRate1,ref string colorPulse)
        {
            int pulseRate = Int32.Parse(pulseRate1);    // converting string to integer for pulse rate
           
            //Alert Condition Checking for PulseRate
            if (pulseRate>=30 && pulseRate < 40)
            {
                status = "Below healthy resting heart rates.";
                colorPulse = "Red";
            }
            else if (pulseRate >= 40 && pulseRate < 60)
            {
                status = "Resting heart rate for sleeping.";
                colorPulse = "Green";
            }
            else if (pulseRate >= 60 && pulseRate < 100)
            {
                status = "Healthy adult resting heartrate.";
                colorPulse = "Green";
            }
            else if (pulseRate >= 100 && pulseRate < 220)
            {
                status = "Acceptable if measured during exercise. Not acceptable if resting heartrate.";
                colorPulse = "Green";
            }
            else if(pulseRate>=220 && pulseRate<=224)
            {
                status = "Abnormally high heart rate.";
                colorPulse = "Red";
            }
            else
            {
                status = "Invalid PulseRate";
                colorPulse = "Red";
            }

            return status;
        }

        public override string CheckTempStatus(string tempVal1, ref string colorTemp)
        {
            int tempVal = Int32.Parse(tempVal1);    // converting string to integer for pulse rate
            if(tempVal>=97 && tempVal<=99)
            {
                status = "Normal temperature.";
                colorTemp = "Green";
            }
            else if((tempVal>=93 && tempVal<97)||(tempVal >99 && tempVal < 113))
            {
                status = "Critical temperature";
                colorTemp = "Red";
            }
            else
            {
                status = "Invalid temperature value";
                colorTemp = "Red";
            }
            return status;
        }
    }
}
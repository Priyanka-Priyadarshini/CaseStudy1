using Console = Colorful.Console;
using System.Drawing;

namespace AlertSystemServer
{
    class FormattedPrint
    {
        bool criticalSPO2 = true, criticalPulse = true, criticalTemp = true;
        public void PrintJson(string pID, string time, string SPO2, string pulse, string temp)
        {
            AlertBase alert1 = new RuleBasedAlert();
            string Spo2stat = alert1.CheckSPO2Status(SPO2, ref criticalSPO2);        //returns status of Spo2
            string pulseStat = alert1.CheckPulseStatus(pulse, ref criticalPulse);    //returns status of Pulse
            string tempStat = alert1.CheckTempStatus(temp, ref criticalTemp);        //returns status of Temp
            string patientRecord = "{'PatientId':" + pID + ", 'TimeStamp':" + time + ", 'SPO2':" + SPO2 + ", 'Pulse_rate':" + pulse + ", 'Temperature':" + temp + ", 'Status':";
            Console.Write(patientRecord);
            
            if (criticalSPO2==false && criticalPulse ==false && criticalTemp ==false)
            {
                Console.Write(Spo2stat, Color.Green);
            }
            else
            {
                if (criticalSPO2)
                    Console.Write(Spo2stat, Color.Red);
                if (criticalPulse)
                    Console.Write(pulseStat, Color.Red);
                if (criticalTemp)
                    Console.Write(tempStat, Color.Red);
            }
            Console.Write("}");
        }
    }
}

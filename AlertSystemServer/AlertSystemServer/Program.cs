using System;
using Console=Colorful.Console;

namespace AlertSystemServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SERVER STARTS...\n");
            //Server Connects..
            NurseStation station = new NurseStation();
            try
            {
                while (true)
                {

                    var patientData = station.ReceivePatientData();  //Receives patient data from client
                    FormattedPrint print = new FormattedPrint();
                    print.PrintJson(patientData[0].ConvertToString(), patientData[1].ConvertToString(), patientData[2].ConvertToString(), patientData[3].ConvertToString(), patientData[4].ConvertToString());
                    System.Console.WriteLine("\n");

                    station.SendAcknowledgement();   //Acknowledgement is sent to Client 

                }
            }
            finally
            {
                if (station != null)
                    ((IDisposable)station).Dispose();
            }
        }
    }
}
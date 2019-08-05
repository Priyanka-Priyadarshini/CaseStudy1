using System;
using System.Threading;

namespace AlertSystemClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string randSPO2 = "@", randPulse = "@", randTemp = "@";
            PatientDataRecord patientRecord = new PatientDataRecord();      //Client Connection
            try
            {
                Console.WriteLine("CLIENT STARTS...\n");
                int i = 1;
                Console.WriteLine("Press any key to stop...");
                Thread.Sleep(1500);
                while (!Console.KeyAvailable)
                {
                    //Randomly generating the patient's records
                    string randPatientId = "TRJIW" + i;
                    RandomVitals vitals = new RandomVitals();
                    vitals.VitalsFromXml();    
                    vitals.GetRandomNo(ref randSPO2, ref randPulse, ref randTemp);
                    Console.WriteLine("Sending Patient data");
                    Console.WriteLine("PatientId:{0}, SPO2:{1}, PulseRate:{2}, Temperature:{3}", randPatientId, randSPO2, randPulse, randTemp);

                    //Sending to server...
                    patientRecord.SendPatientData(randPatientId, randSPO2, randPulse, randTemp);

                     //Acknowledgement received from NurseStation
                    Console.WriteLine(patientRecord.ReceiveAcknowledgement());
                    Console.WriteLine("\n");

                    i++;
                    Thread.Sleep(10000);  //After every 10sec patient data is sent to server

                }
            }
            finally
            {
                if (patientRecord != null)
                    ((IDisposable)patientRecord).Dispose();
            }
        }
    }
}

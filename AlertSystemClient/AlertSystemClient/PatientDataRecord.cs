using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace AlertSystemClient
{
    class PatientDataRecord
    {
        RequestSocket socket;
        PatientDataRecord()
        {
            socket = new RequestSocket();
            socket.Connect("tcp://localhost:8080");
        }
        public void SendPatientData(string randPatientId,string randSPO2,string randPulse,string randTemp)
        {
            socket.SendMoreFrame(randPatientId)
                .SendMoreFrame(GetTimestamp(DateTime.Now))
                .SendMoreFrame(randSPO2)
                .SendMoreFrame(randPulse)
                .SendFrame(randTemp);


        }
        public string GetTimestamp(DateTime now)
        {
            return now.ToString("dd/MM/yyyy/HH:mm:ss");
        }
        static void Main(string[] args)
        {
             string randSPO2 = "@", randPulse = "@", randTemp = "@";
            PatientDataRecord p1 = new PatientDataRecord();      //Client Connection
            try
            {
                
                Console.WriteLine("CLIENT STARTS...\n");
                int i = 1;
                while(true)
                {
                    //Randomly generating the patient's records
                    string randPatientId = "TRJIW" + i;
                    RandomFromDict r1 = new RandomFromDict();
                    r1.GetRandomNo(ref randSPO2, ref randPulse, ref randTemp);
                    Console.WriteLine("Sending Patient data");
                    Console.WriteLine("PatientId:{0}, SPO2:{1}, PulseRate:{2}, Temperature:{3}", randPatientId, randSPO2, randPulse, randTemp);

                    //Sending to server...
                    p1.SendPatientData(randPatientId, randSPO2, randPulse, randTemp);

                    var msgReceived = p1.socket.ReceiveFrameString(); //Acknowledgement received from NurseStation
                    Console.WriteLine(msgReceived);
                    Console.WriteLine("\n");
                    Thread.Sleep(10000);
                    i++;
                }
            }
            finally
            {
                if (p1.socket != null)
                    ((IDisposable)p1.socket).Dispose();
            }


        }


    }
   public class RandomFromDict
    {
         Dictionary<string, string> dSPO2, dPulse, dTemp;

        //Function to read from XML document and store in a dictionary tag-wise
        public Dictionary<string, string> ParseXmlData(string xmlPath, string tag)
        {
            //fileName = xmlPath;
            string data = File.ReadAllText(xmlPath);
            XDocument doc = XDocument.Parse(data);     //Parsing xml doc
            Dictionary<string, string> dataDictionary = new Dictionary<string, string>();

            //storing in a dictionary...
            foreach (XElement element in doc.Descendants().Where(p => p.HasElements == false))
            {
                int keyInt = 0;
                string keyName = element.Name.LocalName;   //tagname of xml added as a key in dictionary
                if (keyName == tag)
                {
                    while (dataDictionary.ContainsKey(keyName))    //if the tagname already exists as a key;it will append it and store in dictionary
                    {
                        keyName = element.Name.LocalName + keyInt++;
                    }

                    dataDictionary.Add(keyName, element.Value);

                }

            }

            return dataDictionary;
        }
        public  void DictFromXml()       //Function that Make all Dictionaries of SPO2,Pulse rate, temperature
        {
           string pathXml = @"C:\Users\320066905\source\repos\AlertSystemClient\AlertSystemClient\bin\Debug\PatientData.xml";

            //--------Dictionary for SPO2----------------*/
            dSPO2 = ParseXmlData(pathXml, "SPO2");

            //--------Dictionary for PulseRate----------------*/
            dPulse = ParseXmlData(pathXml, "Pulse_rate_value");

            //--------Dictionary for Temperature----------------*/
            dTemp = ParseXmlData(pathXml, "Temperature_Value");

        }
        //Randomly generates values stored in a dictionary
        public  string RandomGen(Dictionary<string, string> dict)
        {
            var rnd = new Random();
            var randomEntry = dict.ElementAt(rnd.Next(0, dict.Count));
            string randomValue = randomEntry.Value;
            return randomValue;
        }
        public  void GetRandomNo(ref string randSPO2, ref string randPulse, ref string randTemp)
        {
            DictFromXml();  //getting the tag-wise dictionaries SPO2,Pulse_rate,Temperature_value
            randSPO2 = RandomGen(dSPO2);
            randPulse = RandomGen(dPulse);
            randTemp = RandomGen(dTemp);
        }

    }
}

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
    class Client
    {
        static void Main(string[] args)
        {
             string randSPO2 = "@", randPulse = "@", randTemp = "@";

                //Client Connection
                using (var client=new RequestSocket())
                {
                    client.Connect("tcp://localhost:8080");
                    Console.WriteLine("CLIENT...\n");
                    for (int i = 0; i < 20; i++)
                    {
                        //Randomly generating the patient's records
                        string randPatientId = "TRJIW" + i;
                        RandomFromDict.getRandomNo(ref randSPO2, ref randPulse, ref randTemp);
                        Console.WriteLine("Sending Patient data");
                        Console.WriteLine("PatientId:{0}, SPO2:{1}, PulseRate:{2}, Temperature:{3}", randPatientId, randSPO2, randPulse, randTemp);
                   
                        //Sending to server...
                         client.SendMoreFrame(randPatientId).SendMoreFrame(GetTimestamp(DateTime.Now)).SendMoreFrame(randSPO2).SendMoreFrame(randPulse).SendFrame(randTemp);
                        var msgReceived = client.ReceiveFrameString();
                        Console.WriteLine(msgReceived);

                        Console.WriteLine("\n");
                        Thread.Sleep(10000);
                      
                    }
                }
            
        }

        public static string GetTimestamp(DateTime now)
        {
            return now.ToString("dd/MM/yyyy/HH:mm:ss");
        }
    }
    class RandomFromDict
    {
        static Dictionary<string, string> dSPO2, dPulse, dTemp;

        //Function to read from XML document and store in a dictionary tag-wise
        public static Dictionary<string, string> parseXmlData(string xmlPath, string tag, out string fileName)
        {
            fileName = xmlPath;
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
        public static void dictFromXml()       //Function that Make all Dictionaries of SPO2,Pulse rate, temperature
        {
            string filename, pathXml = @"C:\Users\320066905\source\repos\AlertSystemClient\AlertSystemClient\bin\Debug\PatientData.xml";

            //--------Dictionary for SPO2----------------*/
            dSPO2 = parseXmlData(pathXml, "SPO2", out filename);

            //--------Dictionary for PulseRate----------------*/
            dPulse = parseXmlData(pathXml, "Pulse_rate_value", out filename);

            //--------Dictionary for Temperature----------------*/
            dTemp = parseXmlData(pathXml, "Temperature_Value", out filename);

        }


        //Randomly generates values stored in a dictionary
        public static string RandomGen(Dictionary<string, string> dict)
        {
            var rnd = new Random();
            var randomEntry = dict.ElementAt(rnd.Next(0, dict.Count));
            string randomValue = randomEntry.Value;
            return randomValue;
        }
        public static void getRandomNo(ref string randSPO2, ref string randPulse, ref string randTemp)
        {
            dictFromXml();  //getting the tag-wise dictionaries SPO2,Pulse_rate,Temperature_value
            randSPO2 = RandomGen(dSPO2);
            randPulse = RandomGen(dPulse);
            randTemp = RandomGen(dTemp);
        }

    }
}

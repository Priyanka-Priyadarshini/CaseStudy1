using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace AlertSystemClient
{
    public class RandomVitals
    {
         Dictionary<string, string> dSPO2, dPulse, dTemp;

        //Function to read from XML document and store in a dictionary tag-wise
        public Dictionary<string, string> ParseXmlData(string xmlPath, string tag)
        {
           // fileName = xmlPath;
            string data = File.ReadAllText(xmlPath);
            XDocument doc = XDocument.Parse(data);     //Parsing xml doc
            Dictionary<string, string> dataDictionary = new Dictionary<string, string>();

            //storing in a dictionary...
            foreach (XElement element in doc.Descendants().Where(p => p.HasElements == false))
            {
                int keyInt = 0;
                string keyName = element.Name.LocalName;   //tagname of xml added as a key in dict
                if (keyName == tag)
                {
                    while (dataDictionary.ContainsKey(keyName))    //if the tagname already exists as a key;it will append it n store in dict
                    {
                        keyName = element.Name.LocalName + keyInt++;
                    }

                    dataDictionary.Add(keyName,element.Value);

                }

            }

            return dataDictionary;
        }
        public void VitalsFromXml()       //Make Dictionaries
        {
            string pathXml = @"C:\Users\320066905\source\repos\HelloWorld\HelloWorld\bin\Debug\PatientData.xml";

            //--------Dictionary for SPO2----------------*/
            dSPO2 = ParseXmlData(pathXml, "SPO2");

            //--------Dictionary for PulseRate----------------*/
            dPulse = ParseXmlData(pathXml, "Pulse_rate_value");

            //--------Dictionary for Temperature----------------*/
            dTemp = ParseXmlData(pathXml, "Temperature_Value");

        }
        //Randomly generates values stored in a dictionary
        public string RandomGen(Dictionary<string, string> dict)
        {
            var rnd = new Random();
            var randomEntry = dict.ElementAt(rnd.Next(0, dict.Count));
            string randomValue = randomEntry.Value;
            return randomValue;
        }
        public void GetRandomNo(ref string randSPO2, ref string randPulse, ref string randTemp)
        {
            //DictFromXml();  //getting the tag-wise dictionaries
            randSPO2 = RandomGen(dSPO2);
            randPulse = RandomGen(dPulse);
            randTemp = RandomGen(dTemp);
        }

    }
}

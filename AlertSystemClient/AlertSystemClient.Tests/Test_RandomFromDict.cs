using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AlertSystemClient.Tests
{
    [TestFixture]
   
    public class Test_RandomFromDict
    {
        RandomFromDict r1;
        [SetUp]
        public void Init()
        {
            r1 = new RandomFromDict();
        }
        
        // test for creating dictionary form xml...
        [TestCase(@"C:\Users\320066905\source\repos\AlertSystemClient\AlertSystemClient\bin\Debug\PatientData.xml", "SPO2", ExpectedResult = 8)]
        [TestCase(@"C:\Users\320066905\source\repos\AlertSystemClient\AlertSystemClient\bin\Debug\PatientData.xml", "Pulse_rate_value", ExpectedResult = 8)]
        [TestCase(@"C:\Users\320066905\source\repos\AlertSystemClient\AlertSystemClient\bin\Debug\PatientData.xml", "Temperature_Value", ExpectedResult = 8)]
        public int Test_ParseXmlData(string pathXml,string tag)
        {
            Dictionary<string, string> dict_SPO2 = r1.ParseXmlData(pathXml, tag);
            return (dict_SPO2.Count());
        }
        [TearDown]
        public void Cleanup()
        {
            r1 = null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AlertSystemClient.Tests
{
    [TestFixture]
    public class RandomVitalsTest
    {
        RandomVitals randomVital;
        [SetUp]
        public void Init()
        {
            randomVital = new RandomVitals();
        }

        // test for creating dictionary form xml...
        [TestCase(@"C:\Users\320066905\source\repos\AlertSystemClient\AlertSystemClient\bin\Debug\PatientData.xml", "SPO2", ExpectedResult = 8)]
        [TestCase(@"C:\Users\320066905\source\repos\AlertSystemClient\AlertSystemClient\bin\Debug\PatientData.xml", "Pulse_rate_value", ExpectedResult = 8)]
        [TestCase(@"C:\Users\320066905\source\repos\AlertSystemClient\AlertSystemClient\bin\Debug\PatientData.xml", "Temperature_Value", ExpectedResult = 8)]
        public int Test_ParseXmlData(string pathXml, string tag)
        {
            Dictionary<string, string> dict_SPO2 = randomVital.ParseXmlData(pathXml, tag);
            return (dict_SPO2.Count());
        }
        [TearDown]
        public void Cleanup()
        {
            if (randomVital != null)
                ((IDisposable)randomVital).Dispose();
        }
    }
}

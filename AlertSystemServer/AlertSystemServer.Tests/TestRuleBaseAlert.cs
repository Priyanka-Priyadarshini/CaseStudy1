using NUnit.Framework;
using System;

namespace AlertSystemServer.Tests
{
    [TestFixture]
    public class TestRuleBasedAlert
    {
        AlertBase alert1;
        string status;
        [SetUp]
        public void Init()
        {
           alert1 = new RuleBasedAlert();

        }
        //test case for validating SPO2...
         [TestCase("101", true, ExpectedResult = true)]
         [TestCase("96", true, ExpectedResult =  false)]
         [TestCase("95", true, ExpectedResult = false)]
         [TestCase("91", true, ExpectedResult = false)]
         [TestCase("90", true, ExpectedResult = true)]
         [TestCase("70", true, ExpectedResult = true)]
         [TestCase("69", true, ExpectedResult = true)]
         [TestCase("0", true, ExpectedResult = true)]
         [TestCase("-1", true, ExpectedResult = true)]
          public bool TestCheckSPO2Status(string SPO2,bool color)
          {
             status = alert1.CheckSPO2Status(SPO2, ref color);
             return color;
          }

        // Test case for validating PulseRate...
        [TestCase("29", true, ExpectedResult = true)]
        [TestCase("30", true, ExpectedResult = true)]
        [TestCase("39", true, ExpectedResult = true)]
        [TestCase("40", true, ExpectedResult = false)]
        [TestCase("59", true, ExpectedResult = false)]
        [TestCase("60", true, ExpectedResult = false)]
        [TestCase("99", true, ExpectedResult = false)]
        [TestCase("100", true, ExpectedResult = false)]
        [TestCase("219", true, ExpectedResult = false)]
        [TestCase("224", true, ExpectedResult = true)]
        [TestCase("225", true, ExpectedResult = true)]
        [TestCase("-25", true, ExpectedResult = true)]
        public bool Test_CheckPulsestatus(string pulse,bool color)
        {
            status = alert1.CheckPulseStatus(pulse, ref color);
            return color;
        }

        //Test case for validating temperature...
        [TestCase("92", true, ExpectedResult = true)]
        [TestCase("93", true, ExpectedResult = true)]
        [TestCase("96", true, ExpectedResult = true)]
        [TestCase("97", true, ExpectedResult = false)]
        [TestCase("99", true, ExpectedResult = false)]
        [TestCase("100", true, ExpectedResult = true)]
        [TestCase("113", true, ExpectedResult = true)]
        [TestCase("-92", true, ExpectedResult = true)]
        public bool Test_CheckTempStatus(string temp, bool color)
        {
            status = alert1.CheckTempStatus(temp, ref color);
            return color;
        }
        [TearDown]
        public void Cleanup()
        {
            if (alert1 != null)
                ((IDisposable)alert1).Dispose();
        }
    }

}

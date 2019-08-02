using NUnit.Framework;

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
           alert1 = new RuledBaseAlert();

        }
        //test case for validating SPO2...
         [TestCase("101", "Green", ExpectedResult = "Invalid SPO2 Value")]
         [TestCase("96", "Red", ExpectedResult =  "Normal healthy individual")]
         [TestCase("95", "Green", ExpectedResult = "Clinically acceptable, but low. Patient may be a smoker, or be unhealthy.")]
         [TestCase("91", "black", ExpectedResult = "Clinically acceptable, but low. Patient may be a smoker, or be unhealthy.")]
         [TestCase("90", "Green", ExpectedResult = "Hypoxemia. Unhealthy and unsafe level.")]
         [TestCase("70", "Green", ExpectedResult = "Hypoxemia. Unhealthy and unsafe level.")]
         [TestCase("69", "Green", ExpectedResult = "Invalid SPO2 Value")]
         [TestCase("0", "White", ExpectedResult = "Invalid SPO2 Value")]
         [TestCase("-1", "Green", ExpectedResult = "Invalid SPO2 Value")]
          public string Test_CheckSPO2Status(string SPO2,ref string color)
          {
             status = alert1.CheckSPO2Status(SPO2, ref color);
             return status;
          }

        // Test case for validating PulseRate...
        [TestCase("29", "Green", ExpectedResult = "Invalid PulseRate")]
        [TestCase("30", "Green", ExpectedResult = "Below healthy resting heart rates.")]
        [TestCase("39", "Green", ExpectedResult = "Below healthy resting heart rates.")]
        [TestCase("40", "Green", ExpectedResult = "Resting heart rate for sleeping.")]
        [TestCase("59", "Green", ExpectedResult = "Resting heart rate for sleeping.")]
        [TestCase("60", "Green", ExpectedResult = "Healthy adult resting heartrate.")]
        [TestCase("99", "Green", ExpectedResult = "Healthy adult resting heartrate.")]
        [TestCase("100", "Green", ExpectedResult = "Acceptable if measured during exercise. Not acceptable if resting heartrate.")]
        [TestCase("219", "Green", ExpectedResult = "Acceptable if measured during exercise. Not acceptable if resting heartrate.")]
        [TestCase("224", "Green", ExpectedResult = "Abnormally high heart rate.")]
        [TestCase("225", "Green", ExpectedResult = "Invalid PulseRate")]
        [TestCase("-25", "Green", ExpectedResult = "Invalid PulseRate")]
        public string Test_CheckPulsestatus(string pulse,string color)
        {
            status = alert1.CheckPulseStatus(pulse, ref color);
            return status;
        }

        //Test case for validating temperature...
        [TestCase("92","green", ExpectedResult = "Invalid temperature value")]
        [TestCase("93", "green", ExpectedResult = "Critical temperature")]
        [TestCase("96", "green", ExpectedResult = "Critical temperature")]
        [TestCase("97", "green", ExpectedResult = "Normal temperature.")]
        [TestCase("99", "green", ExpectedResult = "Normal temperature.")]
        [TestCase("100", "green", ExpectedResult = "Critical temperature")]
        [TestCase("113", "green", ExpectedResult = "Invalid temperature value")]
        [TestCase("-92", "green", ExpectedResult = "Invalid temperature value")]
        public string Test_CheckTempStatus(string temp, string color)
        {
            status = alert1.CheckTempStatus(temp, ref color);
            return status;
        }
        [TearDown]
        public void Cleanup()
        {
            alert1 = null;
        }
    }

}

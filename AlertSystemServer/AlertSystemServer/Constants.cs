namespace AlertSystemServer
{
    class Constants
    {
        public int SPO2AcceptableMax = 95;
        public int SPO2NormalMax = 100;
        public string SPO2Normal= "Normal healthy individual";
        public int SPO2AcceptableMin = 91;
        public string SPO2Acceptable= "Clinically acceptable, but low. Patient may be a smoker, or be unhealthy.";
        public int SPO2HypoxemiaMax = 90;
        public int SPO2HypoxemiaMin = 70;
        public string SPO2Hypoxemia = "Hypoxemia. Unhealthy and unsafe level.";
        public string SPO2Invalid = "Invalid SPO2 Value";

        public int pulseBelowHealthyMin =30;
        public int pulseBelowHealthyMax =40;
        public string pulseBelowHealthy= "Below healthy resting heart rates.";
        public int pulseRestingMax = 60;
        public string pulseResting= "Resting heart rate for sleeping.";
        public int pulseHealthyMax =100;
        public string pulseHealthy = "Healthy adult resting heartrate.";
        public int pulseAcceptableMax = 220;
        public int pulseAbnormalMax = 224;
        public string pulseAcceptable = "Acceptable if measured during exercise. Not acceptable if resting heartrate.";
        public string pulseAbnormal = "Abnormally high heart rate.";
        public string pulseInvalid = "Invalid PulseRate";

        public int tempNormalMin =97;
        public int tempNormalMax =99;
        public int tempCriticalMin = 93;
        public int tempCriticalMax = 113;
        public string tempNormal = "Normal temperature.";
        public string tempCritical = "Critical temperature";
        public string tempInvalid = "Invalid temperature value";









    }
}

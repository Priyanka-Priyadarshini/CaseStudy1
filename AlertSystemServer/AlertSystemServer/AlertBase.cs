using System;

namespace AlertSystemServer
{
    public abstract class AlertBase
    {
        public string status;
        public int ConvertToInt(string strValue)
        {
            int value = Int32.Parse(strValue);
            return value;
        }
        public abstract string CheckSPO2Status(string SPO2Val1, ref bool critical1);
        public abstract string CheckPulseStatus(string str1, ref bool critical2);
        public abstract string CheckTempStatus(string tempVal, ref bool critical3);

    }
}

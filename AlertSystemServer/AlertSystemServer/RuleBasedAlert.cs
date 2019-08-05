namespace AlertSystemServer
{
    public class RuleBasedAlert : AlertBase
    {
        Constants constant = new Constants();
        //SPO2 status check functionality
        public override string CheckSPO2Status(string SPO2Val1, ref bool criticalSPO2)
        {
            int SPO2Val = ConvertToInt(SPO2Val1);
            
            //Status Checking for SPO2
            if (SPO2Val > constant.SPO2AcceptableMax && SPO2Val <= constant.SPO2NormalMax)
            {
                status = constant.SPO2Normal;
                criticalSPO2 = false;
            }
            else if (SPO2Val >= constant.SPO2AcceptableMin && SPO2Val <= constant.SPO2AcceptableMax)
            {
                status = constant.SPO2Acceptable;
                criticalSPO2 = false;
            }
            else if (SPO2Val >= constant.SPO2HypoxemiaMin && SPO2Val <= constant.SPO2HypoxemiaMax)
            {
                status = constant.SPO2Hypoxemia;
                criticalSPO2 = true;
            }
            else
            {
                status = constant.SPO2Invalid;
                criticalSPO2 = true;
            }
            return status;
        }
        //Pulse_rate Functionality
        public override string CheckPulseStatus(string pulseRate1, ref bool criticalPulse)
        {
            int pulseRate = ConvertToInt(pulseRate1);    // converting string to integer for pulse rate

            //Alert Condition Checking for PulseRate
            if (pulseRate >= constant.pulseBelowHealthyMin && pulseRate < constant.pulseBelowHealthyMax)
            {
                status = constant.pulseBelowHealthy;
                criticalPulse = true;
            }
            else if (pulseRate >= constant.pulseBelowHealthyMax && pulseRate < constant.pulseRestingMax)
            {
                status = constant.pulseResting;
                criticalPulse = false;
            }
            else if (pulseRate >= constant.pulseRestingMax && pulseRate < constant.pulseHealthyMax)
            {
                status = constant.pulseHealthy;
                criticalPulse = false;
            }
            else if (pulseRate >= constant.pulseHealthyMax && pulseRate < constant.pulseAcceptableMax)
            {
                status = constant.pulseAcceptable;
                criticalPulse = false;
            }
            else if (pulseRate >= constant.pulseAcceptableMax && pulseRate <= constant.pulseAbnormalMax)
            {
                status = constant.pulseAbnormal;
                criticalPulse = true;
            }
            else
            {
                status =constant.pulseInvalid;
                criticalPulse = true;
            }

            return status;
        }

        public override string CheckTempStatus(string tempVal1, ref bool criticalTemp)
        {
            int tempVal = ConvertToInt(tempVal1);    // converting string to integer for pulse rate
            if (tempVal >= constant.tempNormalMin && tempVal <= constant.tempNormalMax)
            {
                status = constant.tempNormal;
                criticalTemp = false;
            }
            else if ((tempVal >= constant.tempCriticalMin && tempVal < constant.tempNormalMin) || (tempVal > constant.tempNormalMax && tempVal < constant.tempCriticalMax))
            {
                status =constant.tempCritical;
                criticalTemp = true;
            }
            else
            {
                status = constant.tempInvalid;
                criticalTemp = true;
            }
            return status;
        }
    }
}
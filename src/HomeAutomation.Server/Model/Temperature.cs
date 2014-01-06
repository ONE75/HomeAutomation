using System;

namespace HomeAutomation.Server.Model
{
    public class Temperature
    {
        public double CurrentTemperature { get; set; }
        public DateTime SavedOn { get; set; }
    }
}
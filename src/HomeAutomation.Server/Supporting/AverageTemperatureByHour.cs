using System;

namespace HomeAutomation.Server.Supporting
{
    public class AverageTemperatureByHour
    {
        public DateTime Date { get; set; }
        public int Hour { get; set; }
        public double AverageTemperature { get; set; }
        public int Points { get; set; }

        public AverageTemperatureByHour(DateTime date, int hour, double averageTemperature, int points)
        {
            Date = date;
            Hour = hour;
            AverageTemperature = averageTemperature;
            Points = points;
        }
    }
}
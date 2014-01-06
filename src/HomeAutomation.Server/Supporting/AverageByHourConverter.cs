using System;
using System.Collections.Generic;
using System.Linq;
using HomeAutomation.Server.Model;

namespace HomeAutomation.Server.Supporting
{
    public class AverageByHourConverter
    {
        public AverageTemperatureByHour GetAverage(DateTime date, int hour, IEnumerable<Temperature> temperatures)
        {
            var hourlyList = temperatures.Where(t => t.SavedOn.Hour == hour).ToList();
            var total = hourlyList.Sum(temperature => temperature.CurrentTemperature);
            double average = hourlyList.Any() ? total/hourlyList.Count() : 0;
            return new AverageTemperatureByHour(date, hour, average, hourlyList.Count());
        }
    }
}
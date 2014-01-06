using System.Collections;
using System.Collections.Generic;
using HomeAutomation.Server.Dto;

namespace HomeAutomation.Server.Supporting
{

    public class TemperatureReadingByDay
    {
        private readonly IEnumerable<AverageTemperatureByHour> _averageTemperatureByHourList;

        public TemperatureReadingByDay(IEnumerable<AverageTemperatureByHour> averageTemperatureByHourList )
        {
            _averageTemperatureByHourList = averageTemperatureByHourList;
        }


        public IEnumerable<AverageTemperatureByHour> AverageTemperatureByHourList
        {
            get { return _averageTemperatureByHourList; }
        }
    }

    //public class AverageByDayConverter
    //{
    //     public TemperatureReadingByDay GetHourlyOverview(DateTime date, IEnumerable<Temperature> temperatures)
    //     {
    //         var sortedList = temperatures.Where(t => t.SavedOn.Date == date.Date).OrderBy(t => t.SavedOn);
    //         IList<AverageTemperatureByHour> averageList = new List<AverageTemperatureByHour>();

    //         var firstHour = sortedList.First().SavedOn.Hour;
    //         var lastHour = sortedList.Last().SavedOn.Hour;

    //         for (int i = firstHour; i <= lastHour; i++)
    //         {
    //             var hourlyList = sortedList.Where(t => t.SavedOn.Hour == i).ToList();
    //             var total = hourlyList.Sum(temperature => temperature.CurrentTemperature);
    //             var average = total/hourlyList.Count();
    //             averageList.Add(new AverageTemperatureByHour(date, i, average, hourlyList.Count));
    //         }
             
    //         return new TemperatureReadingByDay(averageList);
    //     }
    //}
}
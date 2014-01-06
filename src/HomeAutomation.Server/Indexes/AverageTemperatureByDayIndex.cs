using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HomeAutomation.Server.Dto;
using HomeAutomation.Server.Model;
using Raven.Client.Indexes;

namespace HomeAutomation.Server.Indexes
{
    public class AverageTemperatureByDayIndex : AbstractIndexCreationTask<Temperature, AverageTemperatureByDayIndex.AverageTemperatureByDayResult>
    {
        public AverageTemperatureByDayIndex()
        {
            Map = docs => from t in docs
                           select new
                           {
                               AverageTemperature = t.CurrentTemperature,
                               SavedOn = t.SavedOn.Date,
                               TempSum = t.CurrentTemperature,
                               Counter = 1
                           };

            Reduce = results => from r in results
                                group r by r.SavedOn
                                    into g
                                    select new
                                    {
                                        SavedOn = g.Key,
                                        Counter = g.Sum(x => x.Counter),
                                        TempSum = g.Sum(x => x.TempSum),
                                        AverageTemperature = g.Sum(x => x.TempSum) / g.Sum(x => x.Counter),
                                    };
        }


        public class AverageTemperatureByDayResult
        {
            public DateTime SavedOn { get; set; }
            public double AverageTemperature { get; set; }
            public int Counter { get; set; }
            public double TempSum { get; set; }
        }
    }
}
using System;
using System.Linq;
using HomeAutomation.Server.Model;
using Raven.Client.Indexes;

namespace HomeAutomation.Server.Indexes
{
    public class TotalTemperatureByDayIndex : AbstractIndexCreationTask<Temperature, TotalTemperatureByDayResult>
    {
        public TotalTemperatureByDayIndex()
        {
            Map = docs => from t in docs
                          select new
                          {
                              Total = t.CurrentTemperature,
                              SavedOn = t.SavedOn.Date,
                              Counter = 1
                          };

            Reduce = results => from r in results
                                group r by r.SavedOn
                                    into g
                                    select new
                                    {
                                        SavedOn = g.Key,
                                        Counter = g.Sum(x => x.Counter),
                                        Total = g.Sum(x => x.Total),
                                    };
        }
    }

    public class TotalTemperatureByDayResult
    {
        public DateTime SavedOn { get; set; }
        public double Total { get; set; }
        public int Counter { get; set; }
        public double Average { get { return Total/Counter; } }
    }
}
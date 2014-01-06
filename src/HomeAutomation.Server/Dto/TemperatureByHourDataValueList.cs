using System.Collections;
using System.Collections.Generic;
using HomeAutomation.Server.Dto.GoogleCharts;

namespace HomeAutomation.Server.Dto
{
    public class TemperatureByHourDataValueList : List<DataValue>
    {
        public TemperatureByHourDataValueList(int hour, double temperature, int points)
        {
            this.Add(new DataValue(hour));
            this.Add(new DataValue(temperature));
        }
    }
}
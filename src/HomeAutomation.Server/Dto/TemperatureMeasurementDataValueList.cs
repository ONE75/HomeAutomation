using System.Collections;
using System.Collections.Generic;
using HomeAutomation.Server.Dto.GoogleCharts;

namespace HomeAutomation.Server.Dto
{
    public class TemperatureMeasurementDataValueList : List<DataValue>
    {
        public TemperatureMeasurementDataValueList(string timeStamp, double temperature)
        {
            this.Add(new DataValue(timeStamp));
            this.Add(new DataValue(temperature));
        }

        public static IEnumerable GetDataTableColumns()
        {
            var list = new List<DataTableColumn>();
            list.Add(new DataTableColumn { label = "Timestamp", type = "string" });
            list.Add(new DataTableColumn { label = "Temperature", type = "number" });
            return list;
        }
    }
}
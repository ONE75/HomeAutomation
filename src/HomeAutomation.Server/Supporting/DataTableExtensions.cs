using System.Collections;
using System.Collections.Generic;
using HomeAutomation.Server.Dto;
using HomeAutomation.Server.Dto.GoogleCharts;
using HomeAutomation.Server.Model;

namespace HomeAutomation.Server.Supporting
{
    public static class DataTableExtensions
    {
        public static GoogleDataTable ToDataTable(this IEnumerable<Temperature> temperatureList)
        {
            var rows = new List<DataTableRow>();

            foreach (var temperature in temperatureList)
            {
                var dtr = new DataTableRow
                              {
                                  c = new TemperatureMeasurementDataValueList(temperature.SavedOn.ToString("HH:mm"), temperature.CurrentTemperature)
                              };
                rows.Add(dtr);
            }

            return new GoogleDataTable
                       {
                           rows = rows,
                           cols = TemperatureMeasurementDataValueList.GetDataTableColumns()
                       };
        }

        public static GoogleDataTable ToDataTable(this TemperatureReadingByDay readings)
        {
            var rows = new List<DataTableRow>();

            foreach (var hour in readings.AverageTemperatureByHourList)
            {
                rows.Add(new DataTableRow { c = new TemperatureByHourDataValueList(hour.Hour, hour.AverageTemperature, hour.Points) });
            }

            var table = new GoogleDataTable()
                            {
                                cols = GetColumns(),
                                rows = rows
                            };
            return table;
        }

        private static IEnumerable GetColumns()
        {
            var list = new List<DataTableColumn>();
            list.Add(new DataTableColumn { label = "Hour", type = "number" });
            list.Add(new DataTableColumn { label = "FloorTemp", type = "number" });
            return list;

        }
    }
}
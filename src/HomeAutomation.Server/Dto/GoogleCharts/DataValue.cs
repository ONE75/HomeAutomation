namespace HomeAutomation.Server.Dto.GoogleCharts
{
    public class DataValue
    {
        public object v { get; set; }

        public DataValue(object value)
        {
            v = value;
        }
    }
}
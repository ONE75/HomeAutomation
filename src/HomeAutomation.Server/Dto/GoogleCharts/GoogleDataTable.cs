using System.Collections;

namespace HomeAutomation.Server.Dto.GoogleCharts
{
    /// <summary>
    /// DataTable format needed for the Google Charts. 
    /// See https://developers.google.com/chart/interactive/docs/reference#DataTable for more info
    /// </summary>
    public class GoogleDataTable
    {
        public IEnumerable cols { get; set; }
        public IEnumerable rows { get; set; }
        public object p { get; set; }
    }
}
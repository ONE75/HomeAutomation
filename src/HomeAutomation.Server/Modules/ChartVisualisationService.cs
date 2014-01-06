using Nancy;

namespace HomeAutomation.Server.Modules
{
    public class ChartVisualisationService : NancyModule
    {
        public ChartVisualisationService()
        {
            Get["/Charts/Temperature"] = _ =>
                                             {
                                                 return View["LastHour"];
                                             };
        }
    }
}
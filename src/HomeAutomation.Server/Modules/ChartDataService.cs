using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HomeAutomation.Server.Dto;
using HomeAutomation.Server.Model;
using HomeAutomation.Server.Supporting;
using Nancy;
using Raven.Client;

namespace HomeAutomation.Server.Modules
{
    public class ChartDataService : NancyModule
    {
        public ChartDataService()
        {
            Get["/ChartDataForLastHour"] = p =>
                {
                    using (var session = Raven.DocumentStore.OpenSession())
                    {
                        IEnumerable<Temperature> temperatureList = session.Query<Temperature>()
                                                     .Where(
                                                         t =>
                                                         t.SavedOn <= DateTime.Now && t.SavedOn >= DateTime.Now.AddHours(-1));
                        temperatureList = temperatureList.OrderBy(t => t.SavedOn).ToList();

                        var datatable = temperatureList.ToDataTable();
                        return Response.AsJson(datatable, HttpStatusCode.OK);
                    }
                };
        }


    }


}
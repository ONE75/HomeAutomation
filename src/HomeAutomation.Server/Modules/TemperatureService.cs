using System;
using System.Collections.Generic;
using System.Diagnostics;
using HomeAutomation.Server.Dto;
using HomeAutomation.Server.Model;
using Nancy;
using Nancy.ModelBinding;
using Raven.Client;
using Raven.Client.Document;

namespace HomeAutomation.Server.Modules
{
    public class TemperatureService : NancyModule
    {
        public TemperatureService()
        {
            Get["/Temperature"] = p => { return Response.AsJson(GetAllTemperatures()); };
  
 
            Post["/Temperature"] = p =>
                                       {
                                           Debug.WriteLine("Posted");
                                           return BuildPostResponse(p);
                                       };
        }

        private IEnumerable<Temperature> GetAllTemperatures()
        {
            using (var session = Raven.DocumentStore.OpenSession())
            {
                return session.Query<Temperature>();
            }
        }

        private Response BuildPostResponse(dynamic o)
        {
            var temp = this.Bind<Temperature>();
            var tempToPersist = new Temperature {CurrentTemperature = temp.CurrentTemperature, SavedOn = DateTime.Now};
            
            using (var session = Raven.DocumentStore.OpenSession())
            {
                session.Store(tempToPersist);
                session.SaveChanges();
            }

            Debug.WriteLine(string.Format("CurrentTemp: {0}", temp.CurrentTemperature));
            return Response.AsJson(tempToPersist, HttpStatusCode.OK);
        }
    }
}
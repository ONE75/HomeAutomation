using HomeAutomation.Server.Indexes;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace HomeAutomation.Server
{
    using Nancy;

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        // The bootstrapper enables you to reconfigure the composition of the framework,
        // by overriding the various methods and properties.
        // For more information https://github.com/NancyFx/Nancy/wiki/Bootstrapper
        protected override void ConfigureRequestContainer(Nancy.TinyIoc.TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

            var documentStore = new DocumentStore
            {
                ConnectionStringName = "RavenDB1"
            }.Initialize();

            IndexCreation.CreateIndexes(typeof(AverageTemperatureByDayIndex).Assembly, documentStore);
            Raven.DocumentStore = documentStore;
        }
    }
}
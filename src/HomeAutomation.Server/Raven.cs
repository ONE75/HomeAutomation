using Raven.Client;

namespace HomeAutomation.Server
{
    public static class Raven
    {
        public static IDocumentStore DocumentStore { get; set; }
    }
}
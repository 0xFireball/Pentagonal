using Nancy;

namespace Pentagonal.Modules
{
    public class PentagonalModule : NancyModule
    {
        public PentagonalModule() : base("/pentagonal")
        {
            Get("/info", _ => "Pentagonal Services - Community Edition");
        }
    }
}
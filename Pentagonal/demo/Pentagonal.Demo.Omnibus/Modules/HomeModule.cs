using Nancy;

namespace Pentagonal.Demo.Omnibus.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get("/", _ => "Hello, World from Pentagonal.Demo.Omnibus!");
        }
    }
}
using Nancy;

namespace Pentagonal.Demo.Omnibus.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get("/", _ => View["index"]);
            Get("/hello", _ => "Hello, World from Pentagonal.Demo.Omnibus!");
        }
    }
}
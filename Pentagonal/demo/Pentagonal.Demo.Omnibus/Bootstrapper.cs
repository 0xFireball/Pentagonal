using LiteDB;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Pentagonal.Auth;

namespace Pentagonal.Demo.Omnibus
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            // Install Pentagonal services

            PentagonalServices.Enable(pipelines, new PentagonalConfiguration());
            PentagonalAuthenticationServices.Enable(pipelines, new PentagonalAuthConfiguration
            {
                Database = new LiteDatabase("omnibus-demo.lidb")
            });
        }
    }
}
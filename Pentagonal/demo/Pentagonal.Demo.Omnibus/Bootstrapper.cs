using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace Pentagonal.Demo.Omnibus
{
    class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            // TODO: Your customization
        }
    }
}
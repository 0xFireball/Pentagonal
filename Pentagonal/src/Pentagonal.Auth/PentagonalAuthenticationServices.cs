using Nancy;
using Nancy.Bootstrapper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pentagonal.Auth
{
    public static class PentagonalAuthenticationServices
    {
        public static PentagonalAuthConfiguration Configuration { get; private set; }

        public static void Enable(IPipelines pipelines, PentagonalAuthConfiguration configuration)
        {
            // Required parameters
            if (pipelines == null) throw new ArgumentNullException(nameof(pipelines));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            // Configuration members
            if (configuration.Database == null) throw new ArgumentNullException(nameof(configuration.Database), "The configuration must provide a database instance! Make sure configuration.Database is set to a valid LiteDatabase instance.");
            if (!configuration.IsValid) throw new ArgumentException("The configuration is invalid! Ensure that all required options are specified.", nameof(configuration));
            // Save configuration
            Configuration = configuration;

            // Set up stateless auth
            pipelines.BeforeRequest.AddItemToStartOfPipeline(GetLoadAuthenticationHook);
        }

        private static async Task<Response> GetLoadAuthenticationHook(NancyContext context, CancellationToken token)
        {
            context.CurrentUser = await Configuration.ResolveUserIdentity(context);
            return null;
        }
    }
}
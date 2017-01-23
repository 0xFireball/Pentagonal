using Nancy.Bootstrapper;
using System;

namespace Pentagonal.Auth
{
    public static class PentagonalAuthenticationServices
    {
        internal static PentagonalAuthConfiguration Configuration { get; private set; }

        public static void Enable(IPipelines pipelines, PentagonalAuthConfiguration configuration)
        {
            // Required parameters
            if (pipelines == null) throw new ArgumentNullException(nameof(pipelines));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            // Configuration members
            if (configuration.Database == null) throw new ArgumentNullException(nameof(configuration.Database), "The configuration must provide a database instance! Make sure configuration.Database is set to a valid LiteDatabase instance.");
            // Save configuration
            Configuration = configuration;
        }
    }
}
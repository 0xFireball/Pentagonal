using Nancy.Bootstrapper;
using System;

namespace Pentagonal
{
    public static class PentagonalServices
    {
        public static PentagonalConfiguration Configuration { get; private set; }

        public static void Enable(IPipelines pipelines, PentagonalConfiguration configuration)
        {
            // Required parameters
            if (pipelines == null) throw new ArgumentNullException(nameof(pipelines));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            // Configuration members

            // Save configuration
            Configuration = configuration;
        }
    }
}
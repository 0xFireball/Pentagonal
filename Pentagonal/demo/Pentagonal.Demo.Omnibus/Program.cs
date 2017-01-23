using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Pentagonal.Demo.Omnibus
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var appDirectory = Directory.GetCurrentDirectory();
            var config = new ConfigurationBuilder()
                .SetBasePath(appDirectory)
                .AddCommandLine(args)
                .AddJsonFile(Path.Combine(appDirectory, "hosting.json"), true)
                .Build();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseConfiguration(config)
                .UseContentRoot(appDirectory)
                .UseStartup<Startup>()
                .Build();
            host.Run();
        }
    }
}

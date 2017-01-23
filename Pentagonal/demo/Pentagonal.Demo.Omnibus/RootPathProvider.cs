using System.IO;
using Nancy;

namespace Pentagonal.Demo.Omnibus
{
    public class CustomRootPathProvider : IRootPathProvider
    {
        public string GetRootPath() => Directory.GetCurrentDirectory();
    }
}
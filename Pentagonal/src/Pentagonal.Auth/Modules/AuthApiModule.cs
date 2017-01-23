using Nancy;

namespace Pentagonal.Auth.Modules
{
    public class AuthApiModule : NancyModule
    {
        public AuthApiModule() : base("/a")
        {
        }
    }
}
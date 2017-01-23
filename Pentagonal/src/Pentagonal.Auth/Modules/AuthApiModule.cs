using Nancy;
using Nancy.Security;
using PenguinUpload.Utilities;
using Pentagonal.Auth.Services;

namespace Pentagonal.Auth.Modules
{
    public class AuthApiModule : NancyModule
    {
        public AuthApiModule() : base("/a")
        {
            this.RequiresAuthentication();
            // Requires API key access
            this.RequiresClaims(x => x.Value == ApiClientAuthenticationService.StatelessAuthClaim.Value);

            var userManager = new WebUserManager();

            // Get user metadata
            Get("/userinfo", async _ =>
            {
                var idUsername = Context.CurrentUser.Identity.Name;
                var user = await userManager.FindUserByUsername(idUsername);
                return Response.AsJsonNet(user);
            });

            // Generate new API key
            Patch("/newkey", async _ =>
            {
                var idUsername = Context.CurrentUser.Identity.Name;
                var user = await userManager.FindUserByUsername(idUsername);
                // Update key
                await userManager.GenerateNewApiKey(user);
                return Response.AsJsonNet(user);
            });

            // Delete a user and all content
            Delete("/nuke/user", async _ =>
            {
                var idUsername = Context.CurrentUser.Identity.Name;
                var user = await userManager.FindUserByUsername(idUsername);
                // Disable user
                await userManager.SetEnabled(user, false);
                // TODO: Custom nuke sequence
                // Now nuke the user
                await userManager.RemoveUser(user.Username);
                return HttpStatusCode.OK;
            });
        }
    }
}
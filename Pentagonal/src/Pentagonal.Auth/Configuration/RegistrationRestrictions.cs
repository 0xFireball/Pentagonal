namespace Pentagonal.Auth.Configuration
{
    public class RegistrationRestrictions
    {
        public string InviteKey { get; set; } = null;
        public bool RegistrationEnabled { get; set; } = true;
    }
}
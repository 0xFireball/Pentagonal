namespace Pentagonal.Auth.Configuration
{
    public class AccountSecurityRequirements
    {
        public int MininumPasswordLength { get; set; } = 8;
        public int MaximumPasswordLength { get; set; } = 128;
        public int MinimumUsernameLength { get; set; } = 4;
        public int MaximumUsernameLength { get; set; } = 40;

    }
}
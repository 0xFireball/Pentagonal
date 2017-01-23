using Pentagonal.Infrastructure.Concurrency;

namespace Pentagonal
{
    public class PentagonalConfiguration
    {
        public UserServiceTable ServiceTable { get; } = new UserServiceTable();
    }
}
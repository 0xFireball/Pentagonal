using LiteDB;

namespace Pentagonal.Auth
{
    public class PentagonalAuthConfiguration
    {
        public LiteDatabase Database { get; set; }
    }
}
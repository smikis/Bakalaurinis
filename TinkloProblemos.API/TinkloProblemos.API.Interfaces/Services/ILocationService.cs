using TinkloProblemos.API.Contracts.Location;

namespace TinkloProblemos.API.Interfaces.Services
{
    public interface ILocationService
    {
        bool Add(WriteLocation prod);
        GetLocation Get(string userId);
    }
}
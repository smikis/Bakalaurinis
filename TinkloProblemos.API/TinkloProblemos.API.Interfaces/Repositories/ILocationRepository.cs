using TinkloProblemos.API.Contracts.Location;

namespace TinkloProblemos.API.Interfaces.Repositories
{
    public interface ILocationRepository
    {
        int Add(WriteLocation prod);
        GetLocation GetByUserId(string userId);
        int Update(UpdateLocation prod, string userId);
    }
}
using System.Threading.Tasks;
using TinkloProblemos.API.Identity.Entities;

namespace TinkloProblemos.API.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<string> GenerateTokenAsync(ApplicationUser user);
    }
}
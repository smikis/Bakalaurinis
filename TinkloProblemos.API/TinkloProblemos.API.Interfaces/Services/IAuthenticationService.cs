using System.Threading.Tasks;
using TinkloProblemos.API.Contracts.Models;
using TinkloProblemos.API.Identity.Entities;

namespace TinkloProblemos.API.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<TokenResult> GenerateTokenAsync(ApplicationUser user);
    }
}
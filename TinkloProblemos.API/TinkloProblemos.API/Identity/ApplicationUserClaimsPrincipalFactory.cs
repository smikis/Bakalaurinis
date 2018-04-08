using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;
using TinkloProblemos.API.Identity.Entities;

namespace TinkloProblemos.API.Identity
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
    {

        public ApplicationUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {

        }

        public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            ClaimsPrincipal principal = await base.CreateAsync(user);

            return principal;
        }
    }
}
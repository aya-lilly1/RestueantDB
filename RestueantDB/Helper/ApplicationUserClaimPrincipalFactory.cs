

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestueantDB.Helper
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<IdentityUser, IdentityRole>
    {
        public ApplicationUserClaimsPrincipalFactory(UserManager<IdentityUser> userManager,
                                    RoleManager<IdentityRole> roleManager,
                                    IOptions<IdentityOptions> options)
                                  :base(userManager, roleManager,options)
        {
        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(IdentityUser user)
            {
            var identity=  await base.GenerateClaimsAsync(user);
            identity.AddClaim( new Claim("Name", user.UserName ??""));
            identity.AddClaim(new Claim("IdUser", user.Id??""));
            return identity;
        }
    }
}

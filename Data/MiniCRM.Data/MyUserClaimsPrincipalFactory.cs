//namespace MiniCRM.Data
//{
//    using System.Security.Claims;
//    using System.Threading.Tasks;

//    using Microsoft.AspNetCore.Identity;
//    using Microsoft.Extensions.Options;
//    using MiniCRM.Data.Models;

//    public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
//    {
//        public MyUserClaimsPrincipalFactory(
//            UserManager<ApplicationUser> userManager,
//            IOptions<IdentityOptions> optionsAccessor)
//            : base(userManager, optionsAccessor)
//        {
//        }

//        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
//        {
//            var identity = await base.GenerateClaimsAsync(user);
//            identity.AddClaim(new Claim("CompanyId", user.CompanyId ?? "[Click to edit profile]"));
//            return identity;
//        }
//    }
//}

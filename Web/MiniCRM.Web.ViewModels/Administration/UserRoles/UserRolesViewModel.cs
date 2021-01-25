namespace MiniCRM.Web.ViewModels.Administration.UserRoles
{
    using System.Collections.Generic;

    using MiniCRM.Data.Models;
    using MiniCRM.Services.Mapping;

    public class UserRolesViewModel : IMapFrom<ApplicationUser>
    {
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}

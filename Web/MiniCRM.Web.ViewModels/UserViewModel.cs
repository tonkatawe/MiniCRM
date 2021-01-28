namespace MiniCRM.Web.ViewModels
{
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Mapping;

    public class UserViewModel : IMapFrom<ApplicationUser>
    {
        public string? ParentId { get; set; }

        public string Id { get; set; }

        public string CompanyId { get; set; }

        public string JobTitleName { get; set; }

        public string CompanyName { get; set; }

        public string FullName { get; set; }
        
        public string FirstName { get; set; }
        
        public string MiddleName { get; set; }
        
        public string LastName { get; set; }

        public string Email { get; set; }
    }
}

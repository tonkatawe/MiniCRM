namespace MiniCRM.Data.Models
{
    public class UserCompanies
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string CompanyId { get; set; }

        public Company Company { get; set; }
    }
}

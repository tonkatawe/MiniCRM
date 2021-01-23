namespace MiniCRM.Web.ViewModels.Companies
{
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Mapping;

    public class CompanyEditModel : CompanyCreateModel, IMapFrom<Company>
    {
        public string Id { get; set; }
    }
}

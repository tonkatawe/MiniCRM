namespace MiniCRM.Web.ViewModels.Owners
{
    using System.Linq;

    using AutoMapper;
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Mapping;

    public class OwnersDashBoardViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string CompanyId { get; set; }

        public int EmployeesCount { get; set; }

        public int CustomersCount { get; set; }

        public string CompanyName { get; set; }

        public int ProductsCount { get; set; }

        public int OrdersCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, OwnersDashBoardViewModel>()
                .ForMember(x => x.ProductsCount, options =>
                {
                    options.MapFrom(c => c.Company.Products.Count(p => p.CompanyId == c.CompanyId));
                });
        }
    }
}

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

        public string BestEmployerName { get; set; }

        public string BestProductName { get; set; }

        public string MostBenefitProduct { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, OwnersDashBoardViewModel>()
                .ForMember(x => x.ProductsCount, options =>
                {
                    options.MapFrom(c => c.Company.Products.Count(p => p.CompanyId == c.CompanyId));
                })
                .ForMember(o => o.OrdersCount, options =>
                {
                    options.MapFrom(o => o.Employees.SelectMany(s => s.Sales).Count());
                })
                .ForMember(x => x.MostBenefitProduct, options =>
                {
                    options.MapFrom(
                        e => e.Employees.SelectMany(s => s.Sales)
                        .OrderByDescending(sp => sp.Products.Max(b => b.Product.Price * b.Quantity))
                        .SelectMany(x => x.Products)
                        .Select(x => x.Product)
                        .FirstOrDefault().Name);

                });
            //.ForMember(x => x.BestEmployerName, options =>
            //{
            //    options.MapFrom(
            //        e => e.Employees.OrderByDescending(s => s.Sales.Max(sp => sp.Products.Max(x => x.Product.Price * x.Quantity))).Select(x => x.FirstName).FirstOrDefault());
            //});
        }
    }
}

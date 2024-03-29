﻿namespace MiniCRM.Web.ViewModels.Customer
{
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Mapping;

    public class CustomerViewModel : IMapFrom<Customer>
    {
        public int Id { get; set; }

        public string JobTitleName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string AdditionalInfo { get; set; }

        public string EmployerFullName { get; set; }

        public string AddressCity { get; set; }

        public string AddressCountry { get; set; }

        public string AddressStreet { get; set; }

        public int AddressZipCode { get; set; }

        public string OwnerId { get; set; }

        public int OrdersCount { get; set; }
    }
}

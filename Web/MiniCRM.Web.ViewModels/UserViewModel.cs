﻿using MiniCRM.Data.Models;
using MiniCRM.Services.Mapping;

namespace MiniCRM.Web.ViewModels
{
    public class UserViewModel : IMapFrom<ApplicationUser>
    {
        public string? ParentId { get; set; }
        public string Id { get; set; }

        public string CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }
    }
}

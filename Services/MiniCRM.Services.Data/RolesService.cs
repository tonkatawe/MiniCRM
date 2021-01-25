using Microsoft.EntityFrameworkCore;

namespace MiniCRM.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MiniCRM.Data.Common.Repositories;
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Data.Contracts;

    public class RolesService : IRolesService
    {
        private readonly IDeletableEntityRepository<ApplicationRole> rolesRepository;

        public RolesService(IDeletableEntityRepository<ApplicationRole> rolesRepository)
        {
            this.rolesRepository = rolesRepository;
        }

        public async Task<IEnumerable<ApplicationRole>> GetAllAsync()
        {
            return await this.rolesRepository.All().ToListAsync();
        }
    }
}

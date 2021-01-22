using System.Linq;
using Microsoft.EntityFrameworkCore;
using MiniCRM.Data.Common.Repositories;
using MiniCRM.Data.Models;

namespace MiniCRM.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using MiniCRM.Services.Data.Contracts;

    public class IndustriesService : IIndustriesService
    {
        private readonly IDeletableEntityRepository<Industry> industryRepository;

        public IndustriesService(IDeletableEntityRepository<Industry> industryRepository)
        {
            this.industryRepository = industryRepository;
        }

        public async Task<int> CreateAsync(string name)
        {
            if (!this.industryRepository.All().Select(x => x.Name).Contains(name))
            {
                var industry = new Industry
                {
                    Name = name,
                };
                await this.industryRepository.AddAsync(industry);
                return await this.industryRepository.SaveChangesAsync();
            }

            return this.industryRepository.All().FirstOrDefault(x => x.Name == name).Id;
        }
    }
}

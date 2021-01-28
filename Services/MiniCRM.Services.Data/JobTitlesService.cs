using Microsoft.EntityFrameworkCore;

namespace MiniCRM.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using MiniCRM.Data.Common.Repositories;
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Data.Contracts;

    public class JobTitlesService : IJobTitlesService
    {
        private readonly IDeletableEntityRepository<JobTitle> jobTitlesRepository;

        public JobTitlesService(IDeletableEntityRepository<JobTitle> jobTitlesRepository)
        {
            this.jobTitlesRepository = jobTitlesRepository;
        }

        public async Task<int> CreateAsync(string name)
        {
            if (!this.jobTitlesRepository.All().Select(x => x.Name).Contains(name))
            {
                var jobTitle = new JobTitle
                {
                    Name = name,
                };
                await this.jobTitlesRepository.AddAsync(jobTitle);
                return await this.jobTitlesRepository.SaveChangesAsync();
            }

            return await this.jobTitlesRepository
                .All()
                .Where(x => x.Name == name)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
        }
    }
}

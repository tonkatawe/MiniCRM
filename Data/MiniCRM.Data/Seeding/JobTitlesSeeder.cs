namespace MiniCRM.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MiniCRM.Data.Models;

    public class JobTitlesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.JobTitles.Any())
            {
                var jobTitle = new JobTitle
                {
                    Name = "General Manager",
                };
                await dbContext.JobTitles.AddAsync(jobTitle);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}

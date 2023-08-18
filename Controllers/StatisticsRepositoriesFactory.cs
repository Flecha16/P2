using System.Collections.Generic;
using System.Linq;
using AuthSystem.Models;
using AuthSystem.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AuthSystem.Data.Repositories
{
    public class StatisticsRepositoryFactory : IStatisticsRepositoryFactory
    {

        private readonly AuthDbContext _dbContext;

        public StatisticsRepositoryFactory(AuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IStatisticsRepository CreateStatisticsRepository()
        {
            return new StatisticsRepository(_dbContext);
        }
    }
}

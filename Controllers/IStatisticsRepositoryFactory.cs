using System.Collections.Generic;
using AuthSystem.Models;

namespace AuthSystem.Repositories
{
    public interface IStatisticsRepositoryFactory
    {
        IStatisticsRepository CreateStatisticsRepository();
    }
}
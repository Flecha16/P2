using System.Collections.Generic;
using AuthSystem.Models;

namespace AuthSystem.Repositories
{
    public interface IStatisticsRepository
    {
        IEnumerable<Statistic> GetAllStatistics();
        Statistic GetStatisticById(int id);
        void CreateStatistic(Statistic statistic);
        void UpdateStatistic(Statistic statistic);
        void DeleteStatistic(int id);
        IEnumerable<Team> GetAllTeams();
        IEnumerable<Player> GetAllPlayers();
        public Statistic GetStatisticByPlayerId(int playerId);
        public Player GetPlayerById(int id);
        bool StatisticExists(int id);
    }
}
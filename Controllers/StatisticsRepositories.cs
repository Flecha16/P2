using System.Collections.Generic;
using System.Linq;
using AuthSystem.Models;
using AuthSystem.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AuthSystem.Data.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly AuthDbContext _context;

        public StatisticsRepository(AuthDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Statistic> GetAllStatistics()
        {
            return _context.Statistics
                .Include(s => s.Player)
                .Include(s => s.Player.Team)
                .Include(s => s.Player.League)
                .ToList();
        }

        public Statistic GetStatisticById(int id)
        {
            return _context.Statistics
                .Include(s => s.Player)
                .Include(s => s.Player.Team)
                .Include(s => s.Player.League)
                .FirstOrDefault(s => s.Id == id);
        }

        public bool StatisticExists(int id)
        {
            return _context.Statistics.Any(e => e.Id == id);
        }

        public Player GetPlayerById(int id)
        {
            return _context.Players.FirstOrDefault(p => p.Id == id);
        }

        public Statistic GetStatisticByPlayerId(int playerId)
        {
            return _context.Statistics.FirstOrDefault(s => s.PlayerId == playerId);
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return _context.Teams.ToList();
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return _context.Players.ToList();
        }

        public void CreateStatistic(Statistic statistic)
        {
            // Código para crear una nueva estadística en la base de datos
            _context.Statistics.Add(statistic);
            _context.SaveChanges();
        }

        public void UpdateStatistic(Statistic statistic)
        {
            // Código para actualizar una estadística existente en la base de datos
            _context.Statistics.Update(statistic);
            _context.SaveChanges();
        }

        public void DeleteStatistic(int id)
        {
            // Código para eliminar una estadística de la base de datos
            var statistic = _context.Statistics.Find(id);
            if (statistic != null)
            {
                _context.Statistics.Remove(statistic);
                _context.SaveChanges();
            }
        }
    }
}

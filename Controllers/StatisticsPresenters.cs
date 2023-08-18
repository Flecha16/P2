using System.Collections.Generic;
using AuthSystem.Models;

namespace AuthSystem.Presenters
{
    public class StatisticsPresenter : IStatisticsPresenter
    {
        public Statistic PresentStatistic(Statistic statistic)
        {
            // Aquí convierte el objeto Statistic en StatisticDto
            // Puedes mapear las propiedades manualmente o utilizar una biblioteca como AutoMapper
            var statisticDto = new Statistic
            {
                Id = statistic.Id,
                Matches = statistic.Matches,
                Minutes = statistic.Minutes,
                Goals = statistic.Goals,
                Assists = statistic.Assists,
                YellowCards = statistic.YellowCards,
                RedCards = statistic.RedCards,
                AverageSpeed = statistic.AverageSpeed,
                Km = statistic.Km,
                // Mapea las demás propiedades según corresponda
            };
            return statisticDto;
        }

        public IEnumerable<Statistic> PresentStatistics(IEnumerable<Statistic> statistics)
        {
            var statisticDtos = new List<Statistic>();
            foreach (var statistic in statistics)
            {
                var statisticDto = PresentStatistic(statistic);
                statisticDtos.Add(statisticDto);
            }
            return statisticDtos;
        }

        public Statistic ConvertToStatistic(Statistic statisticDto)
        {
            // Aquí convierte el objeto StatisticDto en Statistic
            // Puedes mapear las propiedades manualmente o utilizar una biblioteca como AutoMapper
            var statistic = new Statistic
            {
                Id = statisticDto.Id,
                Matches = statisticDto.Matches,
                Minutes = statisticDto.Minutes,
                Goals = statisticDto.Goals,
                Assists = statisticDto.Assists,
                YellowCards = statisticDto.YellowCards,
                RedCards = statisticDto.RedCards,
                AverageSpeed = statisticDto.AverageSpeed,
                Km = statisticDto.Km,
                // Mapea las demás propiedades según corresponda
            };
            return statistic;
        }
    }
}
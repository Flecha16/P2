using AuthSystem.Models;

namespace AuthSystem.Presenters
{
    public interface IStatisticsPresenter
    {
        Statistic PresentStatistic(Statistic statistic);
        IEnumerable<Statistic> PresentStatistics(IEnumerable<Statistic> statistics);
        Statistic ConvertToStatistic(Statistic statisticDto);
    }
}
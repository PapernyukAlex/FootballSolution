using FootballShared.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebFootball.Controllers;

public class TopScorerStatisticsController : Controller
{
    private readonly UnitOfWork _unitOfWork;
    private readonly ILogger<HomeController> _logger;

    public TopScorerStatisticsController(ILogger<HomeController> logger, UnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index([FromQuery(Name = "competition_id")] int competitionId)
    {
        var topScorerStatistics = _unitOfWork.TopScorerStatisticsRepository.GetAllByCompetition(competitionId);
        return View(topScorerStatistics.OrderByDescending(ts => ts.Goals).ToList());
    }
}

using Microsoft.AspNetCore.Mvc;
using FootballShared.Repositories;
using FootballShared.Models;
using WebFootball.Views;
using WebFootball.Models.ViewModels;

namespace WebFootball.Controllers;

public class MatchController : Controller
{
    private readonly UnitOfWork _unitOfWork;
    private readonly ILogger<HomeController> _logger;

    public MatchController(ILogger<HomeController> logger, UnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index([FromQuery(Name = "competition_id")] int competitionId,
        [FromQuery(Name = "matchday")] int? matchDay) {
        var currentMatchday = matchDay ?? _unitOfWork.MatchRepository.GetCurrentMatchdayByCompetitionId(competitionId);
        var matches = _unitOfWork.MatchRepository.GetByCompetitionIdAndMatchdate(competitionId, currentMatchday);
        var competitions = _unitOfWork.MatchRepository.GetCompetitions();
        var matchdays = _unitOfWork.MatchRepository.GetAllMatchDays(competitionId);
        matchdays.Sort();
        return View(
                new MatchesForDayViewModel { 
                    SelectedCompetitionId = competitionId,
                    SelectedMatchDay = currentMatchday,
                    Matches = matches,
                    MatchDays = matchdays,
                    Competitions = competitions
                });
    }
}


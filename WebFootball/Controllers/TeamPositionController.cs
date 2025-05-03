using FootballShared.Enums;
using FootballShared.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebFootball.Models.ViewModels;

namespace WebFootball.Controllers;

public class TeamPositionController: Controller
{
    private readonly UnitOfWork _unitOfWork;
    private readonly ILogger<HomeController> _logger;

    public TeamPositionController(ILogger<HomeController> logger, UnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index([FromQuery(Name = "competition_id")] int competitionId,
        [FromQuery(Name = "type")] TeamPositionType type = TeamPositionType.Total)
    {
        var teamPositions = _unitOfWork.TeamPositionRepository.GetByCompetitionAndType(competitionId, type);
        var activeCompetitions= _unitOfWork.TeamPositionRepository.GetCompetitions();
        return View(new TeamPositionsWithType(competitionId, activeCompetitions, type, teamPositions));
    }
}

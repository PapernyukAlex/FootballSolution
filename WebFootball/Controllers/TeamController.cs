using FootballShared.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebFootball.Controllers;
public class TeamController : Controller
{
    private readonly UnitOfWork _unitOfWork;
    private readonly ILogger<HomeController> _logger;

    public TeamController(ILogger<HomeController> logger, UnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index([FromQuery(Name = "team_id")] int teamId)
    {
        return View(_unitOfWork.TeamRepository.GetById(teamId));
    }
}

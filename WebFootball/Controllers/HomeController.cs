using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebFootball.Models;
using FootballShared.Repositories;
using WebFootball.Models.ViewModels;

namespace WebFootball.Controllers;

public class HomeController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var competitions = _unitOfWork.MatchRepository.GetCompetitions();
            var defaultCompetitionId = competitions.First().Id;
            return View(new CompetitionsAndSelected(defaultCompetitionId, competitions));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

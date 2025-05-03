using FootballShared.Models;

namespace WebFootball.Models.ViewModels
{
    public class MatchesForDayViewModel
    {
        public int SelectedMatchDay { get; set; }
        public int SelectedCompetitionId{ get; set; }   
        public List<int> MatchDays { get; set; } = new();
        public List<Competition> Competitions { get; set; } = new();
        public List<Match> Matches { get; set; } = new();
    }
}

using FootballShared.Models;

namespace WebFootball.Models.ViewModels
{
    public class CompetitionsAndSelected(int competitionId, List<Competition> competitions)
    {
        public int SelectedCompetitionId { get; } = competitionId;
        public List<Competition> Competitions { get; } = competitions;
    }
}

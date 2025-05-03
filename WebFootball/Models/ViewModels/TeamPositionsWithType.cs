using FootballShared.Enums;
using FootballShared.Models;

namespace WebFootball.Models.ViewModels
{
    public class TeamPositionsWithType(int competitionId, List<Competition> competitions, 
        TeamPositionType type, List<TeamPosition> teamPositions)
    {
        public List<TeamPosition> TeamPositions { get; } = teamPositions;
        public int SelectedCompetitionId { get; } = competitionId;
        public List<Competition> Competitions { get; } = competitions;

        public TeamPositionType SelectedType { get; } = type;

        public List<TeamPositionType> AllTypes { get; } = Enum.GetValues(typeof(TeamPositionType)).Cast<TeamPositionType>().ToList();
    }
}

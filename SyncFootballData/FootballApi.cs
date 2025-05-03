using Microsoft.Extensions.Logging;
using System.Collections.Specialized;
using System.Configuration;
namespace FootballExtractor
{
    internal class FootballApi
    {
        
        private static readonly NameValueCollection ApiConfig = (NameValueCollection)ConfigurationManager.GetSection("ApiSettings");
        private static readonly String BaseUrl = ApiConfig["BaseUrl"]!;
        private static readonly String AuthToken = ApiConfig["AuthToken"]!;
        private readonly HttpClient client = new HttpClient(); // Reused instance

        private ILogger<FootballApi> Logger { get; }

        public FootballApi(ILogger<FootballApi> logger) {
            client.DefaultRequestHeaders.Add("X-Auth-Token", AuthToken);
            Logger = logger;
        }

        internal String GetAllTeams(int competitionId, int season)
        {
            return GetRequest($"competitions/{competitionId}/teams/?season={season}");
        }

        internal String GetTeamInfo(int teamId)
        {
            return GetRequest($"teams/{teamId}");
        }

        internal String GetMatches(int competitionId, int teamId, int season)
        {
            return GetRequest($"teams/{teamId}/matches/?season={season}&competitions={competitionId}");
        }

        internal String GetTopGoalStatistics(int competitionId, int season, int limit)
        {
            return GetRequest($"competitions/{competitionId}/scorers?season={season}&limit={limit}");
        }

        internal String GetStandings(int competitionId, int season)
        {
            return GetRequest($"competitions/{competitionId}/standings?season={season}");
        }

        String GetRequest(string additionalUrl)
        {
            string url = BaseUrl + additionalUrl;

            int maxRetries = 3;
            int delay = 62000;
            int attempt = 0;

            HttpResponseMessage responseMessage;

            while (true)
            {
                responseMessage = client.GetAsync(url).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    break;
                }
                else if (responseMessage.StatusCode != System.Net.HttpStatusCode.TooManyRequests 
                    || attempt >= maxRetries)
                {
                    throw new HttpRequestException($"Request failed with status code {responseMessage.StatusCode}");
                } else {
                    attempt++;
                    Logger.LogWarning($"Attempt {attempt}: Too many requests. Retrying in {delay / 1000} seconds...");

                    Thread.Sleep(delay);
                }
            }

            string responseBody = responseMessage!.Content.ReadAsStringAsync().Result;
            return responseBody;
        }

    }
}

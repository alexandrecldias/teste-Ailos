using Newtonsoft.Json;
using Questao2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questao2.Service
{
    public class FootballMatchService
    {
        private readonly HttpClient _client;

        public FootballMatchService()
        {
            _client = new HttpClient();
        }

        public async Task<int> GetTotalScoredGoals(string team, int year)
        {
            int totalGoals = 0;
            totalGoals += await GetGoalsByTeamAndYear(team, year, "team1");
            totalGoals += await GetGoalsByTeamAndYear(team, year, "team2");
            return totalGoals;
        }

        private async Task<int> GetGoalsByTeamAndYear(string team, int year, string teamParam)
        {
            int totalGoals = 0;
            int currentPage = 1;
            bool hasMorePages = true;

            while (hasMorePages)
            {
                string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&{teamParam}={team}&page={currentPage}";
                HttpResponseMessage response = await _client.GetAsync(url);
                string responseData = await response.Content.ReadAsStringAsync();

                var apiResponse = JsonConvert.DeserializeObject<FootballMatchApiResponse>(responseData);
                foreach (var match in apiResponse.Data)
                {
                    totalGoals += (teamParam == "team1") ? int.Parse(match.Team1Goals) : int.Parse(match.Team2Goals);
                }

                hasMorePages = currentPage < apiResponse.TotalPages;
                currentPage++;
            }

            return totalGoals;
        }
    }
}

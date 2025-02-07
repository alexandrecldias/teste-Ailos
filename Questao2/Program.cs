using Newtonsoft.Json;
using Questao2.Service;

public class Program
{
    public static async Task Main()
    {
        var service = new FootballMatchService();

        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = await service.GetTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = await service.GetTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        
        return 0;
    }

}
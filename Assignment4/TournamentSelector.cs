namespace Assignment4;

internal static class TournamentSelector
{
    public static List<Individual> Select(List<Individual> population, int k, int numParents)
    {
        var selectedParents = new List<Individual>();
        var random = new Random();

        for (int i = 0; i < numParents; i++)
        {
            var tournament = new List<Individual>();

            for (int j = 0; j < k; j++)
            {
                int randomIndex = random.Next(population.Count);
                tournament.Add(population[randomIndex]);
            }

            Individual winner = tournament.OrderByDescending(ind => ind.Fitness).First();
            selectedParents.Add(winner);
        }

        return selectedParents;
    }
}

namespace Assignment4;

internal static class RouletteWheelSelector
{
    public static List<Individual> Select(List<Individual> population, int numParents)
    {
        List<Individual> selectedParents = new List<Individual>();
        Random random = new Random();

        double totalFitness = population.Sum(ind => ind.Fitness);

        for (int i = 0; i < numParents; i++)
        {
            double randValue = random.NextDouble() * totalFitness;
            double cumulativeFitness = 0;

            foreach (Individual individual in population)
            {
                cumulativeFitness += individual.Fitness;

                if (cumulativeFitness >= randValue)
                {
                    selectedParents.Add(individual);
                    break;
                }
            }
        }

        return selectedParents;
    }
}

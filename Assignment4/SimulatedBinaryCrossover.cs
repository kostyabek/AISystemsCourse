namespace Assignment4;

internal static class SimulatedBinaryCrossover
{
    public static List<Entity> Run(Entity parent1, Entity parent2, double eta, double crossoverProbability)
    {
        if (parent1.Genes.Length != parent2.Genes.Length)
            throw new ArgumentException("Parents must have the same number of genes.");

        List<Entity> offspring = new List<Entity>();
        Random random = new Random();

        double[] child1 = new double[parent1.Genes.Length];
        double[] child2 = new double[parent2.Genes.Length];

        for (int i = 0; i < parent1.Genes.Length; i++)
        {
            double rand = random.NextDouble();

            double beta = rand <= 0.5
                ? Math.Pow(2.0 * rand, 1.0 / (eta + 1.0))
                : Math.Pow(1.0 / (2.0 - 2.0 * rand), 1.0 / (eta + 1.0));

            child1[i] = 0.5 * ((1.0 + beta) * parent1.Genes[i] + (1.0 - beta) * parent2.Genes[i]);
            child2[i] = 0.5 * ((1.0 - beta) * parent1.Genes[i] + (1.0 + beta) * parent2.Genes[i]);

            if (random.NextDouble() <= crossoverProbability)
            {
                double temp = child1[i];
                child1[i] = child2[i];
                child2[i] = temp;
            }
        }

        offspring.Add(new Entity { Genes = child1 });
        offspring.Add(new Entity { Genes = child2 });

        return offspring;
    }
}

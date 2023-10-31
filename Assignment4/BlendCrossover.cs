namespace Assignment4;

internal static class BlendCrossover
{
    public static List<Entity> Run(Entity parent1, Entity parent2, double alpha)
    {
        if (parent1.Genes.Length != parent2.Genes.Length)
        {
            throw new ArgumentException("Parents must have the same number of genes.");
        }

        var offspring = new List<Entity>();
        var random = new Random();

        double[] child1 = new double[parent1.Genes.Length];
        double[] child2 = new double[parent2.Genes.Length];

        for (int i = 0; i < parent1.Genes.Length; i++)
        {
            double minGene = Math.Min(parent1.Genes[i], parent2.Genes[i]);
            double maxGene = Math.Max(parent1.Genes[i], parent2.Genes[i]);

            double range = maxGene - minGene;

            double minChild1 = minGene - range * alpha;
            double maxChild1 = maxGene + range * alpha;

            child1[i] = random.NextDouble() * (maxChild1 - minChild1) + minChild1;
            child2[i] = random.NextDouble() * (maxChild1 - minChild1) + minChild1;
        }

        offspring.Add(new Entity { Genes = child1 });
        offspring.Add(new Entity { Genes = child2 });

        return offspring;
    }
}

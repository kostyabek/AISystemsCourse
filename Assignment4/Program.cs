using Assignment4;

internal class Program
{
    private static readonly Random random = new();
    private const int PopulationSize = 100;
    private const double MutationRate = 0.1;
    private const int Generations = 1000;
    private const double MinValue = -10.0;
    private const double MaxValue = 10.0;

    private static double FitnessFunction(double x, double y)
    {
        return x * x + y * y;
    }

    private static double[] CreateRandomIndividual()
    {
        var x = random.NextDouble() * (MaxValue - MinValue) + MinValue;
        var y = random.NextDouble() * (MaxValue - MinValue) + MinValue;

        return new double[] { x, y };
    }

    private static void Main(string[] args)
    {
        var parent1 = new Entity { Genes = new double[] { 1.0, 2.0, 3.0, 4.0 } };
        var parent2 = new Entity { Genes = new double[] { 5.0, 6.0, 7.0, 8.0 } };

        Console.WriteLine("Blend Crossover");
        double alpha = 0.5;

        List<Entity> offspring = BlendCrossover.Run(parent1, parent2, alpha);

        Console.WriteLine("Parent 1 Genes: " + string.Join(", ", parent1.Genes));
        Console.WriteLine("Parent 2 Genes: " + string.Join(", ", parent2.Genes));
        Console.WriteLine("Child 1 Genes: " + string.Join(", ", offspring[0].Genes));
        Console.WriteLine("Child 2 Genes: " + string.Join(", ", offspring[1].Genes));

        Console.WriteLine();

        Console.WriteLine("Simulated Binary Crossover");
        double eta = 5.0;
        double crossoverProbability = 0.9;

        offspring = SimulatedBinaryCrossover.Run(parent1, parent2, eta, crossoverProbability);

        Console.WriteLine("Parent 1 Genes: " + string.Join(", ", parent1.Genes));
        Console.WriteLine("Parent 2 Genes: " + string.Join(", ", parent2.Genes));
        Console.WriteLine("Child 1 Genes: " + string.Join(", ", offspring[0].Genes));
        Console.WriteLine("Child 2 Genes: " + string.Join(", ", offspring[1].Genes));

        //var population = new List<double[]>(PopulationSize);

        //for (var i = 0; i < PopulationSize; i++)
        //{
        //    population.Add(CreateRandomIndividual());
        //}

        //for (var generation = 0; generation < Generations; generation++)
        //{
        //    // Evaluate the fitness of each individual
        //    var fitnessScores = population.Select(individual => FitnessFunction(individual[0], individual[1])).ToList();

        //    // Select the best individuals to be parents for the next generation
        //    var parents = population
        //        .OrderBy(individual => FitnessFunction(individual[0], individual[1]))
        //        .Take(PopulationSize / 2)
        //        .ToList();

        //    // Create a new population through crossover and mutation
        //    var newPopulation = new List<double[]>();

        //    while (newPopulation.Count < PopulationSize)
        //    {
        //        var parent1 = parents[random.Next(parents.Count)];
        //        var parent2 = parents[random.Next(parents.Count)];
        //        var child = Crossover(parent1, parent2);
        //        child = Mutate(child);
        //        newPopulation.Add(child);
        //    }

        //    population = newPopulation;
        //}

        //// Find the best solution in the final population
        //var bestSolution = population
        //    .OrderBy(individual => FitnessFunction(individual[0], individual[1]))
        //    .First();

        //var minimumValue = FitnessFunction(bestSolution[0], bestSolution[1]);

        //Console.WriteLine($"Minimum value found: {minimumValue}");
        //Console.WriteLine($"Location (x, y): ({bestSolution[0]}, {bestSolution[1]})");
    }

    private static double[] Crossover(double[] parent1, double[] parent2)
    {
        // Simple averaging crossover
        var x = (parent1[0] + parent2[0]) / 2;
        var y = (parent1[1] + parent2[1]) / 2;

        return new double[] { x, y };
    }

    private static double[] Mutate(double[] individual)
    {
        if (random.NextDouble() < MutationRate)
        {
            var x = individual[0] + (random.NextDouble() * 2 - 1); // Small random change
            var y = individual[1] + (random.NextDouble() * 2 - 1); // Small random change
            return new double[] { x, y };
        }

        return individual;
    }
}

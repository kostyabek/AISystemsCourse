namespace Assignment4;

internal class Individual
{
    public Guid Id { get; } = Guid.NewGuid();
    public double Fitness { get; set; }

    public override string ToString()
    {
        return $"{Id}, {Fitness}";
    }
}

namespace Assignment2;

internal record Node(JugsState State)
{
    public List<Node> Children { get; set; } = new List<Node>();
}

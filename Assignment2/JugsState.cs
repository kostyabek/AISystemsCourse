namespace Assignment2;

public record JugsState(int J1, int J2)
{
    public override string ToString()
    {
        return $"[{J1}, {J2}]";
    }
}

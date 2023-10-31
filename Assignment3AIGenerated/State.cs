namespace Assignment3AIGenerated;

public class State
{
    public int MissionariesLeft { get; }
    public int CannibalsLeft { get; }
    public int Boat { get; }
    public int MissionariesRight { get; }
    public int CannibalsRight { get; }

    public State(int missionariesLeft, int cannibalsLeft, int boat, int missionariesRight, int cannibalsRight)
    {
        MissionariesLeft = missionariesLeft;
        CannibalsLeft = cannibalsLeft;
        Boat = boat;
        MissionariesRight = missionariesRight;
        CannibalsRight = cannibalsRight;
    }

    public bool IsValidState()
        => MissionariesLeft >= 0 &&
            CannibalsLeft >= 0 &&
            MissionariesRight >= 0 &&
            CannibalsRight >= 0 &&
            (MissionariesLeft == 0 || MissionariesLeft >= CannibalsLeft) &&
            (MissionariesRight == 0 || MissionariesRight >= CannibalsRight);

    public bool IsGoalState()
        => MissionariesLeft == 0 && CannibalsLeft == 0;

    public override bool Equals(object? obj)
        => obj is State other &&
            MissionariesLeft == other.MissionariesLeft &&
            CannibalsLeft == other.CannibalsLeft &&
            Boat == other.Boat &&
            MissionariesRight == other.MissionariesRight &&
            CannibalsRight == other.CannibalsRight;

    public override int GetHashCode()
        => Tuple.Create(MissionariesLeft, CannibalsLeft, Boat, MissionariesRight, CannibalsRight).GetHashCode();
}

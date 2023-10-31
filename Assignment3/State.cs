namespace Assignment3;

internal record State(
    int MissionariesOnLeft,
    int CannibalsOnLeft,
    BoatState Boat,
    int MissionariesOnRight,
    int CannibalsOnRight)
{
    public override string ToString()
    {
        return $"[M: {MissionariesOnLeft}, C: {CannibalsOnLeft}] [P: {Boat.Position}, M: {Boat.Missionaries}, C: {Boat.Cannibals}] [M: {MissionariesOnRight}, C: {CannibalsOnRight}]";
    }
}

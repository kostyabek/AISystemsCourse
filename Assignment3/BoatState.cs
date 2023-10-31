namespace Assignment3;

internal record BoatState(
    BoatPosition Position,
    int Missionaries,
    int Cannibals)
{
    internal bool HasFreeSeats => Missionaries + Cannibals < 2;
}

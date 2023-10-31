using System.Text;

namespace Assignment3;

public class CannibalsAndMissionariesSolver
{
    private readonly Node _rootNode;
    private readonly State _targetState;

    public CannibalsAndMissionariesSolver(int missionariesOnLeft, int cannibalsOnLeft)
    {
        var boatState = new BoatState(BoatPosition.Left, 0, 0);
        int missionariesOnRight = 0, cannibalsOnRight = 0;
        var rootState = new State(missionariesOnLeft, cannibalsOnLeft, boatState, missionariesOnRight, cannibalsOnRight);
        var childNodes = new List<Node>();

        _rootNode = new Node(rootState, childNodes);
        _targetState = new State(0, 0, new BoatState(BoatPosition.Right, 0, 0), missionariesOnLeft, cannibalsOnLeft);
    }

    public void BuildGraph()
    {
        PrintState(_rootNode.State, 1);

        var generatedStates = new HashSet<State>();
        BuildChildrenNodes(_rootNode, 2, generatedStates);
    }

    private (bool IsSolutionFound, HashSet<State?> GeneratedStates) BuildChildrenNodes(Node node, int level, HashSet<State?> generatedStates)
    {
        State parentState = node.State;
        List<Node> childNodes = node.Children;
        var childStates = new List<State?>
        {
            PutCannibalOnBoat(parentState),
            PutMissionaryOnBoat(parentState),
            RemoveCannibalFromBoat(parentState),
            RemoveMissionaryFromBoat(parentState),
            MoveBoat(parentState, BoatPosition.Left),
            MoveBoat(parentState, BoatPosition.Right)
        };

        childStates = childStates.Where(e => e is not null && generatedStates.All(s => s != e)).ToList();

        HashSet<State?> newGeneratedStates = generatedStates;
        newGeneratedStates.UnionWith(childStates);

        childStates.ForEach(e => PrintState(e, level));

        if (childStates.Any(IsWin))
        {
            return (true, newGeneratedStates);
        }

        childNodes.AddRange(childStates.Select(e => new Node(e, new List<Node>())));
        foreach (Node childNode in childNodes)
        {
            if (!IsLose(childNode.State))
            {
                (bool IsSolutionFound, HashSet<State?> GeneratedStates) tuple = BuildChildrenNodes(childNode, level + 1, newGeneratedStates);
                if (tuple.IsSolutionFound)
                {
                    break;
                }
            }
        }

        return (false, newGeneratedStates);
    }

    private static State? PutCannibalOnBoat(State state)
    {
        if (!CanPutCannibalOnBoat(state))
        {
            return null;
        }

        var newBoatState = new BoatState(
            state.Boat.Position,
            state.Boat.Missionaries,
            state.Boat.Cannibals + 1);

        return state.Boat.Position == BoatPosition.Left
            ? new State(
                state.MissionariesOnLeft,
                state.CannibalsOnLeft - 1,
                newBoatState,
                state.MissionariesOnRight,
                state.CannibalsOnRight)
            : new State(
                state.MissionariesOnLeft,
                state.CannibalsOnLeft,
                newBoatState,
                state.MissionariesOnRight,
                state.CannibalsOnRight - 1);
    }

    private static State? PutMissionaryOnBoat(State state)
    {
        if (!CanPutMissionaryOnBoat(state))
        {
            return null;
        }

        var newBoatState = new BoatState(
            state.Boat.Position,
            state.Boat.Missionaries + 1,
            state.Boat.Cannibals);

        return state.Boat.Position == BoatPosition.Left
            ? new State(
                state.MissionariesOnLeft - 1,
                state.CannibalsOnLeft,
                newBoatState,
                state.MissionariesOnRight,
                state.CannibalsOnRight)
            : new State(
                state.MissionariesOnLeft,
                state.CannibalsOnLeft,
                newBoatState,
                state.MissionariesOnRight - 1,
                state.CannibalsOnRight);
    }

    private static State? MoveBoat(State state, BoatPosition direction)
    {
        if (state.Boat.Position == direction || state.Boat.Cannibals == 0 && state.Boat.Missionaries == 0)
        {
            return null;
        }

        var newBoatState = new BoatState(direction, state.Boat.Missionaries, state.Boat.Cannibals);

        return new State(
            state.MissionariesOnLeft,
            state.CannibalsOnLeft,
            newBoatState,
            state.MissionariesOnRight,
            state.CannibalsOnRight);
    }

    private static State? RemoveCannibalFromBoat(State state)
    {
        if (!CanRemoveCannibalFromBoat(state))
        {
            return null;
        }

        var newBoatState = new BoatState(
            state.Boat.Position,
            state.Boat.Missionaries,
            state.Boat.Cannibals - 1);

        return state.Boat.Position == BoatPosition.Left
            ? new State(
                state.MissionariesOnLeft,
                state.CannibalsOnLeft + 1,
                newBoatState,
                state.MissionariesOnRight,
                state.CannibalsOnRight)
            : new State(
                state.MissionariesOnLeft,
                state.CannibalsOnLeft,
                newBoatState,
                state.MissionariesOnRight,
                state.CannibalsOnRight + 1);
    }

    private static State? RemoveMissionaryFromBoat(State state)
    {
        if (!CanRemoveMissionaryFromBoat(state))
        {
            return null;
        }

        var newBoatState = new BoatState(
            state.Boat.Position,
            state.Boat.Missionaries - 1,
            state.Boat.Cannibals);

        return state.Boat.Position == BoatPosition.Left
            ? new State(
                state.MissionariesOnLeft + 1,
                state.CannibalsOnLeft,
                newBoatState,
                state.MissionariesOnRight,
                state.CannibalsOnRight)
            : new State(
                state.MissionariesOnLeft,
                state.CannibalsOnLeft,
                newBoatState,
                state.MissionariesOnRight + 1,
                state.CannibalsOnRight);
    }

    private static bool CanPutCannibalOnBoat(State state)
    {
        return state.Boat.HasFreeSeats &&
            (state.Boat.Position == BoatPosition.Left && state.CannibalsOnLeft > 0
                || state.Boat.Position == BoatPosition.Right && state.CannibalsOnRight > 0);
    }

    private static bool CanPutMissionaryOnBoat(State state)
    {
        return state.Boat.HasFreeSeats &&
            (state.Boat.Position == BoatPosition.Left && state.MissionariesOnLeft > 0
                || state.Boat.Position == BoatPosition.Right && state.MissionariesOnRight > 0);
    }

    private static bool CanRemoveCannibalFromBoat(State state)
    {
        return state.Boat.Cannibals > 0;
    }

    private static bool CanRemoveMissionaryFromBoat(State state)
    {
        return state.Boat.Missionaries > 0;
    }

    private static bool IsLose(State state)
    {
        return state.Boat.Position == BoatPosition.Left
            ? state.CannibalsOnLeft + state.Boat.Cannibals > state.MissionariesOnLeft + state.Boat.Missionaries
            : state.CannibalsOnRight + state.Boat.Cannibals > state.MissionariesOnRight + state.Boat.Missionaries;
    }

    private bool IsWin(State state)
    {
        return state == _targetState;
    }

    private static void PrintState(State state, int level)
    {
        var stringBuilder = new StringBuilder();
        for (int i = 1; i < level; i++)
        {
            stringBuilder.Append(' ');
        }

        Console.WriteLine($"{stringBuilder}{state}");
    }
}

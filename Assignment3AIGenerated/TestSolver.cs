namespace Assignment3AIGenerated;

public class BestFirstSearch
{
    private List<State> _path;
    private List<State> _visited;
    private PriorityQueue<State, int> _frontier;

    public List<State>? Solve(State initialState)
    {
        _path = new List<State>();
        _visited = new List<State>();
        _frontier = new PriorityQueue<State, int>();

        _frontier.Enqueue(initialState, CalculatePriority(initialState));

        while (_frontier.Count > 0)
        {
            State currentState = _frontier.Dequeue();

            _path.Add(currentState);
            _visited.Add(currentState);

            if (currentState.IsGoalState())
            {
                return _path;
            }

            foreach (State nextState in GetValidNextStates(currentState))
            {
                if (!_visited.Contains(nextState) && _frontier.UnorderedItems.All(f => f.Element != nextState))
                {
                    _frontier.Enqueue(nextState, CalculatePriority(nextState));
                }
            }
        }

        return null;
    }

    private static List<State> GetValidNextStates(State currentState)
    {
        var nextStates = new List<State>();

        int[] missionaries = { 1, 2, 0 };
        int[] cannibals = { 1, 2, 0 };

        if (currentState.Boat == 0)
        {
            foreach (int m in missionaries)
            {
                foreach (int c in cannibals)
                {
                    if (m + c is >= 1 and <= 2)
                    {
                        var nextState = new State(
                            currentState.MissionariesLeft - m,
                            currentState.CannibalsLeft - c,
                            1,
                            currentState.MissionariesRight + m,
                            currentState.CannibalsRight + c);

                        if (nextState.IsValidState())
                        {
                            nextStates.Add(nextState);
                        }
                    }
                }
            }
        }
        else
        {
            foreach (int m in missionaries)
            {
                foreach (var c in cannibals)
                {
                    if (m + c is >= 1 and <= 2)
                    {
                        var nextState = new State(
                            currentState.MissionariesLeft + m,
                            currentState.CannibalsLeft + c,
                            0,
                            currentState.MissionariesRight - m,
                            currentState.CannibalsRight - c);

                        if (nextState.IsValidState())
                        {
                            nextStates.Add(nextState);
                        }
                    }
                }
            }
        }

        return nextStates;
    }

    private static int CalculatePriority(State state)
        => state.MissionariesLeft + state.CannibalsLeft;
}
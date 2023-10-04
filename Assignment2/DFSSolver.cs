namespace Assignment2;

public class DFSSolver
{
    private readonly int _n;
    private readonly int _m;
    private readonly int _k;
    private readonly int _numberOfPossibleStates;

    private readonly JugsState _initialState;
    private readonly JugsState _targetState;
    private readonly List<Node> _graph;
    private readonly List<JugsState> _searchStepsHistory;

    public DFSSolver(int n, int m, int k)
    {
        _n = n;
        _m = m;
        _k = k;
        _numberOfPossibleStates = (_n + 1) * (_m + 1);

        _initialState = new JugsState(0, 0);
        _targetState = new JugsState(_k, 0);
        _graph = new List<Node>();
        _searchStepsHistory = new List<JugsState>();

        BuildGraph();
    }

    public void SearchGraph()
    {
        if (SearchNode(_graph.First()))
        {
            PrintSteps();
        } else
        {
            Console.WriteLine("Could not find solution!");
        }
    }

    private void BuildGraph()
    {
        var createdStates = new HashSet<JugsState>
        {
            _initialState
        };

        var rootNode = new Node(_initialState);
        _graph.Add(rootNode);

        BuildNodeChildren(rootNode, createdStates);
    }

    private JugsState FillJug(JugsState previous, int jugNumber)
        => jugNumber == 1
            ? new(_n, previous.J2) 
            : new(previous.J1, _m);

    private JugsState EmptyJug(JugsState previous, int jugNumber)
        => jugNumber == 1
            ? new(0, previous.J2)
            : new(previous.J1, 0);

    private JugsState PourFromJug(JugsState previous, int jugNumber)
    {
        switch (jugNumber)
        {
            case 1:
                {
                    int emptyAmountInSecondJar = _m - previous.J2;

                    return previous.J1 <= emptyAmountInSecondJar
                        ? new(0, previous.J2 + previous.J1)
                        : new(previous.J1 - emptyAmountInSecondJar, _m);
                }
            default:
                {
                    int emptyAmountInFirstJar = _n - previous.J1;

                    return previous.J2 <= emptyAmountInFirstJar
                        ? new(previous.J1 + previous.J2, 0)
                        : new(_n, previous.J2 - emptyAmountInFirstJar);
                }
        }
    }

    private bool IsSolutionFound(JugsState state)
        => _targetState.Equals(state);

    private (bool isSolutionFound, HashSet<JugsState> createdStates) BuildNodeChildren(Node parentNode, HashSet<JugsState> createdStates)
    {
        JugsState parentState = parentNode.State;
        var childStates = new List<JugsState>();

        if (parentState.J1 != 0 && parentState.J2 != _m)
        {
            var newState = PourFromJug(parentState, 1);
            if (!createdStates.Contains(newState))
            {
                childStates.Add(newState);
            }
        }
        if (parentState.J2 != 0 && parentState.J1 != _n)
        {
            JugsState newState = PourFromJug(parentState, 2);
            if (!createdStates.Contains(newState))
            {
                childStates.Add(newState);
            }
        }
        if (parentState.J1 != _n)
        {
            JugsState newState = FillJug(parentState, 1);
            if (!createdStates.Contains(newState))
            {
                childStates.Add(newState);
            }
        }
        if (parentState.J2 != _m)
        {
            JugsState newState = FillJug(parentState, 2);
            if (!createdStates.Contains(newState))
            {
                childStates.Add(newState);
            }
        }
        if (parentState.J1 != 0)
        {
            JugsState newState = EmptyJug(parentState, 1);
            if (!createdStates.Contains(newState))
            {
                childStates.Add(newState);
            }
        }
        if (parentState.J2 != 0)
        {
            JugsState newState = EmptyJug(parentState, 2);
            if (!createdStates.Contains(newState))
            {
                childStates.Add(newState);
            }
        }

        HashSet<JugsState> newCreatedStates = createdStates;
        newCreatedStates.UnionWith(childStates);

        parentNode.Children = childStates.Select(s => new Node(s)).ToList();

        if (childStates.Any(IsSolutionFound))
        {
            return (true, createdStates);
        }

        if (createdStates.Count == _numberOfPossibleStates)
        {
            throw new ApplicationException("There is no solution to this problem!");
        }

        foreach (Node childNode in parentNode.Children)
        {
            (bool isSolutionFound, HashSet<JugsState> createdStates) tuple = BuildNodeChildren(childNode, newCreatedStates);
            if (tuple.isSolutionFound)
            {
                return (true, tuple.createdStates);
            }

            newCreatedStates.UnionWith(tuple.createdStates);
        }

        return (false, newCreatedStates);
    }

    private void PrintSteps()
        => _searchStepsHistory.ForEach(Console.WriteLine);

    private bool SearchNode(Node node)
    {
        _searchStepsHistory.Add(node.State);
        if (IsSolutionFound(node.State))
        {
            return true;
        }

        foreach (Node childNode in node.Children)
        {
            if (SearchNode(childNode))
            {
                return true;
            }
        }

        return false;
    }
}

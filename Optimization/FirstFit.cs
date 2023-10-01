
namespace Optimization;

class FirstFit
{
    private static bool _isCoveredAllElements(List<int> uSet, List<Subset> solution)
    {
        foreach (var subset in solution)
        {
            uSet.RemoveAll(elem => subset.SubSet.Contains(elem));
        }

        return !uSet.Any();
    }

    private static IEnumerable<int> _findCoveredElements(IEnumerable<int> uSet, IEnumerable<Subset> solution)
    {
        return solution.SelectMany(subset => subset.SubSet);
    }
    
    public static IEnumerable<Subset> FindFirstSolution(List<int> uSet, List<Subset> subsets)
    {
        // 1.Step - Init empty set
        var solution = new List<Subset>();
        
        // 2.Step - Cover all elements of U
        while (!_isCoveredAllElements(uSet ,solution))
        {
            var coveredElems = _findCoveredElements(uSet, solution);
            var optSet = subsets.MaxBy(x => x.SubSet.Except(coveredElems).Count());
            
            solution.Add(optSet);
        }

        return solution;
    }
}
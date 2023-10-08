
namespace Application;

public class FirstFit
{
        
    public static IEnumerable<Subset> FindFirstSolution(List<int> uSet, List<Subset> subsets)
    {
        // 1.Step - Init empty set
        var solution = new List<Subset>();
        
        // 2.Step - Cover all elements of U
        while (!_isCoveredAllElements(uSet, solution))
        {
            var covered = _findCoveredElements(uSet, solution).ToList();
            var uncovered = uSet.Except(covered);
            var optimalSubset = subsets.MaxBy(subset => subset.SubSet.Except(covered).Count());
            
            // at least one new element covered => good!
            var optSet = uncovered.Any(e => optimalSubset != null && optimalSubset.SubSet.Contains(e))
                ? optimalSubset
                : null;
            
            // otherwise => not possible cover all elements!
            if (optSet == null) break;

            solution.Add(optSet);
        }

        return solution;
    }
    
    private static bool _isCoveredAllElements(List<int> uSet, List<Subset> solution)
    {
        var totalSet = new List<int>(uSet);
        
        foreach (var subset in solution)
        {
            totalSet.RemoveAll(elem => subset.SubSet.Contains(elem));
        }

        return !totalSet.Any();
    }

    private static IEnumerable<int> _findCoveredElements(IEnumerable<int> uSet, IEnumerable<Subset> solution)
    {
        return solution.SelectMany(subset => subset.SubSet);
    }
}
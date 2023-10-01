namespace Optimization;

public class SimulatedAnnealing
{
    private static int _alpha = 1; // Weight for Cost Function: Count of chosen subsets
    private static int _beta = 0; // Weight for Cost Function: Count of uncovered elements
    private static int _gamma = 0; // Weight for Cost Function: Weight of chosen subsets

    public SimulatedAnnealing(int alpha = 1, int beta = 0, int gamma = 0)
    {
        _alpha = alpha;
        _beta = beta;
        _gamma = gamma;
    }
    
    public static int CalculateCostFunction(IEnumerable<int> uSet, List<Subset> solution)
    {
        var subsetCount = solution.Count;
        var uncoveredCount = uSet.Except(solution.Select(x => x.SubSet).SelectMany(subset => subset)).Count();
        var weightSum = solution.Sum(x => x.Weight);
        
        return _alpha * subsetCount + _beta * uncoveredCount + _gamma * weightSum;
    }
    
    public void Process(List<int> uSet, List<Subset> subsets)
    {
        var currentSolution = FirstFit.FindFirstSolution(uSet, subsets).ToList();
        var bestSolution = currentSolution;

        var cost = CalculateCostFunction(uSet, currentSolution);
        //TODO: Add Temperature function + algorithm loop
    }

    static void Main() { }
}
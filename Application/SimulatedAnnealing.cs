using System.Diagnostics;

namespace Application;

public class SimulatedAnnealing
{
    private static Temperature _temperature;
    private static int _alpha = 1; // Weight for Cost Function: Count of chosen subsets
    private static int _beta = 0; // Weight for Cost Function: Count of uncovered elements
    private static int _gamma = 0; // Weight for Cost Function: Weight of chosen subsets

    public SimulatedAnnealing(Temperature temperature, int alpha = 1, int beta = 0, int gamma = 0)
    {
        _temperature = temperature;
        _alpha = alpha;
        _beta = beta;
        _gamma = gamma;
    }

    public Result Process(List<int> uSet, List<Subset> subsets)
    {
        var watch = new Stopwatch();
        watch.Start();

        // 1.Step - Init Annealing lists
        var tempList = new List<decimal>(_temperature.TempList) { 0m };
        var stepList = _temperature.StepList;

        // 2. Step - Find First Solution
        
        var currentSol = FirstFit.FindFirstSolution(uSet, subsets).ToList();
        var bestSol = new List<Subset>(currentSol);

        var startCost = _calculateCostFun(uSet, currentSol);

        // 3. Step - Solve
        
        var k = 0;
        do
        {
            if (!subsets.Any()) break; // not possible cover any
            
            for (var n = 0; n < stepList[n]; n++)
            {
                var anotherSol = _generateNewSolution(currentSol, subsets);

                var curCost = _calculateCostFun(uSet, currentSol);
                var anotherCost = _calculateCostFun(uSet, anotherSol);
                var bestCost = _calculateCostFun(uSet, bestSol);

                if (anotherCost < bestCost)
                {
                    if(startCost < anotherCost)
                        Console.Write("why?");
                    bestSol = anotherSol;
                }

                if (anotherCost < curCost || _getRandomNumber() < _eFun(curCost, anotherCost, tempList[k]))
                {
                    currentSol = anotherSol;
                }
            }

            k += 1;
        } while (tempList[k] != 0);

        watch.Stop();

        return new Result(
            selectedSubsets: bestSol,
            startCost: startCost,
            endCost: _calculateCostFun(uSet, bestSol),
            timeProcessed: watch.ElapsedMilliseconds / 1000.0
        );
    }

    private static int _calculateCostFun(IEnumerable<int> uSet, List<Subset> solution)
    {
        var subsetCount = solution.Count;
        var uncoveredCount = uSet.Except(solution.Select(x => x.SubSet).SelectMany(subset => subset)).Count();
        var weightSum = solution.Sum(x => x.Weight);

        return _alpha * subsetCount + _beta * uncoveredCount + _gamma * weightSum;
    }

    // 25% - Add random set
    // 25% - Remove random set
    // 50% - Replace random set
    private static List<Subset> _generateNewSolution(List<Subset> currSol, List<Subset> subsets)
    {
        List<Subset> newSolution;
        
        var dice = _getRandomNumber();
        
        if (Subset.IsEqual(currSol,subsets)) newSolution = _removeRandomSet(currSol, subsets); //Selected all
        else if (!currSol.Any()) newSolution = _addRandomSet(currSol, subsets); // Selected nothing
        else
            newSolution = dice switch
            {
                < 0.25 => _addRandomSet(currSol, subsets),
                < 0.5 => _removeRandomSet(currSol, subsets),
                _ => _replaceRandomSet(currSol, subsets)
            };

        return newSolution;
    }

    private static List<Subset> _addRandomSet(IReadOnlyCollection<Subset> currSol, IEnumerable<Subset> subsets)
    {
        var unusedSubsets = subsets.Except(currSol).ToList();
        return new List<Subset>(currSol) { _getRandomSet(unusedSubsets) };
    }

    private static List<Subset> _removeRandomSet(List<Subset> currSol, IReadOnlyList<Subset> subsets)
    {
        var newSolution = new List<Subset>(currSol);
        newSolution.Remove(_getRandomSet(subsets));
        
        return newSolution;
    }

    private static List<Subset> _replaceRandomSet(List<Subset> currSol, IEnumerable<Subset> subsets)
    {
        var usedSet = _getRandomSet(currSol);
        var newSet = _getRandomSet(subsets.Except(currSol).ToList());

        var newSolution = new List<Subset>(currSol);
        newSolution.Remove(usedSet);
        newSolution.Add(newSet);
        
        return newSolution;
    }

    private static Subset _getRandomSet(IReadOnlyList<Subset> subsets)
    {
        var rand = new Random();
        var setIndex = rand.Next(0, subsets.Count);
        return subsets[setIndex];
    }
    
    private static double _getRandomNumber()
    {
        var rand = new Random();
        return rand.NextDouble();
    }

    private static double _eFun(int curCost, int anotherCost, decimal temperature)
    {
        return Math.Exp((double)((curCost - anotherCost) / temperature));
    }
}
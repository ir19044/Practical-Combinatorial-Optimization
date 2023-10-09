using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.Tests;


[TestClass]
public class SimulatedAnnealingTest
{
    
    // MethodName: <What to test?>_<Test Input>_<Expected Output>
    [TestMethod]
    [DataRow(80)]
    [DataRow(90)]
    public void SimulatedAnnealing_BestAnswerGetPrecision_AcceptCover(int precision)
    {
        // Arrange
        var uSet = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
        var subsets = new List<Subset>
        {
            new(new List<int> { 1 }),
            new(new List<int> { 1, 3, 4, 6 }),
            new(new List<int> { 1, 6, 7 }),
            new(new List<int> { 2, 3, 4 }),
            new(new List<int> { 2, 3, 5, 7 }),
            new(new List<int> { 2, 3, 4, 6, 7 }),
            new(new List<int> { 3, 4 }),
            new(new List<int> { 5 }),
            new(new List<int> { 7 })
        };

        // Act
        var anw = _runSimulatedAnnealing(uSet, subsets, expectedCost: 2); // expected: { 2, 3, 5, 7}, { 1, 3, 4, 6}
        
        // Assert
        foreach (var asw in anw.Result) 
            Assert.IsTrue(asw.EndCost <= asw.StartCost);
        Assert.IsTrue(anw.Precision >= precision / 100m);
    }

    private static (List<Result> Result, decimal Precision)_runSimulatedAnnealing(
        List<int> uSet, List<Subset> subsets, int expectedCost, int shots = 1000)
    {
        var results = new List<Result>();

        var temperature = new Temperature(tempStateCount: 1000);
        var alg = new SimulatedAnnealing(temperature, beta: 3);

        for (var i = 0; i < shots; i++) results.Add(alg.Process(uSet, subsets));
        var precision = Math.Round((decimal)results.Count(x => x.EndCost == expectedCost) / shots, 4);

        return (results, precision);
    }
}
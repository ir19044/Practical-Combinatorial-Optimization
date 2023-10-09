using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.Tests;


[TestClass]
public class SimulatedAnnealingTest
{
    private static TestContext testContextInstance;

    // Property to access the TestContext instance
    public TestContext TestContext
    {
        get { return testContextInstance; }
        set { testContextInstance = value; }
    }

    // MethodName: <What to test?>_<Test Input>_<Expected Output>
    [TestMethod]
    [DataRow(80)]
    [DataRow(90)]
    public void SetOf7From9_BestAnswerGetPrecision_AcceptCover(int precision)
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
    
    [TestMethod]
    [DataRow(95)]
    [DataRow(99)]
    public void SetOf4From5_BestAnswerGetPrecision_AcceptCover(int precision)
    {
        // Arrange
        var uSet = new List<int> { 1, 2, 3, 4 };
        var subsets = new List<Subset>
        {
            new(new List<int> { 1, 2 }),
            new(new List<int> { 2, 3 }),
            new(new List<int> { 1, 4 }),
            new(new List<int> { 3 }),
            new(new List<int> { 4 })
        };

        // Act
        var anw = _runSimulatedAnnealing(uSet, subsets, expectedCost: 2); // expected: { 1, 4}, { 2, 3}
        
        // Assert
        foreach (var asw in anw.Result) 
            Assert.IsTrue(asw.EndCost <= asw.StartCost);
        Assert.IsTrue(anw.Precision >= precision / 100m);
    }
    
    [TestMethod]
    [DataRow(95)]
    [DataRow(99)]
    public void SetOf8From8_BestAnswerGetPrecision_AcceptCover(int precision)
    {
        // Arrange
        var uSet = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
        var subsets = new List<Subset>
        {
            new(new List<int> { 1 }),
            new(new List<int> { 1, 2, 5 }), //first Fit choice 2
            new(new List<int> { 3, 6, 7 }), //first Fit choice 3
            new(new List<int> { 4, 5, 6, 7, 8 }), //first Fit choice 1
            new(new List<int> { 2, 3, 4, 5 }),
            new(new List<int> { 1, 6, 7, 8 }),
            new(new List<int> { 1, 2 }),
            new(new List<int> { 4, 7, 8 })
        };

        // Act
        var anw = _runSimulatedAnnealing(uSet, subsets, expectedCost: 2); // expected: { 2, 3, 4, 5}, { 1, 6, 7, 8}
        
        // Assert
        foreach (var asw in anw.Result) 
            Assert.IsTrue(asw.EndCost <= asw.StartCost);
        Assert.IsTrue(anw.Precision >= precision / 100m);
    }
    
    [TestMethod]
    [DataRow(85)]
    [DataRow(90)]
    [DataRow(95)]
    public void SetOf12From10_BestAnswerGetPrecision_AcceptCover(int precision)
    {
        // Arrange
        var uSet = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        var subsets = new List<Subset>
        {
            new(new List<int> { 1, 2, 3, 4 }), //first Fit choice 1
            new(new List<int> { 5, 6, 10 }), //first Fit choice 2
            new(new List<int> { 6, 7, 11 }), //first Fit choice 3
            new(new List<int> { 7, 12 }), //first Fit choice 5
            new(new List<int> { 5, 6, 7 }),
            new(new List<int> { 10, 11, 12 }),
            new(new List<int> { 10 }),
            new(new List<int> { 2, 3, 4, 10 }),
            new(new List<int> { 8, 9 }), //first Fit choice 4
            new(new List<int> { 11 }),
        };

        // Act
        var anw = _runSimulatedAnnealing(uSet, subsets, expectedCost: 4); // expected: {1, 2, 3, 4}, {5, 6 ,7}, {8, 9}, {10, 11, 12}
        
        // Assert
        foreach (var asw in anw.Result) 
            Assert.IsTrue(asw.EndCost <= asw.StartCost);
        Assert.IsTrue(anw.Precision >= precision / 100m);
    }
    
    [TestMethod]
    [DataRow(90)]
    [DataRow(95)]
    public void SetOf15From10_BestAnswerGetPrecision_AcceptCover(int precision)
    {
        // Arrange
        var uSet = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        var subsets = new List<Subset>
        {
            new(new List<int> { 1, 2 }), //first Fit choice 1
            new(new List<int> { 4, 5 }), //first Fit choice 2
            new(new List<int> { 7, 8 }), //first Fit choice 3
            new(new List<int> { 10, 11 }), //first Fit choice 4
            new(new List<int> { 13, 14 }), //first Fit choice 5
            new(new List<int> { 15 }), //first Fit choice 6
            
            new(new List<int> { 1, 2 }),
            new(new List<int> { 3, 4 }), //first Fit choice 7
            new(new List<int> { 5, 6 }), //first Fit choice 8
            new(new List<int> { 9, 10 }), //first Fit choice 9
            new(new List<int> { 11, 12 }) //first Fit choice 10
        };

        // Act
        var anw = _runSimulatedAnnealing(uSet, subsets, expectedCost: 8);
        // expected: {1,2}, {3,4}, {5,6}, {7,8}, {9,10}, {11,12}, {13,14}, {15}
        
        // Assert
        foreach (var asw in anw.Result) 
            Assert.IsTrue(asw.EndCost <= asw.StartCost);
        Assert.IsTrue(anw.Precision >= precision / 100m);
    }
    
    [TestMethod]
    [DataRow(100)]
    public void SetOf5From1_BestAnswerGetPrecision_AcceptCover(int precision)
    {
        // Arrange
        var uSet = new List<int> { 1, 2, 3, 4, 5 };
        var subsets = new List<Subset>
        {
            new(new List<int> { 1, 2, 3, 4, 5 })
        };

        // Act
        var anw = _runSimulatedAnnealing(uSet, subsets, expectedCost: 1); // expected: { 1, 2, 3, 4, 5}
        
        // Assert
        foreach (var asw in anw.Result) 
            Assert.IsTrue(asw.EndCost <= asw.StartCost);
        Assert.IsTrue(anw.Precision >= precision / 100m);
    }
    
    [TestMethod]
    [DataRow(100)]
    public void UnableCoverAll_BestAnswerGetPrecision_AcceptCover(int precision)
    {
        // Arrange
        var uSet = new List<int> { 1, 2, 3, 4, 5 };
        var subsets = new List<Subset>
        {
            new(new List<int> { 3 }),
            new(new List<int> { 1, 2, 3, 4 }),
            new(new List<int> { 1, 2 })
        };

        // Act
        var anw = _runSimulatedAnnealing(uSet, subsets, expectedCost: 4); // expected: { 1, 2, 3, 4}, count(usedSets)+3*count(uncovered)
        
        // Assert
        foreach (var asw in anw.Result) 
            Assert.IsTrue(asw.EndCost <= asw.StartCost);
        Assert.IsTrue(anw.Precision >= precision / 100m);
    }
    
    [TestMethod]
    [DataRow(100)]
    public void UnableCoverAny_BestAnswerGetPrecision_AcceptCover(int precision)
    {
        // Arrange
        var uSet = new List<int> { 1, 2, 3, 4, 5 };
        var subsets = new List<Subset>();

        // Act
        var anw = _runSimulatedAnnealing(uSet, subsets, expectedCost: 15); // expected: { }, count(usedSets)+3*count(uncovered)
        
        // Assert
        foreach (var asw in anw.Result) 
            Assert.IsTrue(asw.EndCost <= asw.StartCost);
        Assert.IsTrue(anw.Precision >= precision / 100m);
    }

    private static (List<Result> Result, decimal Precision)_runSimulatedAnnealing(
        List<int> uSet, List<Subset> subsets, int expectedCost, int shots = 1000)
    {
        Debug.WriteLine($"Starting test: {testContextInstance.TestName}");
        Debug.WriteLine("================================================================");

        var results = new List<Result>();

        var temperature = new Temperature(tempStateCount: 1000); //default count of steps to decrease temperature
        var alg = new SimulatedAnnealing(temperature, beta: 3); //beta - Weight for Cost Function: Count of uncovered elements

        for (var i = 0; i < shots; i++) results.Add(alg.Process(uSet, subsets));
        var precision = Math.Round((decimal)results.Count(x => x.EndCost == expectedCost) / shots, 4);
        
        Debug.WriteLine($"Probability of the best solution: {precision*100} % ({shots} shots)");
        Debug.WriteLine($"Expected cost:                    {expectedCost}");
        Debug.WriteLine($"First fit found cost:             {results.First().StartCost}");
        Debug.WriteLine($"The best found cost:              {results.Min(x => x.EndCost)}");
        Debug.WriteLine($"1 shot execution time:            {Math.Round(results.First().TimeProcessed, 4)} seconds");
        Debug.WriteLine($"{shots} shots execution time:        {Math.Round(results.Sum(x => x.TimeProcessed), 4)} seconds");
        Debug.WriteLine("================================================================ \n \n");

        return (results, precision);
    }
}
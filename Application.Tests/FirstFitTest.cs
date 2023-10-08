using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.Tests;


[TestClass]
public class FirstFitTest
{

    // MethodName: <What to test?>_<Test Input>_<Expected Output>
    [TestMethod]
    public void CoverAll_SetOf7_AcceptCover()
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
        var asw = FirstFit.FindFirstSolution(uSet, subsets).ToList();

        // Assert
        CollectionAssert.AreEqual(asw[0].SubSet, new List<int> { 2, 3, 4, 6, 7 }); // largest 1st set, uncovered = { 1, 5 }
        Assert.AreEqual(asw[0].Weight, 0);

        CollectionAssert.AreEqual(asw[1].SubSet, new List<int> { 1 }); // uncovered = { 5 }
        Assert.AreEqual(asw[1].Weight, 0);

        CollectionAssert.AreEqual(asw[2].SubSet, new List<int> { 2, 3, 5, 7 });  //  uncovered = { }
        Assert.AreEqual(asw[2].Weight, 0);
        
        Assert.IsTrue(asw.Count == 3);
    }
    
    [TestMethod]
    public void CoverAll_SetOf4_AcceptCover()
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
        var asw = FirstFit.FindFirstSolution(uSet, subsets).ToList();

        // Assert
        CollectionAssert.AreEqual(asw[0].SubSet, new List<int> { 1, 2 }); // largest 1st set, uncovered = { 3, 4 }
        Assert.AreEqual(asw[0].Weight, 0);

        CollectionAssert.AreEqual(asw[1].SubSet, new List<int> { 2, 3 }); // uncovered = { 4 }
        Assert.AreEqual(asw[1].Weight, 0);

        CollectionAssert.AreEqual(asw[2].SubSet, new List<int> { 1, 4 });  //  uncovered = { }
        Assert.AreEqual(asw[2].Weight, 0);
        
        Assert.IsTrue(asw.Count == 3);
    }
    
    [TestMethod]
    public void CoverAll_OneSubset_AcceptCover()
    {
        // Arrange
        var uSet = new List<int> { 1, 2, 3, 4, 5 };
        var subsets = new List<Subset>
        {
            new(new List<int> { 1, 2, 3, 4, 5 })
        };

        // Act
        var asw = FirstFit.FindFirstSolution(uSet, subsets).ToList();

        // Assert
        CollectionAssert.AreEqual(asw[0].SubSet, new List<int> { 1, 2, 3, 4 ,5 }); // largest 1st set, uncovered = {  }
        Assert.AreEqual(asw[0].Weight, 0);
        
        Assert.IsTrue(asw.Count == 1);
    }
    
    [TestMethod]
    public void UnableCoverAll_SetOf5_AcceptCover()
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
        var asw = FirstFit.FindFirstSolution(uSet, subsets).ToList();

        // Assert
        CollectionAssert.AreEqual(asw[0].SubSet, new List<int> { 1, 2, 3, 4 }); // largest 1st set, uncovered = { 5 }
        Assert.AreEqual(asw[0].Weight, 0);
        
        Assert.IsTrue(asw.Count == 1);
    }
    
    [TestMethod]
    public void UnableCoverAny_SetOf5_AcceptCover()
    {
        // Arrange
        var uSet = new List<int> { 1, 2, 3, 4, 5 };
        var subsets = new List<Subset>();

        // Act
        var asw = FirstFit.FindFirstSolution(uSet, subsets).ToList();

        // Assert
        Assert.IsTrue(asw.Count == 0);
    }
}
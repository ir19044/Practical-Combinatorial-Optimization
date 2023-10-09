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
    public void CoverAll_SetOf8_AcceptCover()
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
        var asw = FirstFit.FindFirstSolution(uSet, subsets).ToList();

        // Assert
        CollectionAssert.AreEqual(asw[0].SubSet, new List<int> { 4, 5, 6, 7, 8 }); // largest 1st set, uncovered = { 1, 2, 3 }
        Assert.AreEqual(asw[0].Weight, 0);

        CollectionAssert.AreEqual(asw[1].SubSet, new List<int> { 1, 2, 5 }); // uncovered = { 3 }
        Assert.AreEqual(asw[1].Weight, 0);

        CollectionAssert.AreEqual(asw[2].SubSet, new List<int> { 3, 6, 7 });  //  uncovered = { }
        Assert.AreEqual(asw[2].Weight, 0);
        
        Assert.IsTrue(asw.Count == 3);
    }
    
    [TestMethod]
    public void CoverAll_SetOf12_AcceptCover()
    {
        // Arrange
        var uSet = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        var subsets = new List<Subset>
        {
            new(new List<int> { 1, 2, 3, 4 }),
            new(new List<int> { 5, 6, 10 }),
            new(new List<int> { 6, 7, 11 }),
            new(new List<int> { 7, 12 }),
            new(new List<int> { 5, 6, 7 }),
            new(new List<int> { 10, 11, 12 }),
            new(new List<int> { 10 }),
            new(new List<int> { 2, 3, 4, 10 }),
            new(new List<int> { 8, 9 }),
            new(new List<int> { 11 }),
        };

        // Act
        var asw = FirstFit.FindFirstSolution(uSet, subsets).ToList();

        // Assert
        CollectionAssert.AreEqual(asw[0].SubSet, new List<int> { 1, 2, 3, 4 }); // largest 1st set, uncovered = { 5, 6, 7, 8, 9, 10 ,11 ,12 }
        Assert.AreEqual(asw[0].Weight, 0);

        CollectionAssert.AreEqual(asw[1].SubSet, new List<int> { 5, 6, 10 }); // uncovered = { 7, 8, 9, 11 ,12 }
        Assert.AreEqual(asw[1].Weight, 0);

        CollectionAssert.AreEqual(asw[2].SubSet, new List<int> { 6, 7, 11 });  //  uncovered = { 8, 9, 12 }
        Assert.AreEqual(asw[2].Weight, 0);
        
        CollectionAssert.AreEqual(asw[3].SubSet, new List<int> { 8, 9 });  //  uncovered = { 12 }
        Assert.AreEqual(asw[3].Weight, 0);
        
        CollectionAssert.AreEqual(asw[4].SubSet, new List<int> { 7, 12 });  //  uncovered = { }
        Assert.AreEqual(asw[4].Weight, 0);
        
        Assert.IsTrue(asw.Count == 5);
    }
    
    [TestMethod]
    public void CoverAll_SetOf15_AcceptCover()
    {
        // Arrange
        var uSet = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        var subsets = new List<Subset>
        {
            new(new List<int> { 1, 2 }),
            new(new List<int> { 4, 5 }),
            new(new List<int> { 7, 8 }),
            new(new List<int> { 10, 11 }),
            new(new List<int> { 13, 14 }),
            new(new List<int> { 15 }),
            
            new(new List<int> { 1, 2 }),
            new(new List<int> { 3, 4 }),
            new(new List<int> { 5, 6 }),
            new(new List<int> { 9, 10 }),
            new(new List<int> { 11, 12 })
        };

        // Act
        var asw = FirstFit.FindFirstSolution(uSet, subsets).ToList();

        // Assert
        CollectionAssert.AreEqual(asw[0].SubSet, new List<int> { 1, 2 }); // largest 1st set
        Assert.AreEqual(asw[0].Weight, 0);

        CollectionAssert.AreEqual(asw[1].SubSet, new List<int> { 4, 5 });
        Assert.AreEqual(asw[1].Weight, 0);

        CollectionAssert.AreEqual(asw[2].SubSet, new List<int> { 7, 8 });
        Assert.AreEqual(asw[2].Weight, 0);
        
        CollectionAssert.AreEqual(asw[3].SubSet, new List<int> { 10, 11 });
        Assert.AreEqual(asw[3].Weight, 0);
        
        CollectionAssert.AreEqual(asw[4].SubSet, new List<int> { 13, 14 });
        Assert.AreEqual(asw[4].Weight, 0);
        
        CollectionAssert.AreEqual(asw[5].SubSet, new List<int> { 15 }); // uncovered: {3,6,9,12}
        Assert.AreEqual(asw[5].Weight, 0);
        
        CollectionAssert.AreEqual(asw[6].SubSet, new List<int> { 3, 4 }); // uncovered: {6,9,12}
        Assert.AreEqual(asw[6].Weight, 0);
        
        CollectionAssert.AreEqual(asw[7].SubSet, new List<int> { 5, 6 }); // uncovered: {9,12}
        Assert.AreEqual(asw[7].Weight, 0);
        
        CollectionAssert.AreEqual(asw[8].SubSet, new List<int> { 9, 10 }); // uncovered: {12}
        Assert.AreEqual(asw[8].Weight, 0);
        
        CollectionAssert.AreEqual(asw[9].SubSet, new List<int> { 11, 12 }); // uncovered: { }
        Assert.AreEqual(asw[9].Weight, 0);
        
        Assert.IsTrue(asw.Count == 10);
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
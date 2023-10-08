namespace Application;

public class Subset
{
    public readonly List<int> SubSet;
    public readonly int Weight;

    public Subset(List<int> subset, int weight = 0)
    {
        SubSet = subset;
        Weight = weight;
    }
    
    public static bool IsEqual(ICollection<Subset> fList, ICollection<Subset> sList)
    {
        if (fList.Count != sList.Count)
            return false;
        
        return fList.All(fList.Contains) && sList.All(fList.Contains);
    }
}
namespace Application;

public class Subset
{
    public List<int> SubSet;
    public int Weight;

    public Subset(List<int> subset, int weight = 0)
    {
        SubSet = subset;
        Weight = weight;
    }
}
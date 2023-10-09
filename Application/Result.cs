namespace Application;

public class Result
{
    public List<Subset> SelectedSubsets;
    public int StartCost;
    public int EndCost;
    public DateTime TimeProcessed;

    public Result(List<Subset> selectedSubsets, int startCost, int endCost, DateTime timeProcessed)
    {
        SelectedSubsets = selectedSubsets;
        StartCost = startCost;
        EndCost = endCost;
        TimeProcessed = timeProcessed;
    }
}
namespace Application;

public struct Temperature
{
    public decimal TempStart { get; }
    public decimal TempEnd { get; }
    public int TempStateCount { get; }
    public List<decimal> TempList { get; }
    public List<int> StepList { get; }

    public Temperature(decimal tempStart = 2m, decimal tempEnd = 0m, int tempStateCount = 5, List<int>? stepList = null)
    {
        TempStart = tempStart;
        TempEnd = tempEnd;
        TempStateCount = tempStateCount;
        StepList = stepList ?? Enumerable.Repeat(1, tempStateCount).ToList();
        TempList = new List<decimal>();
        GenerateTemperatureList();
    }

    private void GenerateTemperatureList()
    {
        var tempStep = (TempStart - TempEnd) / (TempStateCount - 1);

        var currentTemp = TempStart;
        for (var i = 0; i < TempStateCount; i++)
        {
            TempList.Add(currentTemp);
            currentTemp -= tempStep;
        }
    }
    
    public static Temperature CreateWithDefaults()
    {
        return new Temperature(2m, 0.4m, 5, null);
    }
}

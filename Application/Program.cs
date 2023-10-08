namespace Application;

class Program
{
    public static List<int> uSet; // Universal Set
    public static List<Subset> subsets; //Set of subsets with each subset weight
    private static List<Subset> _solution; // Set of subsets as solution

    public static void InitMembers(bool isDefault = true)
    {
        if (isDefault)
        {
            uSet = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
            subsets = new List<Subset>
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
        }
        else
        {
            uSet = new List<int>();
            subsets = new List<Subset>();
        }
    }
    
    static void Main()
    {
        InitMembers();

        //var temperature = Temperature.CreateWithDefaults();
        var temperature = new Temperature(tempStateCount: 1000);

        var alg = new SimulatedAnnealing(temperature, beta: 3);
        var solution = alg.Process(uSet, subsets);

        foreach (var subset in solution)
        {
            foreach (var v in subset.SubSet)
            {
                Console.Write(v);
            }
            Console.Write('\n');
        }

    }
}
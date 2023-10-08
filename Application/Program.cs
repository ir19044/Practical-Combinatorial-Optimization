namespace Application;

class Program
{
    public static List<int> uSet; // Universal Set
    public static List<Subset> subsets; //Set of subsets with each subset weight
    private static List<Subset> _solution; // Set of subsets as solution

    static void Main()
    {
        _initMembers(isDefault: false);
        
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

    private static void _initMembers(bool isDefault = true)
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
            // 1.Step - read cheater names
            var cheaterDict = _readCheatersFromConsole();
            _printCheaters(cheaterDict);

            uSet = new List<int>();
            foreach (var (key,_) in cheaterDict) uSet.Add(key);
            
            // 2.Step - read cheater groups
            var subsetsWithoutTime = _readCheaterGroupsFromConsole();
            
            // 3.Step - read time to visit each group
            subsets = _readCheaterGroupTimeFromConsole(subsetsWithoutTime);
            _printCheaterGroups(subsets);
        }
    }

    private static Dictionary<int, string> _readCheatersFromConsole()
    {
        Console.WriteLine("Ievadiet bleza vardu (ievadiet '0', lai pabeigtu):");

        var index = 1;
        var cheaterDict = new Dictionary<int, string>();
        while (true)
        {
            var input = Console.ReadLine();
                
            if(string.IsNullOrWhiteSpace(input)) continue;
            if (input == "0") break;
                
            cheaterDict.Add(index, input);
            index++;
        }
        Console.Write("Visi blezi ir ievaditi! \n");

        return cheaterDict;
    }

    private static List<Subset> _readCheaterGroupsFromConsole()
    {
        Console.WriteLine("Ievadiet grupu, izmantojot numurus atdalitos ar komatiem (ievadiet '0', lai pabeigtu):");
            
        var rawSubsets = new List<Subset>();

        while (true)
        {
            var items = new List<int>();
            var input = Console.ReadLine();

            if(string.IsNullOrWhiteSpace(input)) continue;
            if (input == "0") break;

            var validInput = true;
            var parts = input.Split(',');
            foreach (var part in parts)
            {
                if (int.TryParse(part.Trim(), out var intValue))
                {
                    if(!items.Contains(intValue)) items.Add(intValue);
                }
                else
                {
                    Console.WriteLine($"Bledis {part.Trim()} nav atrasts! Meginiet velreiz \n");
                    validInput = false;
                    break;
                }
            }

            if(validInput && rawSubsets.All(x => x.SubSet != items)) 
                rawSubsets.Add(new Subset(new List<int>(items)));
        }
        Console.Write("Visas grupas ir ievaditas! \n");

        return rawSubsets;
    }

    private static List<Subset> _readCheaterGroupTimeFromConsole(List<Subset> rawGroups)
    {
        Console.WriteLine("Ievadiet katras grupas apmeklesanas laiku (vesels skaitlis): \n");
            
        var groups = new List<Subset>();
        
        foreach (var s in rawGroups)
        {
            Console.Write(string.Join(" ", s.SubSet));
            Console.Write(" apm. laiks: ");

            bool validInput;
            do
            {
                validInput = true;
                var input = Console.ReadLine();

                if (int.TryParse(input, out var intValue))
                {
                    groups.Add(new Subset(new List<int>(s.SubSet), intValue));
                }
                else
                {
                    Console.WriteLine($"Nekorekts {input} apm. laiks! Meginiet velreiz \n");
                    validInput = false;
                }
            } while (validInput is false);
        }
        
        return groups;
    }
    
    private static void _printCheaters(Dictionary<int, string> cheaters)
    {
        foreach (var (index, name) in cheaters)
        {
            Console.WriteLine($"Bleza numurs: {index}, Vards: {name}");
        }
        Console.WriteLine('\n');
    }

    private static void _printCheaterGroups(List<Subset> cheaterGroups)
    {
        foreach (var group in cheaterGroups)
        {
            Console.WriteLine($"Grupa: {string.Join(" ", group.SubSet)}; Laiks apmeklejumam: {group.Weight};");
        }
        Console.WriteLine(); // empty line
    }
}
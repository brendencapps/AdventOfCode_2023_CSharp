using System.Reflection;

CheckSolution("Part 1", Part1Solution("..\\..\\..\\Inputs\\part1.txt"), 54697);
CheckSolution("Part 2", Part2Solution("..\\..\\..\\Inputs\\part1.txt"), 54885);

int Part1Solution(string path)
{
    return FileLinesReader(path).ConvertAll(line =>
    {
        var firstDigit = line.First(ch => ch is >= '0' and <= '9') - '0';
        var lastDigit = line.Last(ch => ch is >= '0' and <= '9') - '0';
        return firstDigit * 10 + lastDigit;
    }).Sum();

}

int Part2Solution(string path)
{
    var numberMap = new Dictionary<string, int>
    {
        { "0", 0 },
        { "1", 1 },
        { "2", 2 },
        { "3", 3 },
        { "4", 4 },
        { "5", 5 },
        { "6", 6 },
        { "7", 7 },
        { "8", 8 },
        { "9", 9 },
        { "zero", 0 },
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 }
    };
    
    return FileLinesReader(path).ConvertAll(line =>
    {
        
        var firstDigitKey = FirstMatch(line, numberMap.Keys);
        var lastDigitKey = LastMatch(line, numberMap.Keys);
        if (firstDigitKey == null || lastDigitKey == null)
        {
            return 0;
        }

        if (numberMap.TryGetValue(firstDigitKey, out int firstDigit) &&
            numberMap.TryGetValue(lastDigitKey, out int lastDigit))
        {
            return firstDigit * 10 + lastDigit;
        }

        return 0;

    }).Sum();
}


List<String> FileLinesReader(string path)
{
    return File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, path)).ToList();
}



void CheckSolution(string label, int result, int? expectedResult = null)
{
    if (expectedResult == null)
    {
        Console.WriteLine($"{label}: {result}");
    }
    else if (result != expectedResult)
    {
        Console.WriteLine($"{label}: Wrong solution {expectedResult} != {result}");
    }
}

string? FirstMatch(string str, Dictionary<string, int>.KeyCollection list)
{
    for (int i = 0; i < str.Length; i++)
    {
        foreach (var match in list)
        {
            if (str.Substring(i).StartsWith(match))
            {
                return match;
            }
        }
    }

    return null;
}

string? LastMatch(string str, Dictionary<string, int>.KeyCollection list)
{
    for (int i = 0; i < str.Length; i++)
    {
        foreach (var match in list)
        {
            if (str.Substring(str.Length - i - 1).StartsWith(match))
            {
                return match;
            }
        }
    }

    return null;
}
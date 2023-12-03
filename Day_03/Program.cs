using System.Reflection;

CheckSolution("Part 1 Example", Part1Solution("..\\..\\..\\Inputs\\example.txt"), 4361);
CheckSolution("Part 1", Part1Solution("..\\..\\..\\Inputs\\part1.txt"), 554003);
CheckSolution("Part 2 Example", Part2Solution("..\\..\\..\\Inputs\\example.txt"), 467835);
CheckSolution("Part 2", Part2Solution("..\\..\\..\\Inputs\\part1.txt"), 87263515);

int Part1Solution(string path)
{
    var problemInput = new ProblemInput(path);
    
    return problemInput.Parts.Sum(x =>
    {
        return problemInput.Symbols.Any(y => y.NextToPartNumber(x)) ? int.Parse(x.Value) : 0;
    });

}

int Part2Solution(string path)
{
    var problemInput = new ProblemInput(path);
    return problemInput.Symbols.Sum(x =>
    {
        if (x.Value == '*')
        {
            var adjacentNumbers = problemInput.Parts.Where(x.NextToPartNumber).ToList();
            if (adjacentNumbers.Count == 2)
            {
                return int.Parse(adjacentNumbers[0].Value) * int.Parse(adjacentNumbers[1].Value);
            }
        }

        return 0;
    });
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

class Symbol
{
    public char Value { get; }
    private readonly int _row;
    private readonly int _col;
    
    public Symbol(char value, int row, int col)
    {
        Value = value;
        _row = row;
        _col = col;
    }

    public bool NextToPartNumber(PartNumber part)
    {
        return part.Col - part.Value.Length - 1 <= _col && _col <= part.Col &&
               part.Row - 1 <= _row && _row <= part.Row + 1;
    }
}

class PartNumber
{
    public string Value { get; }
    public int Row { get;  }
    public int Col { get; }

    public PartNumber(string value, int row, int col)
    {
        Value = value;
        Row = row;
        Col = col;
    }
}

class ProblemInput
{
    public List<Symbol> Symbols { get; } = new();
    public List<PartNumber> Parts { get; } = new();

    public ProblemInput(string path)
    {
        var lines = FileLinesReader(path);
        for (int row = 0; row < lines.Count; row++)
        {
            var line = lines[row];
            var currentNumber = "";
            for (int col = 0; col < line.Length; col++)
            {
                if (line[col] >= '0' && line[col] <= '9')
                {
                    currentNumber += line[col];
                }
                else
                {
                    if (line[col] != '.')
                    {
                        Symbols.Add(new Symbol(line[col], row, col));
                    }

                    if (!string.IsNullOrEmpty(currentNumber))
                    {
                        Parts.Add(new PartNumber(currentNumber, row, col));
                        currentNumber = "";
                    }
                }
            }
            if (!string.IsNullOrEmpty(currentNumber))
            {
                Parts.Add(new PartNumber(currentNumber, row, line.Length));
            }
        }
    }
    
    private List<String> FileLinesReader(string path)
    {
        return File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, path)).ToList();
    }
}
using System.Reflection;

CheckSolution("Part 1 Example", Part1Solution("..\\..\\..\\Inputs\\example.txt"), 8);
CheckSolution("Part 1", Part1Solution("..\\..\\..\\Inputs\\part1.txt"), 2406);
CheckSolution("Part 2 Example", Part2Solution("..\\..\\..\\Inputs\\example.txt"), 2286);
CheckSolution("Part 2", Part2Solution("..\\..\\..\\Inputs\\part1.txt"), 78375);

int Part1Solution(string path)
{
    return FileLinesReader(path).ConvertAll(line =>
    {
        var game = new Game(line);
        return game.Rounds.Any(x => x.Red > 12 || x.Green > 13 || x.Blue > 14) ? 0 : game.GameId;
    }).Sum();

}

int Part2Solution(string path)
{
    return FileLinesReader(path).ConvertAll(line =>
    {
        var game = new Game(line);
        return game.Rounds.Max(x => x.Red) * game.Rounds.Max(x => x.Green) * game.Rounds.Max(x => x.Blue);
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

class Game
{
    public int GameId { get; }
    public List<Round> Rounds { get; }
    public Game(string game)
    {
        var gameParts = game.Split(": ");
        GameId = int.Parse(gameParts[0].Split(" ")[1]);
        Rounds = new List<Round>();

        foreach (var round in gameParts[1].Split("; "))
        {
            Rounds.Add(new Round(round));
        }
    }
}

class Round
{
    public int Red { get;  }
    public int Green { get;  }
    public int Blue { get;  }

    public Round(string round)
    {
        Red = 0;
        Green = 0;
        Blue = 0;
        
        foreach (var cubes in round.Split(", "))
        {
            var cubeParts = cubes.Split(" ");
            switch (cubeParts[1])
            {
                case "red": Red = int.Parse(cubeParts[0]); break;
                case "green": Green = int.Parse(cubeParts[0]); break;
                case "blue": Blue = int.Parse(cubeParts[0]); break;
            }
        }
    }
}
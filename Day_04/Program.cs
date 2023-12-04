

using System.Reflection;
using System.Text.RegularExpressions;

CheckSolution("Part 1 Example", Part1Solution("..\\..\\..\\Inputs\\example.txt"), 13);
CheckSolution("Part 1", Part1Solution("..\\..\\..\\Inputs\\part1.txt"), 20667);
CheckSolution("Part 2 Example", Part2Solution("..\\..\\..\\Inputs\\example.txt"), 30);
CheckSolution("Part 2", Part2Solution("..\\..\\..\\Inputs\\part1.txt"), 5833065);


int Part1Solution(string path)
{
    return FileLinesReader(path).Sum(x =>
    {
        var card = new Card(x);
        if (card.Matches == 0) return 0;
        return (int)Math.Pow(2.0, new Card(x).Matches - 1);
    });

}

int Part2Solution(string path)
{
    var cards = FileLinesReader(path).ConvertAll(x => new Card(x));
    for (int i = 0; i < cards.Count; i++)
    {
        for (int j = 0; j < cards[i].Matches; j++)
        {
            cards[i + j + 1].Copies += cards[i].Copies;
        }
    }
    
    return cards.Sum(x => x.Copies);
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


List<String> FileLinesReader(string path)
{
    return File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, path)).ToList();
}


public class Card {
    public int Matches { get; }
    public int Copies { get; set; }

    public Card(string card)
    {
        var cardParts = card.Substring(card.IndexOf(':') + 1).Trim().Split(" | ");
        var winningNumbersTrimmed = Regex.Replace(cardParts[0].Trim(), "\\s+", " ");
        var cardNumbersTrimmed = Regex.Replace(cardParts[1].Trim(), "\\s+", " ");
        var winningNumbers = winningNumbersTrimmed.Split(" ").ToList().ConvertAll(int.Parse);
        var cardNumbers = cardNumbersTrimmed.Split(" ").ToList().ConvertAll(int.Parse);
        Matches = cardNumbers.Count(x => winningNumbers.Contains(x));
        Copies = 1;
    }
}
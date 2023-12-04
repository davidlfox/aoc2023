using System.Text.RegularExpressions;

public static class Day4
{
    public static void Run()
    {
        // int points = 0;
        Dictionary<int, int> cardsAndCopies = [];
        List<string> lines = File.ReadAllLines("./day4in.txt").ToList();
        for(int i = 0; i< lines.Count;i++)
        {
            int card, copiesToMake;
            ProcessLine(cardsAndCopies, lines, i, out card, out copiesToMake);
            UpsertCard(card, cardsAndCopies);
            if (cardsAndCopies.TryGetValue(card, out int value) && value > 1)
            {
                Console.WriteLine($"Card {card} already exists, making {copiesToMake} copies");
                for(int k = 0;k < value - 1; k++)
                {
                    ProcessLine(cardsAndCopies, lines, i, out card, out copiesToMake);
                }
            }
        }

        // Console.WriteLine($"Points: {points}");
        Console.WriteLine($"Part 2: {cardsAndCopies.Values.Sum()}");
    }

    private static void ProcessLine(Dictionary<int, int> cardsAndCopies, List<string> lines, int i, out int card, out int copiesToMake)
    {
        var matches = Regex.Matches(lines[i], @"Card\s+(\d+):\s+((\d+\s+)+)\|\s+((\d+\s*)+)");
        card = int.Parse(matches[0].Groups[1].Value);
        
        int[] wins = matches[0].Groups[2].Value.Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x)).ToArray();
        int[] mine = matches[0].Groups[4].Value.Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x)).ToArray();
        // Console.WriteLine(matches);
        IEnumerable<int> winningNums = wins.Intersect(mine);
        // if (winningNums.Count() > 0)
        // {
        //     int i = 0;
        //     int score = 1;
        //     while(i < winningNums.Count()-1)
        //     {
        //         score *= 2;
        //         i++;
        //     }
        //     points += score;
        // }
        copiesToMake = winningNums.Count();
        for (int j = 1; j <= copiesToMake; j++)
        {
            UpsertCard(card + j, cardsAndCopies);
        }
    }

    private static void UpsertCard(int card, Dictionary<int, int> cardsAndCopies)
    {
        if(cardsAndCopies.TryGetValue(card, out int value))
        {
            cardsAndCopies[card] = value + 1;
        }
        else
        {
            cardsAndCopies.Add(card, 1);
        }
    }
}
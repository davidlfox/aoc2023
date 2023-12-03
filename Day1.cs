using System.Text.RegularExpressions;

public static class Day1
{
    public static void Run()
    {
        var lines = File.ReadAllLines("./day1in.txt");
        int total = 0;
        foreach(var line in lines)
        {
            // remove all letters from line
            var digits = Regex.Replace(line, "[^0-9]", "").ToArray();
            if (digits.Length == 1)
            {
                // parse the char to int
                int num = int.Parse(digits[0].ToString());
                total += num + (num*10);
            }
            else
            {
                char firstNum = digits[0];
                char lastNum = digits[^1];
                total += int.Parse($"{firstNum}{lastNum}");
            }
        }

        Console.WriteLine($"Total: {total}");
    }
}
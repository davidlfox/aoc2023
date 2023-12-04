public static class Day3
{
    public static void Run()
    {
        // uniques: - * & $ @ / # = % +
        char[,] grid = new char[142,142];
        for(int i = 0;i < 142;i++)
        {
            grid[0,i] = 'x';
        }
        string[] lines = File.ReadAllLines("./day3in.txt");
        for(int i = 0;i < lines.Length;i++)
        {
            grid[i+1,0] = 'x';
            for(int j = 0;j < lines[i].Length;j++)
            {
                grid[i+1,j+1] = lines[i][j];
            }
            grid[i+1,141] = 'x';
        }
        for(int i = 0;i < 142;i++)
        {
            grid[141,i] = 'x';
        }
        // log the unique characters in the grid
        // List<char> uniques = [];
        // for(int i = 0;i < 142;i++)
        // {
        //     for(int j = 0;j < 142;j++)
        //     {
        //         if (!uniques.Contains(grid[i,j]))
        //         {
        //             uniques.Add(grid[i,j]);
        //         }
        //     }
        // }
        // Console.WriteLine($"Unique characters: {string.Join(", ", uniques)}");
        // print the character grid
        // for(int i = 0;i < 142;i++)
        // {
        //     for(int j = 0;j < 142;j++)
        //     {
        //         Console.Write(grid[i,j]);
        //     }
        //     Console.WriteLine();
        // }
        
        List<int> parts = [];
        List<char> uniques = ['-', '*', '&', '$', '@', '/', '#', '=', '%', '+'];
        Dictionary<(int, int), List<int>> gears = [];

        for(int i = 0;i < 142;i++)
        {
            for(int j = 0;j < 142;j++)
            {
                if (grid[i,j] == 'x')
                {
                    continue;
                }
                if(char.IsDigit(grid[i,j]))
                {
                    string fullNum = grid[i,j].ToString();
                    // find the full number by increasing j coordinate until we hit a character that isnt a digit
                    int jindex = j;
                    while(char.IsDigit(grid[i,jindex+1]))
                    {
                        fullNum += grid[i,jindex+1];
                        jindex++;
                    }
                    Console.WriteLine($"Found number {fullNum} at ({i},{j})");

                    List<(int, int)> coordsToCheck =
                    [
                        (i-1,j-1),
                        (i-1,j),
                        (i-1,j+1),
                        (i,j-1),
                        // (i,j+1),
                        (i+1,j-1),
                        (i+1,j),
                        (i+1,j+1),
                    ];
                    for(int k = j+1;k <= jindex;k++)
                    {
                        // add i-1,j; i-1,j+1, i+1,j, i+1,j+1 to coordsToCheck
                        coordsToCheck.Add((i-1,k));
                        coordsToCheck.Add((i-1,k+1));
                        coordsToCheck.Add((i+1,k));
                        coordsToCheck.Add((i+1,k+1));
                    }
                    // finally check to the right
                    coordsToCheck.Add((i,jindex+1));

                    foreach(var coord in coordsToCheck.Distinct())
                    {
                        if (grid[coord.Item1,coord.Item2] == '*')
                        {
                            parts.Add(int.Parse(fullNum));
                            if (gears.ContainsKey((coord.Item1,coord.Item2)))
                            {
                                gears[(coord.Item1,coord.Item2)].Add(int.Parse(fullNum));
                            }
                            else
                            {
                                gears[(coord.Item1, coord.Item2)] = [int.Parse(fullNum)];
                            }
                            Console.WriteLine($"Found {grid[coord.Item1,coord.Item2]} at ({coord.Item1},{coord.Item2})");
                            break;
                        }
                    }

                    // move forward ahead of the number you just checked
                    j = jindex;

                }
            }
        }

        // Console.WriteLine($"Part 1: {parts.Sum()}");
        // console writeline the sum of the product of the gears where the array length is exactly 2
        Console.WriteLine($"Part 2: {gears.Where(kvp => kvp.Value.Count == 2).Select(kvp => kvp.Value.Aggregate(1, (acc, val) => acc * val)).Sum()}");
    }
}
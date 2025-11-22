namespace Practic6;

public class Output
{
    public static void DrawTable(int tablesCount, List<string[]> strings)
    {
        int[] rowsSizes = new int[tablesCount];

        foreach (string[] str in strings)
        {
            for (int i = 0; i < str.Length; i++)
            {
                rowsSizes[i] = Math.Max(str[i].Length, rowsSizes[i]);
            }
        }
        int sumSizes = rowsSizes.Sum() + tablesCount + 1;
        
        Console.WriteLine(new string('-', sumSizes));
        foreach (string[] str in strings)
        {
            Console.Write("|");
            for (int i = 0; i < str.Length; i++)
            {
                Console.Write(str[i] + new string(' ',rowsSizes[i] - str[i].Length) + '|');
            }
            Console.WriteLine('\n' + new string('-', sumSizes));
        }

        Console.ReadLine();
    }
}




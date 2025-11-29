namespace Practic6;

public static class Output
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

public static class GetInput
{
    public static ParserOptions GetParserArgs()
        {
            string folder, file, rows;
            int[] pagination;
    
            while (true)
            {
                Console.Write("папка с таблицами: ");
                folder = Console.ReadLine()!;
                Console.Clear();
                if (Directory.Exists(folder))
                {
                    break;
                }
                Console.WriteLine("такой папки не существует, либо неверно указан путь");
            }
    
            while (true)
            {
                Console.Write("название файла (с расширением): ");
                file = Console.ReadLine()!; 
                Console.Clear();
                
                if (File.Exists($"{folder}/{file}"))
                {
                    break;
                }
                Console.WriteLine("такого файла не существует, либо неверно указан путь");
            }
            
            
            
            while (true)
            {
                string p;
                Console.Write("нужные строки (формат записи \"1-10\" - с первой строки по 10, включая первую строку (символ \"*\" означает включить все строки): ");
                p = Console.ReadLine()!;
                Console.Clear();
                if (p == "*")
                {
                    pagination = [-1, 0];
                    break;
                }
                
                string parsPag = ParserRegExs.Pagination(p);
                if (parsPag != "")
                {
                    string[] sPag = parsPag.Split('-');
                    if (int.Parse(sPag[1]) > int.Parse(sPag[0]))
                    {
                        pagination = [int.Parse(sPag[0]), int.Parse(sPag[1])];
                        break;
                    }
                    
                    if (int.Parse(sPag[1]) < int.Parse(sPag[0]))
                    {
                        Console.WriteLine("период не может идти в уменьшение");
                    }
                }
                Console.WriteLine("неверный формат ввода");
            }
    
            Console.Write("нужные столбцы (названия столбцов или \"*\" для всех столбцов): ");
            rows = Console.ReadLine()!;
            Console.Clear();
            return new ParserOptions(folder, file, pagination, rows);
        }
}



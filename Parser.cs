namespace Practic6;


public record SettingsParser(string FolderPath, string Name, int[] Pagination, string Rows)
{
    public string FolderPath { get; } = FolderPath;
    public string Name { get; } = Name;
    public string Rows { get; } = Rows;
    public int[] Pagination { get; } = Pagination;
    
    public string FullPath { get; } = FolderPath + "\\" + Name;
    public List<int> TablesNeedNums { get; } = new();
    public char Separator { get; } = Name.Split('.').Last() == "csv" ? ';' : '\t';
}



public class Parser
{
    private readonly SettingsParser _settings;
    private readonly StreamReader _reader;
    private readonly List<string[]> _parsedStrings = new();
    
    public Parser()
    {
        _settings = Input();
        _reader = new StreamReader(_settings.FullPath);
        
        string[] tablesFile = MyRegEx.TablesFile(_reader.ReadLine()!, _settings.Separator);

        if (_settings.Rows != "*")
        {
            
            foreach (string tableNeed in _settings.Rows.Split(' '))
            {
                for (int i = 0; i < tablesFile.Length; i++)
                {
                    if (tableNeed.ToLower() == tablesFile[i].ToLower())
                    {
                        _settings.TablesNeedNums.Add(i);
                    }
                }
            }
        }

        else
        {
            for (int i = 0; i < tablesFile.Length; i++)
            {
                _settings.TablesNeedNums.Add(i);
            }
        }
        
        if (_settings.TablesNeedNums.Count == 0)
        {
            throw new Exception("неверно введены столбцы, попробуйте еще раз");
        }
        
        string toAdd = "";

        for (int i = 0; i < tablesFile.Length; i++)
        {
            if (_settings.TablesNeedNums.Contains(i))
            {
                toAdd += tablesFile[i] + ' ';
            }
        }
        toAdd = toAdd.Trim();

        _parsedStrings.Add(toAdd.Split(' '));
        
        Pars();
    }

    private void Pars()
    {
        string line;
        int numLine = 1;
        while ((line = _reader.ReadLine()!) != null)
        {
            if (_settings.Pagination[0] == -1 || _settings.Pagination[0] <= numLine && numLine < _settings.Pagination[1])
            {
                string toAdd = "";
                string[] sLine = line.Split(_settings.Separator);
                for (int i = 0; i < sLine.Length; i++)
                {
                    if (_settings.TablesNeedNums.Contains(i))
                    {
                        toAdd += sLine[i] + '|';
                    }
                }
                toAdd = toAdd.Trim();
                _parsedStrings.Add(toAdd.Substring(0, toAdd.Length-1).Split('|'));
            }

            numLine++;
        }

        Output.DrawTable(_settings.TablesNeedNums.Count, _parsedStrings);
    }

    private SettingsParser Input()
    {
        string folder, file, rows;
        int[] pagination = [];

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
            
            string parsPag = MyRegEx.Pagination(p);
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
        return new SettingsParser(folder, file, pagination, rows);
    }


}
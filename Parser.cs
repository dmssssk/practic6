

namespace Practic6;

public class Parser
{
    private readonly SettingsPars _settings;
    private readonly StreamReader _reader;
    private readonly List<string[]> _parsedStrings = new List<string[]>();
    
    public Parser()
    {
        _settings = Input();
        // _settings = DebugSettings();
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

    // private SettingsPars DebugSettings()
    // {
    //     // return new SettingsPars("C:\\Users\\dimai\\RiderProjects\\Practic6\\Practic6\\testFiles","contractors.csv",[2, 10],"Id Name");
    //     // return new SettingsPars("C:\\Users\\dimai\\RiderProjects\\Practic6\\Practic6\\testFiles","employees.tsv",[-1, 0],"id Name salary");
    //     return new SettingsPars("C:\\Users\\dimai\\RiderProjects\\Practic6\\Practic6\\testFiles","employees.tsv",[-1, 0],"*");
    //     
    // }

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

        DrawTable();
    }

    void DrawTable()
    {
        int[] rowsSizes = new int[_settings.TablesNeedNums.Count];

        foreach (string[] str in _parsedStrings)
        {
            for (int i = 0; i < str.Length; i++)
            {
                rowsSizes[i] = Math.Max(str[i].Length, rowsSizes[i]);
            }
        }
        int sumSizes = rowsSizes.Sum() + _settings.TablesNeedNums.Count + 1;
        
        Console.WriteLine("-".Repeat(sumSizes));
        foreach (string[] str in _parsedStrings)
        {
            Console.Write("|");
            for (int i = 0; i < str.Length; i++)
            {
                Console.Write(str[i] + " ".Repeat(rowsSizes[i] - str[i].Length) + '|');
            }
            Console.WriteLine('\n' + "-".Repeat(sumSizes));
        }

        Console.ReadLine();
    }
    
    private SettingsPars Input()
    {
        string folder = "", file = "", rows = "";
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
        return new SettingsPars(folder, file, pagination, rows);
    }


}
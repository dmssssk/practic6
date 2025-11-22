

namespace Practic6;

public class Parser
{
    private readonly SettingsPars _settings;
    private readonly StreamReader _reader;
    private List<string[]> _parsedStrings = new List<string[]>();
    

    public Parser()
    {
        // _settings = Input();
        _settings = DebugSettings();
        _reader = new StreamReader(_settings.FullPath);
        
        string[] tablesFile = MyRegEx.TablesFile(_reader.ReadLine()!, _settings.Separator);
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

    private SettingsPars DebugSettings()
    {
        // return new SettingsPars("C:\\Users\\dimai\\RiderProjects\\Practic6\\Practic6\\testFiles","contractors.csv",[2, 10],"Id Name");
        return new SettingsPars("C:\\Users\\dimai\\RiderProjects\\Practic6\\Practic6\\testFiles","employees.tsv",[2, 10],"Id Name");
        
    }


    private void Pars()
    {
        string line;
        int numLine = 1;
        while ((line = _reader.ReadLine()) != null)
        {
            if (_settings.Pagination[0] <= numLine && numLine < _settings.Pagination[1])
            {
                
                
                
                
                string toAdd = "";
                string[] sLine = line.Split(_settings.Separator);
                for (int i = 0; i < sLine.Length; i++)
                {
                    // Console.WriteLine($"{_settings.TablesNeedNums[0]} {_settings.TablesNeedNums[1]} {i}");
                    if (_settings.TablesNeedNums.Contains(i))
                    {
                        toAdd += sLine[i] + '|';
                        // Console.WriteLine(sLine[i]);
                    }
                }
                toAdd = toAdd.Trim();
                _parsedStrings.Add(toAdd.Substring(0, toAdd.Length-1).Split('|'));
                
                
            }

            numLine++;
        }

        foreach (string[] strings in _parsedStrings)
        {
            foreach (string s in strings)
            {
                Console.Write(s + " | ");
            }
            Console.WriteLine();
        }
        
        Console.ReadLine();
    }
    
    private SettingsPars Input()
    {
        string folder = "", file = "", rows = "";
        int[] pagination = [];
        bool flag = false;

        while (!flag)
        {
            Console.Write("папка с таблицами: ");
            folder = Console.ReadLine()!;
            Console.Clear();
            if (Directory.Exists(folder))
            {
                flag = true;
            }
        }

        flag = false;
        while (!flag)
        {
            Console.Write("название файла (с расширением): ");
            file = Console.ReadLine()!; 
            Console.Clear();
            
            if (File.Exists($"{folder}/{file}"))
            {
                flag = true;
            }
        }
        
        
        flag = false;
        while (!flag)
        {
            string p;
            Console.Write("нужные строки (формат записи \"1-10\" - с первой строки по 10, включая первую строку: ");
            p = Console.ReadLine()!;
            Console.Clear();
            string parsPag = MyRegEx.Pagination(p);
            if (parsPag != "")
            {
                string[] sPag = parsPag.Split('-');
                if (int.Parse(sPag[1]) > int.Parse(sPag[0]))
                {
                    flag = true;
                    pagination = [int.Parse(sPag[0]), int.Parse(sPag[1])];
                }
            }
        }

        Console.Write("нужные столбцы (названия): ");
        rows = Console.ReadLine()!;
        Console.Clear();
        return new SettingsPars(folder, file, pagination, rows);
    }


}
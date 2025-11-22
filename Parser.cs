

namespace Practic6;

public class Parser
{
    private readonly SettingsPars _settings;
    private readonly StreamReader _reader;
    

    public Parser()
    {
        _settings = Input();
        _reader = new StreamReader(_settings.FullPath);
        
        string[] tablesFile = MyRegEx.TablesFile(_reader.ReadLine()!, _settings.Extension);
        foreach (string tableNeed in _settings.Rows.Split(' '))
        {
            for (int i = 0; i < tablesFile.Length; i++)
            {
                if (tableNeed == tablesFile[i])
                {
                    _settings.TablesNeedNums.Add(i);
                }
            }
        }
    }
    
    
    private SettingsPars Input()
    {
        string folder = "", file = "", pagination = "", rows = "";
        bool flag = false;

        while (!flag)
        {
            folder = Console.ReadLine()!;
            if (Directory.Exists(folder))
            {
                flag = true;
            }
        }

        flag = false;
        while (!flag)
        {
            file = Console.ReadLine()!;
            if (File.Exists($"{folder}/{file}"))
            {
                flag = true;
            }
        }
        
        
        flag = false;
        while (!flag)
        {
            pagination = Console.ReadLine()!;
            string parsPag = MyRegEx.Pagination(pagination);
            if (parsPag != "")
            {
                string[] sPag = parsPag.Split('-');
                if (int.Parse(sPag[1]) > int.Parse(sPag[0]))
                {
                    flag = true;
                }
            }
        }

        rows = Console.ReadLine()!;
        return new SettingsPars(folder, file, pagination, rows);
    }

    // private bool CheckValid()
    // {
    //     string[] tablesFile = MyRegEx.TablesFile(_reader.ReadLine()!, _settings.Extension);
    //     return false;
    // }
}
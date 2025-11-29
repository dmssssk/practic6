namespace Practic6;

public record ParserOptions(string FolderPath, string Name, int[] Pagination, string Rows)
{
    public string FolderPath { get; } = FolderPath;
    public string Name { get; } = Name;
    public string Rows { get; } = Rows;
    public int[] Pagination { get; } = Pagination;
    
    public string FullPath { get; } = FolderPath + "\\" + Name;
    public List<int> TablesToOutput { get; } = new();
    public char Separator { get; } = Path.GetExtension(Name) == "csv" ? ';' : '\t';
}



public class Parser
{
    private readonly ParserOptions _settings;
    private readonly StreamReader _reader;
    private readonly List<string[]> _parsedStrings = new();
    
    public Parser(ParserOptions settings)
    {
        _settings = settings;
        _reader = new StreamReader(_settings.FullPath);
        
    }

    private void Initialize()
    {
        string[] tablesFile = ParserRegExs.TablesFile(_reader.ReadLine()!, _settings.Separator);

        if (_settings.Rows != "*")
        {
            foreach (string tableNeed in _settings.Rows.Split(' '))
            {
                for (int i = 0; i < tablesFile.Length; i++)
                {
                    if (tableNeed.ToLower() == tablesFile[i].ToLower())
                    {
                        _settings.TablesToOutput.Add(i);
                    }
                }
            }
        }

        else
        {
            for (int i = 0; i < tablesFile.Length; i++)
            {
                _settings.TablesToOutput.Add(i);
            }
        }
        
        if (_settings.TablesToOutput.Count == 0)
        {
            throw new Exception("неверно введены столбцы, попробуйте еще раз");
        }
        
        string toAdd = "";

        for (int i = 0; i < tablesFile.Length; i++)
        {
            if (_settings.TablesToOutput.Contains(i))
            {
                toAdd += tablesFile[i] + ' ';
            }
        }
        toAdd = toAdd.Trim();

        _parsedStrings.Add(toAdd.Split(' '));
    }

    public void Parsing()
    {
        Initialize();
        
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
                    if (_settings.TablesToOutput.Contains(i))
                    {
                        toAdd += sLine[i] + '|';
                    }
                }
                toAdd = toAdd.Trim();
                _parsedStrings.Add(toAdd.Substring(0, toAdd.Length-1).Split('|'));
            }

            numLine++;
        }

        Output.DrawTable(_settings.TablesToOutput.Count, _parsedStrings);
    }

}
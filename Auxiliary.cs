using System.Text.RegularExpressions;

namespace Practic6;

public static class MyRegEx
{
    public static string Pagination(string pag)
    {
        return new Regex(@"[1-9]\d*-[1-9]\d*").Match(pag).ToString();
    }
    
    public static string[] TablesFile(string firstString, char sep)
    {
        return new Regex(@$"(\w+)|(\w+{sep}\w+)+").Matches(firstString).Select(m => m.Value).ToArray();
    }
}


class SettingsPars(string folder, string name, int[] pagination, string rows)
{
    public string FolderPath { get; } = folder;
    public string Name { get; } = name;
    public int[] Pagination { get; } = pagination;
    public string Rows { get; } = rows;
    public string Extension { get;  } = name.Split('.').Last();
    public string FullPath { get;  } = folder + "\\" + name;
    public List<int> TablesNeedNums = new List<int>();
    public char Separator = name.Split('.').Last() == "csv" ? ';' : '\t';
}

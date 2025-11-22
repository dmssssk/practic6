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
    public List<int> TablesNeedNums { get; } = new List<int>();
    public char Separator { get; } = name.Split('.').Last() == "csv" ? ';' : '\t';
}


public static class MyOverrides
{
    public static string Repeat(this string str, int count)
    {
        string a = "";
        for (int i = 0; i < count; i++)
        {
            a += str;
        }
        return a;
    }
}
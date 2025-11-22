using System.IO.Enumeration;
using System.Text.RegularExpressions;

namespace Practic6;

public static class MyRegEx
{
    // public static string FileName(string filename)
    // {
    //     return new Regex("[a-z, A-Z, 0-9]{0,30}.[t, c]sv").Match(filename).ToString();
    // }

    public static string Pagination(string pag)
    {
        return new Regex(@"[1-9]\d+-[1-9]\d+").Match(pag).ToString();
    }

    // public static string Rows(string rows)
    // {
    //     return new Regex(@"\w+[;,\t]]")
    // }

    public static string[] TablesFile(string firstString, string extension)
    {
        if (extension == ".csv")
        {
            return new Regex(@"(\w+)|(\w+;\w+)+").Matches(firstString).Select(m => m.Value).ToArray();
        }
        return new Regex(@"(\w+)|(\w+\t\w+)+").Matches(firstString).Select(m => m.Value).ToArray();
    }
}


class SettingsPars(string folder, string name, string pagination, string rows)
{
    public string FolderPath { get; } = folder;
    public string Name { get; } = name;
    public string Pagination { get; } = pagination;
    public string Rows { get; } = rows;
    public string Extension { get;  } = name.Split('.').Last();
    public string FullPath { get;  } = folder + "\\" + name;
    public List<int> TablesNeedNums = new List<int>();
}
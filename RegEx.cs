namespace Practic6;
using System.Text.RegularExpressions;


public static class ParserRegExs
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
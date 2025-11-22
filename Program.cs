namespace Practic6;

public static class Program
{
    public static void Main()
    {
        string s = "GYDFds\tfsdfs";
        
        // Console.WriteLine(MyRegEx.TablesFile(s));

        foreach (string ss in MyRegEx.TablesFile(s, ".tsv"))
        {
            Console.WriteLine(ss);
        }
        
        StreamReader gg = new StreamReader("C:\\Users\\dimai\\RiderProjects\\Practic6\\Practic6\\testFiles\\contractors.csv");
        string sss;
        for(int i = 0; i < 1000; i++)
        {
            sss = gg.ReadLine();
            if (sss != null)
            {
                Console.WriteLine(sss);
            }
        }



    }
}
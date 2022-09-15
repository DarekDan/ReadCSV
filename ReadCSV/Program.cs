using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

namespace ReadCSV
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = "\n",
                Delimiter = "\t",
                Encoding = Encoding.UTF8,
                HasHeaderRecord = true,
                Quote = '\"'
            };
            using var reader = new StreamReader(@"Path-to-file");
            using var csv = new CsvReader(reader, config);
            var cnt = 0L;
            var lineNumber = 1L;
            csv.Read();
            var headerRead = csv.ReadHeader();
            while (csv.Read())
            {
                lineNumber++;
                for (int i = 0; i < csv.HeaderRecord.Length; i++)
                {
                    var col = csv.GetField(i);
                    if (col.ToCharArray().Any(a => a < 32))
                    {
                        cnt++;
                        Console.WriteLine($"Line {lineNumber}, {i+1} column");
                    }
                }
            }

            Console.WriteLine($"Found {cnt} non-printable characters");
        }
    }
}
namespace BookstoreCatalogue
{
    using Logs.Data;
    using Logs.Models;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;

    enum ShowLog
    {
        InConsole,
        InTextFile
    }

    class MainLogic
    {
        static void Main(string[] args)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            bool resetBookstore = true;
            bool resetLog = true;
            ShowLog destination = ShowLog.InTextFile;
            LogsContext log = new LogsContext();                
            DB dbManager = new DB(resetBookstore, resetLog,log);

            //Problem 3.	Simple Books Import from XML File
            String inputPath = "../../XML/simple-books.xml";
            dbManager.ProcessBooks(inputPath, false);

            //Problem 4.	Complex Books Import from XML File
            inputPath = "../../XML/complex-books.xml";
            dbManager.ProcessBooks(inputPath, true);

            //Problem 5.	Simple Search for Books
            inputPath = "../../XML/simple-query1.xml";
            string output = dbManager.ExecuteSimpleQuery(inputPath);
            Console.WriteLine(output);

            //Problem 6.	Search for Reviews
            inputPath = "../../XML/reviews-queries.xml";
            String outputPath = "../../XML/reviews-search-results.xml";
        dbManager.ExecuteReviewsQueries(inputPath, outputPath);

            //Problem 7.	Search Logs (Code First)
            switch (destination)
            {
                case ShowLog.InConsole:
                    foreach (Log item in log.Logs)
                        Console.WriteLine("{0} - {1}", item.LogDate, item.QueryXml);
                    break;
                case ShowLog.InTextFile:
                    using (StreamWriter writer = new StreamWriter(
                        "../../LogOutput.txt", false, Encoding.UTF8))
                    {
                        foreach (Log item in log.Logs)
                            writer.Write("{0}\n{1}\n{2}\n{3}\n",
                                item.Id, item.LogDate, item.QueryXml, new String('-', 20));
                    }
                    break;
            }
        }
    }
}
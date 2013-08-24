using NLog;
using System;
using System.Linq;

class LogentriesDemo
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    
    static void Main()
    {
        logger.Info("Project started.");
        try
        {
            Console.Write("Enter your age: ");
            int age = int.Parse(Console.ReadLine());
            Console.WriteLine("After an year you will be {0} years old", age + 1);
        }
        catch (Exception ex)
        {
            logger.Error(ex);
            throw ex;
        }
        logger.Info("Project stopped.");
    }
}

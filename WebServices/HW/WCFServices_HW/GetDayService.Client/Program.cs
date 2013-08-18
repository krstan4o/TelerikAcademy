using System;
using GetDayService.Client.DayOfWeekReference;
namespace GetDayService.Client
{
    class Program
    {
        static void Main()
        {
            ServiceClient client = new ServiceClient();
            string dayOfWeek = client.GetDayOfWeek(DateTime.Now);
            Console.WriteLine(dayOfWeek);
        }
    }
}

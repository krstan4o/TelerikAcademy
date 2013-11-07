using System;
using GSMCore;

namespace GSMTest
{
    class GSMCallHistoryTest    //Task 12 - Test the CallHistory of a gsm
    {
        static void Main()
        {
            GSM NokiaN92 = new GSM("Nokia", "N82", 190, "Pesho Gosho", new Battery(Battery.Type.NiCD, 150, 13.5), new Display(2.5, 2000000000));

            Console.WriteLine("Add some calls...");
            NokiaN92.AddCall("0892023111", 250, 2013, 2, 25, 21, 9, 3);
            NokiaN92.AddCall("0892023111", 50, 2013, 2, 22, 12, 2, 30);
            NokiaN92.AddCall("0892023111", 110, 2013, 1, 7, 18, 3, 12);
            Console.WriteLine("Print the calls:");
            foreach (var item in NokiaN92.CallHistory)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Calculate price of calls if the price per minute is 0.37: {0}", NokiaN92.TotalCallsPrice((decimal)0.37));

            Console.WriteLine("Deleting longest call duration  with the method .DeleteCall(Call)");
            NokiaN92.DeleteCall(NokiaN92.CallHistory[0]);
            Console.WriteLine("Calculate price of calls if the price per minute is 0.37: {0}", NokiaN92.TotalCallsPrice((decimal)0.37));

            Console.WriteLine("Clear the CallHistory and print it:");
            NokiaN92.ClearCallHistory();
            foreach (var item in NokiaN92.CallHistory)
            {
                Console.WriteLine(item);
            }
        }
    }
}

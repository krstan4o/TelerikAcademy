using System;
using GSMCore;
using System.Collections.Generic;

namespace GSMTest
{
    public class GSMTest    //Task 7 - Write class GSMTest to test the GSM class...
    {
        static void Main()
        {
            GSM[] gsms = new GSM[3]     //Array of GSMs
            {
                new GSM("SonyEricson","T100"),
                new GSM("Nokia","N92",130,"krstancho",new Battery(),new Display(2.2,256000)),
                new GSM("Nokia","N82",190,"Pesho Gosho",new Battery(Battery.Type.NiCD,150,13.5),new Display(2.5,2000000000))
            };
          
            //Printing...
            foreach (GSM gsm in gsms)
            {
                Console.WriteLine(gsm);
            }
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Static field IPhone4S:");
            Console.WriteLine(GSM.IPhone4S);
        }
    }
}

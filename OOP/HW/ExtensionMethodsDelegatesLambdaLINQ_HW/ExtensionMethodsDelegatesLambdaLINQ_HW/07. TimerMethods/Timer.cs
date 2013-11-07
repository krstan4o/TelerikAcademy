using System;
using System.Threading;

class Timer
{
    static void SetInterval(Action method, int secounds)
    {
        while (true)
        {
            Thread.Sleep(secounds * 1000);

            method();
        }
    }

    static void Main()
    {
        SetInterval(new Action(() =>
            Console.WriteLine("OMFG the same message over and over again WTF")
        ), 1);
    }
}
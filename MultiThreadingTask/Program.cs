using System;
using System.Threading;

class Program
{
    static AutoResetEvent numberTurn = new AutoResetEvent(true);  
    static AutoResetEvent alphaTurn = new AutoResetEvent(false); 

    static void Main()
    {
        Console.WriteLine("Main Thread Started");

        Thread t1 = new Thread(() => PrintNumbers("Thread 1"));
        Thread t2 = new Thread(() => PrintAlphabets("Thread 2"));

        t1.Start();
        t2.Start();

        t1.Join();
        t2.Join();

        Console.WriteLine("Main Thread Completed");
    }

    static void PrintNumbers(string label)
    {
        for (int i = 1; i <= 5; i++)
        {
            numberTurn.WaitOne(); 
            Console.WriteLine($"[{label}] Number: {i}");
            Thread.Sleep(150);     
            alphaTurn.Set();       
        }
    }

    static void PrintAlphabets(string label)
    {
        for (int i = 0; i < 5; i++)
        {
            alphaTurn.WaitOne(); 
            char ch = (char)('A' + i);
            Console.WriteLine($"[{label}] Alphabet: {ch}");
            Thread.Sleep(150);    
            numberTurn.Set();     
        }
    }
}
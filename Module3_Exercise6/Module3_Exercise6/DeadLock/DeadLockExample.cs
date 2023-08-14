using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module3_Exercise6.DeadLock;

public static class DeadlockExample
{
    private static object lock1 = new object();
    private static object lock2 = new object();

    public static void Usage()
    {
        Thread thread1 = new Thread(DoWork1);
        Thread thread2 = new Thread(DoWork2);

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();

        Console.WriteLine("Both threads completed.");
    }

    private static void DoWork1()
    {
        Thread.Sleep(10);
        lock (lock1) // Thread 1 take lock1
        {
            Console.WriteLine("Thread 1: Holding lock1...");

            // Introducing a delay to increase the chances of deadlock
            Thread.Sleep(100);

            Console.WriteLine("Thread 1: Waiting for lock2...");

            lock (lock2)
            {
                Console.WriteLine("Thread 1: Acquired lock2.");
            }
        }
    }

    private static void DoWork2()
    {
        lock (lock2) // Thread 2 take lock2
        {
            Console.WriteLine("Thread 2: Holding lock2...");

            // Introducing a delay to increase the chances of deadlock
            Thread.Sleep(100);

            Console.WriteLine("Thread 2: Waiting for lock1...");

            lock (lock1)
            {
                Console.WriteLine("Thread 2: Acquired lock1.");
            }
        }
    }
}

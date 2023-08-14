using Module3_Exercise6.LockExample;
using Module3_Exercise6.SemaphoreSlimExample;
using System.Threading.Channels;
using Module3_Exercise6.DeadLock;

namespace Module3_Exercise6;

internal class Program
{
    static async Task Main(string[] args)
    {
        DeadlockExample.Usage();

        await PrinterManagerExample.UsageAsync();
        Console.Read();

        //var a = new BankAccount(10);
        //var b = new BankAccount(20);

        //Type type1 = a.GetType();
        //Type type2 = b.GetType();

        //if (type1 == type2)
        //{
        //    Console.WriteLine("Equals");
        //}

        //await CancellationTokenExample1();
        //await TaskCompletionExample2();

        //await CounterExample.Example();
        //Console.WriteLine("Hello, World!");
        //Console.Read();
    }

    public static async Task CancellationTokenExample1()
    {
        var source = new CancellationTokenSource();
        CancellationToken token = source.Token;

        Console.CancelKeyPress += (o, ev) =>
        {
            source.Cancel();
        };

        token.Register(() => Console.WriteLine("I am invoked when my source is cancelled1"));
        token.Register(() => Console.WriteLine("I am invoked when my source is cancelled2"));

        Task.Run(async () =>
        {
            await Task.Delay(5000);
            await Console.Out.WriteLineAsync("Cancel Request");
            source.Cancel();
        });

        while (!token.IsCancellationRequested)
        {
            Console.WriteLine("Logic");
            await Task.Delay(500);
        }
    }

    public static async Task TaskCompletionExample2()
    {
        TaskCompletionSource<string> taskSource = new TaskCompletionSource<string>();

        Console.CancelKeyPress += (o, ev) =>
        {
            taskSource.SetResult("Hello from Event.");
        };

        Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        Console.WriteLine("I am stopped.");

        string msg = await taskSource.Task;
        
        Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        Console.WriteLine("I continue work");
        Console.WriteLine(msg);
    }
}

using System.Diagnostics.Metrics;

namespace Module3_Exercise6.LockExample;

internal class CounterExample
{
    private static int _counter = 0;
    private static readonly object _objectSync = new object();

    public static Task Example()
    {
        Task.Run(() => Increase());
        Task.Run(() => Increase());

        return Task.CompletedTask;
    }

    private static void Increase()
    {
        for (int i = 0; i < 1000000; i++)
        {
            lock (_objectSync)
            {
                _counter++;
            }

            Interlocked.Increment(ref _counter); // alternative
        }

        Console.WriteLine("The counter is " + _counter);
    }
}

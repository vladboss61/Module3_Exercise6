using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module3_Exercise6.SemaphoreSlimExample;

internal static class PrinterManagerExample
{
    public static async Task UsageAsync()
    {
        // SemaphoreSlim is a synchronization primitive in C# that allows controlling access to a shared
        // resource by a certain number of threads at a time.
        // It's useful when you want to limit the concurrent access to a resource, like
        // a connection pool, database connections, or any scenario
        // where you need to control the degree of parallelism.


        //In this example, even though there are 5 tasks trying to print documents,
        //only 2 tasks will execute concurrently due to the semaphore limit set to 2 in the PrinterManager
        //constructor. The other tasks will wait until permits become available.

        PrinterManager printerManager = new PrinterManager(printerCount: 2); // Limit to 2 printers

        var tasks = new[]
        {
            printerManager.PrintDocumentAsync("Document 1"),
            printerManager.PrintDocumentAsync("Document 2"),
            printerManager.PrintDocumentAsync("Document 3"),
            printerManager.PrintDocumentAsync("Document 4"),
            printerManager.PrintDocumentAsync("Document 5")
        };

        await Task.WhenAll(tasks);
    }
}

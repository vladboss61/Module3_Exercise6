namespace Module3_Exercise6.SemaphoreSlimExample;

public sealed class PrinterManager
{
    private readonly SemaphoreSlim _printerSemaphore;

    public PrinterManager(int printerCount)
    {
        _printerSemaphore = new SemaphoreSlim(printerCount);
    }

    public async Task PrintDocumentAsync(string documentName)
    {
        Console.WriteLine($"Waiting to print {documentName}");

        await _printerSemaphore.WaitAsync(); // Acquire a semaphore permit

        try
        {
            Console.WriteLine($"Printing {documentName}");
            await Task.Delay(TimeSpan.FromSeconds(2)); // Simulate printing process
            Console.WriteLine($"Finished printing {documentName}");
        }
        finally
        {
            _printerSemaphore.Release(); // Release the semaphore permit
        }
    }
}

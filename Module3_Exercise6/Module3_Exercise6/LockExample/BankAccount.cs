
namespace Module3_Exercise6.LockExample;

public sealed class BankAccount
{
    private readonly object _objectSync = new object(); // string / Type - maybe runtime error | struct - compilation error
    private readonly Mutex mutex = new Mutex(false, "myMutex"); // string / Type - maybe runtime error | struct - compilation error
    private decimal _balance;

    public BankAccount(decimal initialBalance)
    {
        _balance = initialBalance;
    }

    public decimal Balance
    {
        get
        {
            lock (_objectSync) // Enter the critical section
            {
                return _balance;
            } // Exit the critical section
        }
    }

    public void Withdraw(decimal amount)
    {
        // Monitor, Mutex, Semaphore, SemaphoreSlip, AutoResetEvent, ManualResetEvent
        try
        {
            mutex.WaitOne();
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero.");
            }

            if (_balance >= amount)
            {
                _balance -= amount;
                Console.WriteLine($"Withdrawn: {amount}, New Balance: {_balance}");
            }
            else
            {
                Console.WriteLine("Insufficient _balance for withdrawal.");
            }
        }
        finally
        {
            mutex.ReleaseMutex();
        }

        //lock (_objectSync) // Enter the critical section
        //{
        //    if (amount <= 0)
        //    {
        //        throw new ArgumentException("Amount must be greater than zero.");
        //    }

        //    if (_balance >= amount)
        //    {
        //        _balance -= amount;
        //        Console.WriteLine($"Withdrawn: {amount}, New Balance: {_balance}");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Insufficient _balance for withdrawal.");
        //    }
        //} // Exit the critical section
    }

    public void Deposit(decimal amount)
    {
        lock (_objectSync) // Enter the critical section
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero.");
            }

            _balance += amount;
            Console.WriteLine($"Deposit: {amount}, New Balance: {_balance}");
        } // Exit the critical section
    }
}


namespace Module3_Exercise6.LockExample;

internal static class BankUsage
{
    public static void Usage()
    {
        BankAccount account = new BankAccount(1000);

        Thread thread1 = new Thread(() =>
        {
            for (int i = 0; i < 20; i++)
            {
                account.Deposit(500);
            }
        });

        Thread thread2 = new Thread(() =>
        {
            for (int i = 0; i < 20; i++)
            {
                account.Withdraw(250);
            }
        });

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();

        Console.WriteLine($"Final Balance: {account.Balance}");
    }
}

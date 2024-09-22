using static System.Threading.Thread;

internal class ThreadSafetyUsingSemaphoreSlim
{
    private static readonly SemaphoreSlim Semaphore = new(3);  // max 3 client
    public static async Task ShowGoodExampleAsync()
    {
        var tasks = new List<Task>();

        for (var i = 1; i <= 10; i++) // 10 clients attempting to access db
        {
            var clientId = i;
            tasks.Add(Task.Run(async () => await AccessDatabase(clientId)));
        }

        await Task.WhenAll(tasks); // wait for all tasks to complete
    }

    private static async Task AccessDatabase(int clientId)
    {
        var threadId = CurrentThread.ManagedThreadId;
        Console.WriteLine($"Client {clientId} | (Thread {threadId}) is waiting for db connection, Semaphore count: {Semaphore.CurrentCount}");

        await Semaphore.WaitAsync(); // wait for available connection
        threadId = CurrentThread.ManagedThreadId;
        Console.WriteLine($"Client {clientId} | (Thread {threadId}) acquired db connection, Semaphore count: {Semaphore.CurrentCount}");

        try
        {
            await Task.Delay(3000);
            Console.WriteLine($"Client {clientId} | (Thread {threadId}) finished working and disconnected");
        }
        finally
        {
            Semaphore.Release(); // release connection for another client
            Console.WriteLine($"Client {clientId} | (Thread {threadId}) released db connection, Semaphore count: {Semaphore.CurrentCount}");
        }
    }
}
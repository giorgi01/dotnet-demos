using static System.Threading.Thread;

namespace SynchronizationPlayground.ThreadSafety
{
    internal class ThreadSafetyUsingLock
    {
        private static int _counter;   // shared resource
        private static readonly object LockObject = new();

        // Safe increment with lock
        public static void SafeIncrement()
        {
            lock (LockObject)
            {
                _counter++;
                Sleep(10);
                Console.WriteLine($"Counter incremented to: {_counter} by thread {CurrentThread.ManagedThreadId}");
            }
        }

        // Increment without lock (unsafe)
        public static void Increment()
        {
            _counter++;
            Sleep(10);
            Console.WriteLine($"Counter incremented to: {_counter} by thread {CurrentThread.ManagedThreadId}");
        }

        public static void ShowMeGoodExample()
        {
            _counter = 0;
            var threads = new Thread[10];

            for (var i = 0; i < 10; i++)
            {
                threads[i] = new Thread(SafeIncrement) { Name = $"Thread_{i + 1}" };
                threads[i].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();  // wait for all threads to finish, then join main thread
            }

            Console.WriteLine($"Final counter value with lock: {_counter}");
        }

        public static void ShowMeBadExample()
        {
            _counter = 0;
            var threads = new Thread[10];

            for (var i = 0; i < 10; i++)
            {
                threads[i] = new Thread(Increment) { Name = $"Thread_{i + 1}" };
                threads[i].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine($"Final counter value without lock: {_counter}");
        }
    }
}

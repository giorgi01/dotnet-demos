using Synchronization.ThreadSafety;

namespace Synchronization
{
    internal class Program
    {
        static async Task Main()
        {
            ThreadSafetyUsingLock.ShowMeGoodExample();
            ThreadSafetyUsingLock.ShowMeBadExample();

            await ThreadSafetyUsingSemaphoreSlim.ShowGoodExampleAsync();


            Console.ReadKey();
        }
    }
}

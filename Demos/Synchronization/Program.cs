using Synchronization.Channels;
using Synchronization.Signaling;
using Synchronization.ThreadSafety;

namespace Synchronization
{
    internal class Program
    {
        static async Task Main()
        {
            //ThreadSafetyUsingLock.ShowMeGoodExample();
            //ThreadSafetyUsingLock.ShowMeBadExample();

            //await ThreadSafetyUsingSemaphoreSlim.ShowGoodExampleAsync();


            //await ChannelDemo.ShowMeGoodExampleWithChannel();
            //await ChannelDemo.ShowMeBadExampleWithoutChannel()

            await SignalingDemo.ShowGoodSignalingExample();

            await SignalingDemo.ShowBadSignalingExample();

            Console.ReadKey();
        }
    }
}

using System.Threading.Channels;

namespace Synchronization.Channels
{
    internal class ChannelDemo
    {
        public static async Task ShowMeGoodExampleWithChannel()
        {
            var channel = Channel.CreateUnbounded<int>();

            var producer = Task.Run(async () =>
            {
                for (var i = 0; i < 10; i++)
                {
                    await channel.Writer.WriteAsync(i);
                    Console.WriteLine($"produced: {i}");
                }
                channel.Writer.Complete();
            });

            var consumer = Task.Run(async () =>
            {
                await foreach (var item in channel.Reader.ReadAllAsync())
                {
                    Console.WriteLine($"consumed: {item}");
                }
            });

            await Task.WhenAll(producer, consumer);
        }

        private static Queue<int> _queue = new();

        public static async Task ShowMeBadExampleWithoutChannel()
        {
            var producer = Task.Run(() =>
            {
                for (var i = 0; i < 10; i++)
                {
                    // there is no lock
                    _queue.Enqueue(i);
                    Console.WriteLine($"produced: {i}");
                }
            });

            var consumer = Task.Run(() =>
            {
                while (_queue.Count > 0 || !producer.IsCompleted)
                {
                    if (_queue.Count > 0)
                    {
                        // there is no lock too, so race condition may occur
                        var item = _queue.Dequeue();
                        Console.WriteLine($"consumed: {item}");
                    }
                }
            });

            await Task.WhenAll(producer, consumer);
        }

    }
}

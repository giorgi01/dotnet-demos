namespace Synchronization.Signaling
{
    internal class SignalingDemo
    {
        private static readonly object Lock = new();
        private static readonly Queue<int> Queue = new();

        private static bool _isAddingCompleted;

        public static async Task ShowGoodSignalingExample()
        {
            var producer = Task.Run(() =>
            {
                for (var i = 0; i < 10; i++)
                {
                    lock (Lock)
                    {
                        Queue.Enqueue(i);
                        Console.WriteLine($"produced: {i}");
                        Monitor.Pulse(Lock);
                    }
                    Thread.Sleep(100);
                }

                lock (Lock)
                {
                    _isAddingCompleted = true;
                    Monitor.Pulse(Lock);
                }
            });

            var consumer = Task.Run(() =>
            {
                while (true)
                {
                    int item;
                    lock (Lock)
                    {
                        while (Queue.Count == 0 && !_isAddingCompleted)
                        {
                            Monitor.Wait(Lock);
                        }

                        if (Queue.Count == 0 && _isAddingCompleted)
                        {
                            break;
                        }

                        item = Queue.Dequeue();
                    }
                    Console.WriteLine($"consumed: {item}");
                }
            });

            await Task.WhenAll(producer, consumer);
        }

        public static async Task ShowBadSignalingExample()
        {
            var producer = Task.Run(() =>
            {
                for (var i = 0; i < 10; i++)
                {
                    // no lock
                    Queue.Enqueue(i);
                    Console.WriteLine($"produced: {i}");
                }
            });

            var consumer = Task.Run(() =>
            {
                // check if the queue has data
                while (true)
                {
                    if (Queue.Count > 0)
                    {
                        // no lock
                        var item = Queue.Dequeue();
                        Console.WriteLine($"consumed: {item}");
                    }
                    else if (producer.IsCompleted)
                    {
                        break;
                    }
                }
            });

            await Task.WhenAll(producer, consumer);
        }


    }
}

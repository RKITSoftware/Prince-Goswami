using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumerLibrary
{
    public class ProducerConsumer
    {
        private readonly BlockingCollection<int> _dataQueue;
        private readonly int _boundedCapacity;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public ProducerConsumer(int boundedCapacity = 5)
        {
            _boundedCapacity = boundedCapacity;
            _dataQueue = new BlockingCollection<int>(_boundedCapacity);
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void StartConsuming()
        {
            Task.Run(() => Consumer(_cancellationTokenSource.Token));
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
            _dataQueue.CompleteAdding(); // Indicate that no more data will be added
        }

        public void AddData(int data)
        {
            if (!_dataQueue.IsAddingCompleted)
            {
                _dataQueue.Add(data);
                Console.WriteLine($"Produced: {data}"); // Print produced data
            }
        }

        private void Consumer(CancellationToken cancellationToken)
        {
            try
            {
                foreach (var data in _dataQueue.GetConsumingEnumerable(cancellationToken))
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    Thread.Sleep(1000); // Simulate data processing
                    CustomConsumer customConsumer = new CustomConsumer();
                    Console.WriteLine($"{customConsumer} - Data: {data}"); // Print custom consumer message
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Consumer operation was canceled.");
            }
        }
    }

    /// <summary> 
    /// Represents a custom consumer object
    /// </summary>
    public class CustomConsumer
    {
        // Override ToString to provide a custom string representation
        public override string ToString()
        {
            return "CustomConsumer: Data Processed!"; // Custom consumer message
        }
    }
}

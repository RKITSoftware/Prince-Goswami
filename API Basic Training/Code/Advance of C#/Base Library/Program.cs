using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static BlockingCollection<int> dataQueue = new BlockingCollection<int>(boundedCapacity: 5);

    static void Main()
    {
        Task.Run(() => Producer());
        Task.Run(() => Consumer());

        Console.ReadLine();
    }

    static void Producer()
    {
        for (int i = 0; i < 10; i++)
        {
            Thread.Sleep(500); // Simulate data production
            dataQueue.Add(i);
            Console.WriteLine($"Produced: {i}");
        }

        dataQueue.CompleteAdding();
    }

    static void Consumer()
    {
        foreach (var data in dataQueue.GetConsumingEnumerable())
        {
            Thread.Sleep(1000); // Simulate data processing
            CustomConsumer customConsumer = new CustomConsumer();
            Console.WriteLine(customConsumer); // Using custom ToString method
        }
    }
}

class CustomConsumer
{
    // Override ToString to provide a custom string representation
    public override string ToString()
    {
        return "CustomConsumer: Data Processed!";
    }
}

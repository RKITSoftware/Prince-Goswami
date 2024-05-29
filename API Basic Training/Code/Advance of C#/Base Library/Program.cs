using System;
using System.Threading.Tasks;
using ProducerConsumerLibrary;

class Program
{
    static void Main()
    {
        ProducerConsumer producerConsumer = new ProducerConsumer();

        producerConsumer.StartConsuming();

        Task.Run(() => ProduceData(producerConsumer));

        Console.WriteLine("Press Enter to stop...");
        Console.ReadLine();

        producerConsumer.Stop();
    }

    static void ProduceData(ProducerConsumer producerConsumer)
    {
        for (int i = 0; i < 10; i++)
        {
            Task.Delay(500).Wait(); // Simulate data production
            producerConsumer.AddData(i);
        }
    }
}

using System;
using System.Collections;

class NonGenericDemo
{
    static void Main()
    {
        ArrayListExample();
        HashtableExample();
        QueueExample();
        StackExample();
    }

    /// <summary>
    /// Demonstrates the usage of ArrayList.
    /// </summary>
    static void ArrayListExample()
    {
        Console.WriteLine("ArrayList Example:");

        // Creating an ArrayList
        ArrayList arrayList = new ArrayList { 1, "two", 3.0, true };

        // Adding elements to ArrayList
        arrayList.Add("four");
        arrayList.Insert(1, "new");

        // Accessing elements in ArrayList
        Console.Write("ArrayList elements: ");
        foreach (var item in arrayList)
        {
            Console.Write(item + " ");
        }

        Console.WriteLine("\n");
    }

    /// <summary>
    /// Demonstrates the usage of Hashtable.
    /// </summary>
    static void HashtableExample()
    {
        Console.WriteLine("Hashtable Example:");

        // Creating a Hashtable
        Hashtable hashtable = new Hashtable
        {
            { "key1", "value1" },
            { "key2", "value2" },
            { "key3", "value3" }
        };

        // Accessing elements in Hashtable
        Console.WriteLine("Value for key2: " + hashtable["key2"]);

        Console.WriteLine("\n");
    }

    /// <summary>
    /// Demonstrates the usage of Queue.
    /// </summary>
    static void QueueExample()
    {
        Console.WriteLine("Queue Example:");

        // Creating a Queue
        Queue queue = new Queue();
        queue.Enqueue("first");
        queue.Enqueue("second");
        queue.Enqueue("third");

        // Dequeue elements from Queue
        Console.Write("Dequeue elements: ");
        while (queue.Count > 0)
        {
            Console.Write(queue.Dequeue() + " ");
        }

        Console.WriteLine("\n");
    }

    /// <summary>
    /// Demonstrates the usage of Stack.
    /// </summary>
    static void StackExample()
    {
        Console.WriteLine("Stack Example:");

        // Creating a Stack
        Stack stack = new Stack();
        stack.Push("one");
        stack.Push("two");
        stack.Push("three");

        // Pop elements from Stack
        Console.Write("Pop elements: ");
        while (stack.Count > 0)
        {
            Console.Write(stack.Pop() + " ");
        }

        Console.WriteLine("\n");
    }
}

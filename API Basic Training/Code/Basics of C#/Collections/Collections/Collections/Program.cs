using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        // Uncomment the regions you want to test

        #region ListExample
        ListExample();
        #endregion

        #region DictionaryExample
        DictionaryExample();
        #endregion

        #region HashSetExample
        HashSetExample();
        #endregion

        #region QueueExample
        QueueExample();
        #endregion

        #region StackExample
        StackExample();
        #endregion

        Console.ReadLine();
    }

    #region ListExample
    static void ListExample()
    {
        Console.WriteLine("List Example:");

        // Creating a List 
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

        // Adding elements to the List
        numbers.Add(6);
        numbers.AddRange(new int[] { 7, 8, 9 });

        // Removing elements from the List
        numbers.Remove(3);
        numbers.RemoveAt(0);

        // Accessing elements in the List
        foreach (var number in numbers)
        {
            Console.Write(number + " ");
        }

        Console.WriteLine();
    }
    #endregion

    #region DictionaryExample
    static void DictionaryExample()
    {
        Console.WriteLine("\nDictionary Example:");

        // Creating a Dictionary with string keys and int values
        Dictionary<string, int> ages = new Dictionary<string, int>();

        // Adding key-value pairs to the Dictionary
        ages.Add("Alice", 25);
        ages["Bob"] = 30;
        ages["Charlie"] = 22;

        // Accessing values in the Dictionary
        foreach (var person in ages)
        {
            Console.WriteLine($"{person.Key}: {person.Value} years old");
        }

        Console.WriteLine();
    }
    #endregion

    #region HashSetExample
    static void HashSetExample()
    {
        Console.WriteLine("HashSet Example:");

        // Creating a HashSet of strings
        HashSet<string> colors = new HashSet<string> { "Red", "Blue", "Green" };

        // Adding elements to the HashSet
        colors.Add("Yellow");
        colors.UnionWith(new[] { "Orange", "Purple" });

        // Removing elements from the HashSet
        colors.Remove("Blue");

        // Checking if an element exists in the HashSet
        Console.WriteLine($"Contains 'Red': {colors.Contains("Red")}");

        // Displaying elements in the HashSet
        foreach (var color in colors)
        {
            Console.Write(color + " ");
        }

        Console.WriteLine();
    }
    #endregion

    #region QueueExample
    static void QueueExample()
    {
        Console.WriteLine("\nQueue Example:");

        // Creating a Queue of strings
        Queue<string> tasks = new Queue<string>();

        // Enqueuing elements to the Queue
        tasks.Enqueue("Task 1");
        tasks.Enqueue("Task 2");
        tasks.Enqueue("Task 3");

        // Dequeuing elements from the Queue
        while (tasks.Count > 0)
        {
            Console.WriteLine($"Processing: {tasks.Dequeue()}");
        }

        Console.WriteLine();
    }
    #endregion

    #region StackExample
    static void StackExample()
    {
        Console.WriteLine("Stack Example:");

        // Creating a Stack of integers
        Stack<int> numbers = new Stack<int>();

        // Pushing elements onto the Stack
        numbers.Push(1);
        numbers.Push(2);
        numbers.Push(3);

        // Popping elements from the Stack
        while (numbers.Count > 0)
        {
            Console.WriteLine($"Popped: {numbers.Pop()}");
        }

        Console.WriteLine();
    }
    #endregion
}


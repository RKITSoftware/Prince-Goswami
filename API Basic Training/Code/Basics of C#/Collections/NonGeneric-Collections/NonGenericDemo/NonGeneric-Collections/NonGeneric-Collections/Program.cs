
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonGeneric_Collections
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Calling all methods
            ArrayListDemo();
            HashtableDemo();
            StackDemo();
            QueueDemo();
            SortedListDemo();
        }

        #region ArrayList
        /// <summary>
        ///This method demonstrates various operations on an ArrayList
        /// </summary>
        public static void ArrayListDemo()
        {
            Console.WriteLine("\nArrayList");
            ArrayList list = new ArrayList();

            // Adding elements
            list.Add("Apple");
            list.Add(10);
            list.Add(true);

            // Accessing elements
            Console.WriteLine("list[0] = "+list[0]);  // Apple
            Console.WriteLine("list[1] = "+list[1]);  // 10

            // Inserting at a specific index
            list.Insert(1, "Banana");

            // Iterating
            foreach (object item in list)
            {
                Console.WriteLine(item);
            }

            // Removing elements
            list.Remove("Apple");
            list.RemoveAt(1);
       

            // Checking existence
            Console.WriteLine(list.Contains(10));  // True

            // Copying
            ArrayList copy = (ArrayList)list.Clone();

            // Resizing
            list.Capacity = 20;  // Set capacity for efficiency
        }
        #endregion

        #region HashTable
        // Hashtable methods
        /// <summary>
        /// This method demonstrates various operations on a Hashtable, a non-generic key-value pair collection in C#.
        /// </summary>
        public static void HashtableDemo()
        {
            Console.WriteLine("\nhashTable");

            Hashtable table = new Hashtable();

            // Adding key-value pairs
            table.Add("Name", "Alice");
            table.Add("Age", 25);

            // Accessing values
            Console.WriteLine(table["Name"]);  // Alice

            // Checking for keys
            Console.WriteLine(table.ContainsKey("Age"));  // True

            // Getting keys and values
            foreach (object key in table.Keys)
            {
                Console.WriteLine(key + ": " + table[key]);
            }

            // Removing items
            table.Remove("Age");

            // Clearing
            table.Clear();
        }
        #endregion

        #region Stack
        /// <summary>
        /// This method demonstrates various operations on a Stack
        /// </summary>
        // Stack methods
        public static void StackDemo()
        {
            Console.WriteLine("\nStack");

            Stack stack = new Stack();

            // Pushing items
            stack.Push("First");
            stack.Push("Second");

            // Peeking at the top
            Console.WriteLine(stack.Peek());  // Second

            // Popping items
            Console.WriteLine(stack.Pop());  // Second

        }
        #endregion

        #region Queue
        /// <summary>
        /// This method demonstrates various operations on a Queue
        /// </summary>
        // Queue methods
        public static void QueueDemo()
        {
            Console.WriteLine("\nQueue");

            Queue queue = new Queue();

            // Enqueueing items
            queue.Enqueue("First");
            queue.Enqueue("Second");

            // Peeking at the first item
            Console.WriteLine(queue.Peek());  // First

            // Dequeueing items
            Console.WriteLine(queue.Dequeue());  // First

            // Checking for existence
            Console.WriteLine(queue.Contains("Second"));  // True
        }
        #endregion

        #region SortedList
        /// <summary>
        /// This method demonstrates various operations on a SortedList
        /// </summary>
        // SortedList methods
        public static void SortedListDemo()
        {
            Console.WriteLine("\nSortedList");

            SortedList list = new SortedList();

            // Adding sorted key-value pairs
            list.Add("A", 1);
            list.Add("C", 3);
            list.Add("B", 2);

            // Accessing values by key
            Console.WriteLine(list["C"]);  // 3

            // Removing items
            list.Remove("B");

            foreach (DictionaryEntry entry in list)
            {
                Console.WriteLine(entry.Key + ": " + entry.Value);
            }
        }
        #endregion
    }
}

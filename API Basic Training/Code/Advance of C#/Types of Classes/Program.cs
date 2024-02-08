using System;


namespace TypesOfClass
{
    class Program
    {
        static void Main()
        {
            // Creating an instance of Lion
            Lion lion = new Lion();
            lion.Display();

            // Creating an instance of Penguin
            Penguin penguin = new Penguin();
            penguin.Display();
            penguin.Swim();

            // Using methods from ZooUtility class
            ZooUtility.FeedAllAnimals();

            Console.ReadKey();
        }
    }
}
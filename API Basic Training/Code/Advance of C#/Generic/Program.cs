using System;

namespace TypesOfClass
{
    class Program
    {
        static void Main()
        {
            // Creating a ZooContainer for Lions
            ZooContainer<Lion> lionContainer = new ZooContainer<Lion>();
            lionContainer.AddAnimal(new Lion());
            lionContainer.DisplayAnimals();

            // Creating a ZooContainer for Penguins
            ZooContainer<Penguin> penguinContainer = new ZooContainer<Penguin>();
            penguinContainer.AddAnimal(new Penguin());
            penguinContainer.DisplayAnimals();

            Console.ReadKey();
        }
    }

}

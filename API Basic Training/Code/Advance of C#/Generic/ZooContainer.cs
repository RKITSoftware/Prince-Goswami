﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesOfClass;

namespace TypesOfClass
{
    /// <summary>
    /// This is a generic class representing a ZooContainer that can hold different types of _animals.
    /// </summary>
    /// <typeparam name="T">The type of animal that the container can hold.</typeparam>
    public class ZooContainer<T> where T : AbstractAnimal
    {
        private List<T> _animals;

        public ZooContainer()
        {
            _animals = new List<T>();
        }

        /// <summary>
        /// Adds an animal to the container.
        /// </summary>
        /// <param name="animal">The animal to add.</param>
        public void AddAnimal(T animal)
        {
            _animals.Add(animal);
        }

        /// <summary>
        /// Displays all _animals in the container.
        /// </summary>
        public void DisplayAnimals()
        {
            Console.WriteLine($"Animals in the container:");
            foreach (var animal in _animals)
            {
                animal.Display();
            }
        }
    }

}

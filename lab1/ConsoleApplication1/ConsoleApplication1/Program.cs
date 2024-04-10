using System;

namespace ConsoleApplication1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Animals[] animals = new Animals[3];
            animals[0] = new Cat("Fluffy", 3, 4, "Cat food", "Persian");
            animals[1] = new Dog("Buddy", 5, 4, "Dog food", "Golden Retriever", "Give paw, Voice");
            animals[2] = new Parrot("Eagle", 3, 2.5, "Feathers", "Hello, I'm good, Meow");
            foreach (var animal in animals)
            {
                if (animal is Mammal mammal)
                {
                    Console.WriteLine($"Type of food: {mammal.TypeOfFood}, Number of legs: {mammal.NumberOfLegs}.");

                    if (animal is Cat cat)
                    {
                        Console.WriteLine($"Breed: {cat.Breed}.");
                        cat.Feed();
                        cat.Feed();
                    }
                    else if (animal is Dog dog)
                    {
                        Console.WriteLine($"Breed: {dog.Breed}, Commands: {dog.Commands}.");
                        dog.AddCommand("Jump");
                        Console.WriteLine($"Commands: {dog.Commands}.");
                    }
                }
                else if (animal is Bird bird)
                {
                    Console.WriteLine($"Wingspan: {bird.Wingspan}, Type of feathers: {bird.TypeOfFeathers}.");

                    if (animal is Parrot parrot)
                    {
                        Console.WriteLine($"Words: {parrot.Words}.");
                        parrot.AddWord("Goodbye");
                        Console.WriteLine($"Words: {parrot.Words}.");
                    }
                }
                animal.Move();
                animal.MakeSound();
            }
        }
    }
}  
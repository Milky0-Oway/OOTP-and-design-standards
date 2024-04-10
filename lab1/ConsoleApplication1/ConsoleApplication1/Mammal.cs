using System;

namespace ConsoleApplication1
{
    public class Mammal : Animals
    {
        public int NumberOfLegs { get; private set; }
        public string TypeOfFood { get; private set; }
        public string Breed { get; private set; }

        protected Mammal(string name, int age, int numberOfLegs, string typeOfFood, string breed) : base(name, age)
        {
            Console.WriteLine("Mammal was created.");
            NumberOfLegs = numberOfLegs;
            TypeOfFood = typeOfFood;
            Breed = breed;
        }

        public override void Move()
        {
            Console.WriteLine($"{Name} is walking or running.");
        }

        public override void MakeSound()
        {
            Console.WriteLine($"{Name} is making a sound.");
        } 
    }
}
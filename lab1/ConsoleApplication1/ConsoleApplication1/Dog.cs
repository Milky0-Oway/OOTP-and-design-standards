using System;

namespace ConsoleApplication1
{
    public class Dog : Mammal
    {
        public string Commands { get; private set; }
        
        public Dog(string name, int age, int numberOfLegs, string typeOfFood, string breed, string commands) : base(name, age, numberOfLegs, typeOfFood,breed)
        {
            Commands = commands;
            Console.WriteLine("Dog was created.");
        }

        public override void Move()
        {
            Console.WriteLine($"{Name} is running or fetching.");
        }

        public override void MakeSound()
        {
            Console.WriteLine($"{Name} is barking.");
        }
        
        public void AddCommand(string command)
        {
            if (Commands != "")
            {
                Commands += ", " + command;
            }
            else
            {
                Commands = command;
            }
        }
    }
}
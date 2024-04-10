using System;
using System.Data.SqlTypes;

namespace ConsoleApplication1
{
    public class Cat : Mammal
    {
        private bool isFeaded = false;
        public Cat(string name, int age, int numberOfLegs, string typeOfFood, string breed) : base(name, age, numberOfLegs, typeOfFood,breed)
        {
            Console.WriteLine("Cat was created.");
        }

        public override void Move()
        {
            Console.WriteLine($"{Name} is jumping or climbing.");
        }

        public override void MakeSound()
        {
            Console.WriteLine($"{Name} is meowing.");
        }

        public void Feed()
        {
            if (isFeaded)
            {
                Console.WriteLine("I don't want to eat.");
            }
            else
            {
                isFeaded = true;
                Console.WriteLine("Thank you!");
            }
        }
    }
}
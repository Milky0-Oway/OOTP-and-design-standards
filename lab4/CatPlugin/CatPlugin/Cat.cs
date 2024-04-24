using System;
using lab2;
using System.Runtime.Serialization;

namespace lab2
{
    [DataContract]
    [Serializable]
    public class Cat : Mammal
    {
        private bool isFeaded = false;
        //[JsonConstructor]
        public Cat(string name, int age, int numberOfLegs, string breed) : base(name, age, numberOfLegs,breed)
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
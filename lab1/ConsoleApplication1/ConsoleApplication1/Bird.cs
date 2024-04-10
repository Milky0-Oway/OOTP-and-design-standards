using System;

namespace ConsoleApplication1
{
    public class Bird : Animals
    {
        public double Wingspan { get; private set; }
        public string TypeOfFeathers { get; private set; }

        public Bird(string name, int age, double wingspan, string typeOfFeathers) : base(name, age)
        {
            Wingspan = wingspan;
            TypeOfFeathers = typeOfFeathers;
            Console.WriteLine("Bird was created.");
        }

        public override void Move()
        {
            Console.WriteLine($"{Name} is flying.");
        }

        public override void MakeSound()
        {
            Console.WriteLine($"{Name} is chirping or singing.");
        }
    }
}
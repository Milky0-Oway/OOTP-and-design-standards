using System;
using lab2;
using System.Runtime.Serialization;

namespace CowPlugin
{
    [DataContract]
    [Serializable]
    public class Cow : Mammal
    {
        [DataMember]
        public string Color { get; private set; }

        public Cow(string name, int age, int numberOfLegs, string breed, string color) : base(name, age, numberOfLegs, breed)
        {
            Color = color;
            Console.WriteLine("Cow was created.");
        }

        public override void Move()
        {
            Console.WriteLine($"{Name} is walking slowly.");
        }

        public override void MakeSound()
        {
            Console.WriteLine($"{Name} is mooing.");
        }

        public void GetColor()
        { 
            Console.WriteLine($"The {Name}'s color is {Color}");
        }
    }
}
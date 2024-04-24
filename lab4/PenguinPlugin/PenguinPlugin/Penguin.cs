using lab2;
using System;
using System.Runtime.Serialization;

namespace PenguinPlugin
{
    [DataContract]
    [Serializable]
    public class Penguin : Bird
    {
        [DataMember]
        public int Distance { get; private set; }
        
        public Penguin(string name, int age, double wingspan, string typeOfFeathers, int distance) : base(name, age, wingspan, typeOfFeathers)
        {
            Distance = distance;
            Console.WriteLine("Penguin was created.");
        }
        public override void Move()
        {
            Console.WriteLine($"{Name} can slip {Distance} meters.");
        }

        public override void MakeSound()
        {
            Console.WriteLine($"{Name} can say displays.");
        }
    }
}
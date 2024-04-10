using System;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;

namespace lab2
{
    [DataContract]
    [Serializable]
    public class Mammal : Animals
    {
        [DataMember]
        public int NumberOfLegs { get; private set; }
        [DataMember]
        public string Breed { get; private set; }
      //  [JsonConstructor]
        protected Mammal(string name, int age, int numberOfLegs, string breed) : base(name, age)
        {
            Console.WriteLine("Mammal was created.");
            NumberOfLegs = numberOfLegs;
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
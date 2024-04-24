using System;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;

namespace lab2
{
    [DataContract]
    [Serializable]
    public class Bird : Animals
    {
        [DataMember]
        public double Wingspan { get; private set; }
        [DataMember]
        public string TypeOfFeathers { get; private set; }
       // [JsonConstructor]
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
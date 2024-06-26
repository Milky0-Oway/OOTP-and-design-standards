﻿using System;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;

namespace lab2
{
    [DataContract]
    [Serializable]
    public class Dog : Mammal
    {
        [DataMember]
        public string Commands { get; private set; }
        
       // [JsonConstructor]
        public Dog(string name, int age, int numberOfLegs, string breed, string commands) : base(name, age, numberOfLegs,breed)
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
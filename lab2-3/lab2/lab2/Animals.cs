using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;


namespace lab2
{
    [DataContract]
    [Serializable]
    [KnownType(typeof(Cat))]
    [KnownType(typeof(Dog))]
    [KnownType(typeof(Parrot))]
    public abstract class Animals
    {
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public int Age { get; private set; }
        //[JsonConstructor]
        protected Animals(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public abstract void Move();

        public abstract void MakeSound();
    }
}

using System;

namespace ConsoleApplication1
{
    public abstract class Animals
    {
        public string Name { get; private set; }
        public int Age { get; private set; }

        protected Animals(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public abstract void Move();

        public abstract void MakeSound();
    }
}
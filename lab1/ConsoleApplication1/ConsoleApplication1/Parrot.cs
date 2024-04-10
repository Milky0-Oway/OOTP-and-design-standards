using System;

namespace ConsoleApplication1
{
    public class Parrot : Bird
    {
        public string Words { get; private set; }
        public Parrot(string name, int age, double wingspan, string typeOfFeathers, string words) : base(name, age, wingspan, typeOfFeathers)
        {
            Words = words;
            Console.WriteLine("Parrot was created.");
        }
        public override void Move()
        {
            Console.WriteLine($"{Name} is flying or jumping.");
        }

        public override void MakeSound()
        {
            Console.WriteLine($"{Name} can say words: {Words}.");
        }

        public void AddWord(string word)
        {
            if (Words != "")
            {
                Words += ", " + word;
            }
            else
            {
                Words = word;
            }
        }
    }
}
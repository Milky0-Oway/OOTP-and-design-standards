using System.Collections.Generic;
using lab2;

namespace lab2
{
    public class CatFactory : IAnimalFactory
    {
        public Animals CreateAnimal(string name, int age, Dictionary<string, string> additionalInfo)
        {
            string breed = additionalInfo["Breed"];
            int numberOfLegs = int.Parse(additionalInfo["NumberOfLegs"]);
            return new Cat(name, age, numberOfLegs, breed);
        }
    }
}
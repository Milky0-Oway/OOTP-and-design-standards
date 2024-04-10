using System.Collections.Generic;

namespace lab2
{
    public class DogFactory : IAnimalFactory
    {
        public Animals CreateAnimal(string name, int age, Dictionary<string, string> additionalInfo)
        {
            string breed = additionalInfo["Breed"];
            int numberOfLegs = int.Parse(additionalInfo["NumberOfLegs"]);
            string commands = additionalInfo["Commands"];
            return new Dog(name, age, numberOfLegs, breed, commands);
        }
    }
}
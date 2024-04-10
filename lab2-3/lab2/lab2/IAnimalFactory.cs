using System.Collections.Generic;

namespace lab2
{
    public interface IAnimalFactory
    {
        Animals CreateAnimal(string name, int age, Dictionary<string, string> additionalInfo);
    }
}
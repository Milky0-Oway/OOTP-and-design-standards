using System.Collections.Generic;

namespace lab2
{
    public class ParrotFactory : IAnimalFactory
    {
        public Animals CreateAnimal(string name, int age, Dictionary<string, string> additionalInfo)
        {
            double wingspan = double.Parse(additionalInfo["Wingspan"]);
            string feathersType = additionalInfo["FeathersType"];
            string words = additionalInfo["Words"];
            return new Parrot(name, age, wingspan, feathersType, words);
        }
    }
}
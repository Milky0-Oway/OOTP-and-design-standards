using System;
using System.Collections.Generic;
using lab2;

namespace CatPlugin
{
    public class CowPlugin : lab2.IAnimalPlugin
    {
        private Cat cat;
        
        public string GetName()
        {
            return "Cat";
        }

        public lab2.Animals CreateAnimal(string name, int age, Dictionary<string, string> additionalInfo)
        {
            string breed = additionalInfo["Breed"];
            int numberOfLegs = int.Parse(additionalInfo["NumberOfLegs"]);
            cat = new Cat(name, age, numberOfLegs, breed);
            return cat;
        }

        public List<string> GetAdditionalFields()
        {
            List<string> fields = new List<string>();
            fields.Add("Breed");
            fields.Add("NumberOfLegs");
            return fields;
        }
        
        public List<string> GetAdditionalActions()
        {
            List<string> actions = new List<string>();
            actions.Add("Feed cat");
            return actions;
        }

        public void PerformAction(string action, string additional)
        {
            if (action == "Feed cat")
            {
                cat.Feed();
            }
        }
    }
}
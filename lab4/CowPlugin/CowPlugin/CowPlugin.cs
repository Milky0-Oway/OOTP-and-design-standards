using System;
using System.Collections.Generic;
using lab2;

namespace CowPlugin
{
    public class CowPlugin : IAnimalPlugin
    {
        private Cow cow;
        
        public string GetName()
        {
            return "Cow";
        }

        public lab2.Animals CreateAnimal(string name, int age, Dictionary<string, string> additionalInfo)
        {
            string breed = additionalInfo["Breed"];
            string color = additionalInfo["Color"];
            int numberOfLegs = int.Parse(additionalInfo["NumberOfLegs"]);
            cow = new Cow(name, age, numberOfLegs, breed, color);
            return cow;
        }

        public List<string> GetAdditionalFields()
        {
            List<string> fields = new List<string>();
            fields.Add("Breed");
            fields.Add("Color");
            fields.Add("NumberOfLegs");
            return fields;
        }
        
        public List<string> GetAdditionalActions()
        {
            List<string> actions = new List<string>();
            actions.Add("Get cow's color");
            return actions;
        }

        public void PerformAction(string action, string additional)
        {
            if (action == "Get cow's color")
            {
                cow.GetColor();
            }
        }
    }
}
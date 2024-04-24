using System;
using System.Collections.Generic;
using lab2;

namespace DogPlugin
{
    public class DogPlugin : IAnimalPlugin
    {
        private Dog dog;
        
        public string GetName()
        {
            return "Dog";
        }

        public lab2.Animals CreateAnimal(string name, int age, Dictionary<string, string> additionalInfo)
        {
            string breed = additionalInfo["Breed"];
            int numberOfLegs = int.Parse(additionalInfo["NumberOfLegs"]);
            string commands = additionalInfo["Commands"];
            dog = new Dog(name, age, numberOfLegs, breed, commands);
            return dog;
        }

        public List<string> GetAdditionalFields()
        {
            List<string> fields = new List<string>();
            fields.Add("Breed");
            fields.Add("NumberOfLegs");
            fields.Add("Commands");
            return fields;
        }
        
        public List<string> GetAdditionalActions()
        {
            List<string> actions = new List<string>();
            actions.Add("Add command");
            return actions;
        }

        public void PerformAction(string action, string additional)
        {
            if (action == "Add command")
            {
                dog.AddCommand(additional);
            }
        }
    }
}
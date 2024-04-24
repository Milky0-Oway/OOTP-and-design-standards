using System;
using System.Collections.Generic;
using lab2;

namespace ParrotPlugin
{
    public class ParrotPlugin : IAnimalPlugin
    {
        private Parrot parrot;
        
        public string GetName()
        {
            return "Parrot";
        }

        public Animals CreateAnimal(string name, int age, Dictionary<string, string> additionalInfo)
        {
            double wingspan = double.Parse(additionalInfo["Wingspan"]);
            string typeOfFeathers = additionalInfo["TypeOfFeathers"];
            string words = additionalInfo["Words"];
            parrot = new Parrot(name, age, wingspan, typeOfFeathers, words);
            return parrot;
        }

        public List<string> GetAdditionalFields()
        {
            List<string> fields = new List<string>();
            fields.Add("Wingspan");
            fields.Add("TypeOfFeathers");
            fields.Add("Words");
            return fields;
        }
        
        public List<string> GetAdditionalActions()
        {
            List<string> actions = new List<string>();
            actions.Add("Add word");
            return actions;
        }

        public void PerformAction(string action, string additional)
        {
            if (action == "Add word")
            {
                parrot.AddWord(additional);
            }
        }
    }
}
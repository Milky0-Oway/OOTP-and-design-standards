using System;
using System.Collections.Generic;
using lab2;

namespace PenguinPlugin
{
    public class PenguinPlugin : IAnimalPlugin
    {
        private Penguin penguin;
        
        public string GetName()
        {
            return "Penguin";
        }
        
        public Animals CreateAnimal(string name, int age, Dictionary<string, string> additionalInfo)
        {
            double wingspan = double.Parse(additionalInfo["Wingspan"]);
            string typeOfFeathers = additionalInfo["TypeOfFeathers"];
            int distance = int.Parse(additionalInfo["Distance"]);
            penguin = new Penguin(name, age, wingspan, typeOfFeathers, distance);
            return penguin;
        }

        public List<string> GetAdditionalFields()
        {
            List<string> fields = new List<string>();
            fields.Add("Wingspan");
            fields.Add("TypeOfFeathers");
            fields.Add("Distance");
            return fields;
        }
        
        public List<string> GetAdditionalActions()
        {
            List<string> actions = new List<string>();
            return actions;
        }

        public void PerformAction(string action, string additional)
        {

        }
    }
}
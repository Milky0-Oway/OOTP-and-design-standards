using System.Collections.Generic;

namespace lab2
{
    public interface IAnimalPlugin
    {
        string GetName();
        Animals CreateAnimal(string name, int age, Dictionary<string, string> additionalInfo);
        List<string> GetAdditionalFields();
        List<string> GetAdditionalActions();
        void PerformAction(string action, string additional);
    }
}
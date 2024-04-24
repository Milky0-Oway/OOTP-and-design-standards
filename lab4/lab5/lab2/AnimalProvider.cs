using System.Collections.Generic;
using System.Runtime.Serialization;

namespace lab2
{
    //[DataContract]
    public class AnimalProvider
    {
        //[DataMember]
        public Dictionary<string, Animals> createdAnimals = new Dictionary<string, Animals>();
        
        public Animals this[string animalName]
        {
            get { return createdAnimals.ContainsKey(animalName) ? createdAnimals[animalName] : null; }
            set { createdAnimals[animalName] = value; }
        }
    }
}
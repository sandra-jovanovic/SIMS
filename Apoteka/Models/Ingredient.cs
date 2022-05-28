using System;

namespace Apoteka.Models
{
    [Serializable]
    public class Ingredient
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Ingredient()
        {
        }

        public Ingredient(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}

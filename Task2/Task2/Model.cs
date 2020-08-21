using System.Collections.Generic;

namespace Model
{
    class Category
    {
        public int ID { get; set; }
        public List<int> ParentIDs { get; set; }
        public string Name { get; set; }
        public List<int> CategoryMembers { get; set; }
    }
    class Ingredient
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    class Composition
    {
        public int IngredientID { get; set; }
        public double Amount { get; set; }
    }
    class Recipe
    {
        public int ID { get; set; }
        public List<int> ParentIDs { get; set; }
        public string Name { get; set; }
        public List<Composition> Composition { get; set; }
        public string Description { get; set; }
        public string Instruction { get; set; }
    }
}

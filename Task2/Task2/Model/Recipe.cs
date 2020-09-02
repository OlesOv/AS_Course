using System.Collections.Generic;

namespace Model
{
    struct Composition
    {
        public int IngredientID { get; set; }
        public double Amount { get; set; }
    }
    class Recipe : MyData
    {
        public List<int> ParentIDs { get; set; }
        public List<Composition> Composition { get; set; }
        public string Description { get; set; }
        public string Instruction { get; set; }
    }
}

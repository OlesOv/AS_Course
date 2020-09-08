using System;
using System.Collections.Generic;
using Model;

namespace Task2.Controller
{
    class RecipeController : MVCConnect
    {
        public RecipeController(MVCConnect core) : base(core) { }
        public void AddRecipe()
        {
            View.ConsoleWriteLine("Name of recipe: ");
            string Name = inputController.Input();
            View.ConsoleWriteLine("Description of recipe: ");
            string Description = inputController.Input();
            View.ConsoleWriteLine("Instruction for recipe: ");
            string Instruction = inputController.Input();
            List<Composition> composition = new List<Composition>();
            if (UnitOfWork.Ingredients.Count() > 0)
            {
                View.ConsoleWriteLine("Adding ingredients. Write /done to finish");
                View.ShowIngredients(UnitOfWork.Ingredients);
                while (true)
                {
                    View.ConsoleWriteLine("Enter ID of ingredient:");
                    string t = inputController.Input();
                    if (t == "/done") break;
                    int ingID = Convert.ToInt32(t);
                    View.ConsoleWriteLine("Enter amount of this ingredient:");
                    double amount = Convert.ToDouble(inputController.Input());
                    composition.Add(new Composition { IngredientID = ingID, Amount = amount });
                }
            }
            Recipe newRec = new Recipe
            {
                Name = Name,
                ID = UnitOfWork.Recipes.Count() + 1,
                Description = Description,
                Instruction = Instruction,
                Composition = composition
            };
            if (UnitOfWork.Categories.Count() > 0)
            {
                List<int> parentIDs = new List<int>();
                View.ShowCategories(UnitOfWork.Categories, true);
                View.ConsoleWriteLine("Which categories this recipe belongs?\n(separated by commas with space)");
                string[] strpID = inputController.Input().Split(", ", StringSplitOptions.RemoveEmptyEntries);
                if (strpID.Length > 0)
                {
                    foreach (string p in strpID)
                    {
                        int t = Convert.ToInt32(p);
                        parentIDs.Add(t);
                        if (UnitOfWork.Categories[t].CategoryMembers == null) UnitOfWork.Categories[t].CategoryMembers = new List<int>();
                        UnitOfWork.Categories[t].CategoryMembers.Add(newRec.ID);
                    }
                }
                newRec.ParentIDs = parentIDs;
            }
            UnitOfWork.Recipes.Add(newRec);
            View.ConsoleWriteLine("Done!");
        }
    }
}

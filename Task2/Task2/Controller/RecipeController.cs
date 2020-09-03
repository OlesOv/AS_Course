using System;
using System.Collections.Generic;
using Model;

namespace Task2.Controller
{
    class RecipeController : MVCConnect
    {
        View View;
        public RecipeController(View view)
        {
            View = view;
        }
        public void AddRecipe()
        {
            View.ConsoleWriteLine("Name of recipe: ");
            string Name = InputController.Input();
            View.ConsoleWriteLine("Description of recipe: ");
            string Description = InputController.Input();
            View.ConsoleWriteLine("Instruction for recipe: ");
            string Instruction = InputController.Input();
            List<Composition> composition = new List<Composition>();
            if (UnitOfWork.Ingredients.Count() > 0)
            {
                View.ConsoleWriteLine("Adding ingredients. Write /done to finish");
                View.ShowIngredients();
                while (true)
                {
                    View.ConsoleWriteLine("Enter ID of ingredient:");
                    string t = InputController.Input();
                    if (t == "/done") break;
                    int ingID = Convert.ToInt32(t);
                    View.ConsoleWriteLine("Enter amount of this ingredient:");
                    double amount = Convert.ToDouble(InputController.Input());
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
                View.ShowCategories(true);
                View.ConsoleWriteLine("Which categories this recipe belongs?\n(separated by commas with space)");
                string[] strpID = InputController.Input().Split(", ", StringSplitOptions.RemoveEmptyEntries);
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

using System;
using System.Collections.Generic;
using Model;

namespace Task2.Controller
{
    class RecipeController
    {
        public static void AddRecipe()
        {
            View.ConsoleWriteLine("Name of recipe: ");
            string Name = MainController.Input();
            View.ConsoleWriteLine("Description of recipe: ");
            string Description = MainController.Input();
            View.ConsoleWriteLine("Instruction for recipe: ");
            string Instruction = MainController.Input();
            List<Composition> composition = new List<Composition>();
            if (MainController.UnitOfWork.Ingredients.Count() > 0)
            {
                View.ConsoleWriteLine("Adding ingredients. Write /done to finish");
                IngredientView.ShowIngredients();
                while (true)
                {
                    View.ConsoleWriteLine("Enter ID of ingredient:");
                    string t = MainController.Input();
                    if (t == "/done") break;
                    int ingID = Convert.ToInt32(t);
                    View.ConsoleWriteLine("Enter amount of this ingredient:");
                    double amount = Convert.ToDouble(MainController.Input());
                    composition.Add(new Composition { IngredientID = ingID, Amount = amount });
                }
            }
            Recipe newRec = new Recipe
            {
                Name = Name,
                ID = MainController.UnitOfWork.Recipes.Count() + 1,
                Description = Description,
                Instruction = Instruction,
                Composition = composition
            };
            if (MainController.UnitOfWork.Categories.Count() > 0)
            {
                List<int> parentIDs = new List<int>();
                CategoryView.ShowCategories(true);
                View.ConsoleWriteLine("Which categories this recipe belongs?\n(separated by commas with space)");
                string[] strpID = MainController.Input().Split(", ", StringSplitOptions.RemoveEmptyEntries);
                if (strpID.Length > 0)
                {
                    foreach (string p in strpID)
                    {
                        int t = Convert.ToInt32(p);
                        parentIDs.Add(t);
                        if (MainController.UnitOfWork.Categories[t].CategoryMembers == null) MainController.UnitOfWork.Categories[t].CategoryMembers = new List<int>();
                        MainController.UnitOfWork.Categories[t].CategoryMembers.Add(newRec.ID);
                    }
                }
                newRec.ParentIDs = parentIDs;
            }
            MainController.UnitOfWork.Recipes.Add(newRec);
            View.ConsoleWriteLine("Done!");
        }
    }
}

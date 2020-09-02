using System;
using System.Collections.Generic;
using Model;

namespace Task2.Controller
{
    class RecipeController
    {
        public static IRepository<Recipe> recipes = new JsonRecipeRepository();
        public static void AddRecipe()
        {
            View.ConsoleWriteLine("Name of recipe: ");
            string Name = Controller.Input();
            View.ConsoleWriteLine("Description of recipe: ");
            string Description = Controller.Input();
            View.ConsoleWriteLine("Instruction for recipe: ");
            string Instruction = Controller.Input();
            List<Composition> composition = new List<Composition>();
            if (IngredientController.ingredients.Count() > 0)
            {
                View.ConsoleWriteLine("Adding ingredients. Write /done to finish");
                IngredientView.ShowIngredients();
                while (true)
                {
                    View.ConsoleWriteLine("Enter ID of ingredient:");
                    string t = Controller.Input();
                    if (t == "/done") break;
                    int ingID = Convert.ToInt32(t);
                    View.ConsoleWriteLine("Enter amount of this ingredient:");
                    double amount = Convert.ToDouble(Controller.Input());
                    composition.Add(new Composition { IngredientID = ingID, Amount = amount });
                }
            }
            Recipe newRec = new Recipe
            {
                Name = Name,
                ID = recipes.Count() + 1,
                Description = Description,
                Instruction = Instruction,
                Composition = composition
            };
            if (CategoryController.categories.Count() > 0)
            {
                List<int> parentIDs = new List<int>();
                CategoryView.ShowCategories(true);
                View.ConsoleWriteLine("Which categories this recipe belongs?\n(separated by commas with space)");
                string[] strpID = Controller.Input().Split(", ", StringSplitOptions.RemoveEmptyEntries);
                if (strpID.Length > 0)
                {
                    foreach (string p in strpID)
                    {
                        int t = Convert.ToInt32(p);
                        parentIDs.Add(t);
                        if (CategoryController.categories[t].CategoryMembers == null) CategoryController.categories[t].CategoryMembers = new List<int>();
                        CategoryController.categories[t].CategoryMembers.Add(newRec.ID);
                    }
                }
                newRec.ParentIDs = parentIDs;
            }
            recipes.Add(newRec);
            recipes.Save();
            CategoryController.categories.Save();
            View.ConsoleWriteLine("Done!");
        }
    }
}

using System;

namespace Task2.Controller
{
    public class RecipeView : View
    {
        public static void ShowRecipe(int index)
        {
            ConsoleWriteLine(MainController.UnitOfWork.Recipes[index].Name, ConsoleColor.Green);
            ConsoleWriteLine("Description: ", ConsoleColor.Green);
            ConsoleWriteLine(MainController.UnitOfWork.Recipes[index].Description);
            ConsoleWriteLine("Instruction: ", ConsoleColor.Green);
            ConsoleWriteLine(MainController.UnitOfWork.Recipes[index].Instruction);
            ConsoleWriteLine("Ingredients: ", ConsoleColor.Green);
            int i = 0;
            foreach (var p in MainController.UnitOfWork.Recipes[index].Composition)
            {
                i++;
                ConsoleWriteLine(String.Format("  {0}. {1} ({2})", i, MainController.UnitOfWork.Ingredients[p.IngredientID].Name, p.Amount));
            }
        }
        public static void ShowRecipes()
        {
            string res = "";
            res += "ID | Name\n";
            foreach (var p in MainController.UnitOfWork.Recipes.GetList())
            {
                res += p.Key + " | " + p.Value.Name + '\n';
            }
            ConsoleWriteLine(res);
        }
    }
}

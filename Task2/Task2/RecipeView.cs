using System;
using System.Collections.Generic;
using System.Text;

namespace Task2
{
    public class RecipeView : View
    {
        public static void ShowRecipe(int index)
        {
            ConsoleWriteLine(Controller.recipes[index].Name, ConsoleColor.Green);
            ConsoleWriteLine("Description: ", ConsoleColor.Green);
            ConsoleWriteLine(Controller.recipes[index].Description);
            ConsoleWriteLine("Instruction: ", ConsoleColor.Green);
            ConsoleWriteLine(Controller.recipes[index].Instruction);
            ConsoleWriteLine("Ingredients: ", ConsoleColor.Green);
            int i = 0;
            foreach (var p in Controller.recipes[index].Composition)
            {
                i++;
                ConsoleWriteLine(String.Format("  {0}. {1} ({2})", i, Controller.ingredients[p.IngredientID].Name, p.Amount));
            }
        }
        public static void ShowRecipes()
        {
            string res = "";
            res += "ID | Name\n";
            foreach (var p in Controller.recipes)
            {
                res += p.Key + " | " + p.Value.Name + '\n';
            }
            ConsoleWriteLine(res);
        }
    }
}

using System;

namespace Task2
{
    abstract class View
    {
        public static void ShowCategories()
        {
            ConsoleWriteLine("ID | Name");
            foreach (var p in Controller.categories)
            {
                if(p.Value.ParentIDs == null)
                {
                    ConsoleWriteLine(p.Key + " | " + p.Value.Name);
                }
            }
        }

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

        public static void ShowCategory(int index)
        {
            ConsoleWriteLine(Controller.categories[index].Name, ConsoleColor.Green);
            ConsoleWriteLine("ID | Name") ;
            foreach (var p in Controller.categories[index].CategoryMembers)
            {
                if (p < 0)
                {
                    ConsoleWriteLine(p + " | " + Controller.categories[-p].Name, ConsoleColor.Cyan);
                }
                else
                {
                    ConsoleWriteLine(p + " | " + Controller.recipes[p].Name);
                }
            }
        }
        public static void ShowIngredients()
        {
            string res = "";
            res += "ID | Name\n";
            foreach (var p in Controller.ingredients)
            {
                res += p.Key + " | " + p.Value.Name + '\n';
            }
            ConsoleWriteLine(res);
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

        public static void ConsoleWriteLine(string text)
        {
            Console.WriteLine(text);
        }

        public static void ConsoleWriteLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}

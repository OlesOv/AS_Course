using System;
using Model;

namespace Task2
{
    class View
    {
        public void ConsoleWriteLine(string text)
        {
            Console.WriteLine(text);
        }
        public void ConsoleWriteLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public void ShowCategories(IRepository<Category> categories, bool isShowingSubCats)
        {
            ConsoleWriteLine("ID | Name");
            foreach (var p in categories.GetList())
            {
                if (isShowingSubCats ? true : p.Value.ParentIDs == null)
                {
                    ConsoleWriteLine(p.Key + " | " + p.Value.Name);
                }
            }
        }
        public void ShowCategory(Category category, IRepository<Category> categories, IRepository<Recipe> recipes)
        {
            ConsoleWriteLine(category.Name, ConsoleColor.Green);
            ConsoleWriteLine("ID | Name");
            try
            {

                foreach (var p in category.CategoryMembers)
                {
                    if (p < 0)
                    {
                        ConsoleWriteLine(p + " | " + categories[-p].Name, ConsoleColor.Cyan);
                    }
                    else
                    {
                        ConsoleWriteLine(p + " | " + recipes[p].Name);
                    }
                }
            }
            catch
            {
                ConsoleWriteLine("I think there is no recipes. Try /add_recipe");
            }
        }

        public void ShowIngredients(IRepository<Ingredient> ingredients)
        {
            string res = "";
            res += "ID | Name\n";
            foreach (var p in ingredients.GetList())
            {
                res += p.Key + " | " + p.Value.Name + '\n';
            }
            ConsoleWriteLine(res);
        }

        public void ShowRecipe(Recipe recipe, IRepository<Ingredient> ingredients)
        {
            ConsoleWriteLine(recipe.Name, ConsoleColor.Green);
            ConsoleWriteLine("Description: ", ConsoleColor.Green);
            ConsoleWriteLine(recipe.Description);
            ConsoleWriteLine("Instruction: ", ConsoleColor.Green);
            ConsoleWriteLine(recipe.Instruction);
            ConsoleWriteLine("Ingredients: ", ConsoleColor.Green);
            int i = 0;
            foreach (var p in recipe.Composition)
            {
                i++;
                ConsoleWriteLine(String.Format("  {0}. {1} ({2})", i, ingredients[p.IngredientID].Name, p.Amount));
            }
        }
        public void ShowRecipes(IRepository<Recipe> recipes)
        {
            string res = "";
            res += "ID | Name\n";
            foreach (var p in recipes.GetList())
            {
                res += p.Key + " | " + p.Value.Name + '\n';
            }
            ConsoleWriteLine(res);
        }
    }
}

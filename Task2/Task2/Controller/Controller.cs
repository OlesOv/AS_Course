using System;
using System.IO;

namespace Task2.Controller
{
    class Controller
    {
        public static string path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\Data";
        
        public static string Input()
        {
            return Console.ReadLine();
        }
        public static void Main()
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            while (true)
            {
                View.ConsoleWriteLine("Enter /help to learn commands", ConsoleColor.Yellow);
                if (CategoryController.categories.Count() > 0) CategoryView.ShowCategories(false);
                else View.ConsoleWriteLine("There is no data. Add new category with /add_category");
                string t = Input();
                switch (t)
                {
                    case "/exit":
                        return;

                    case "/help":
                        View.ConsoleWriteLine("/exit - to exit\n/add_category - to add category\n/add_recipe - to add recipe\n/add_ingredient - to add ingredient\nNow you can navigate through categories, subcategories and recipes using IDs");
                        break;

                    case "/add_category":
                        CategoryController.AddCategory();
                        break;

                    case "/add_recipe":
                        RecipeController.AddRecipe();
                        break;

                    case "/add_ingredient":
                        IngredientController.AddIngredient();
                        break;

                    default:
                        int selected;
                        try
                        {
                            selected = Convert.ToInt32(t);
                        }
                        catch
                        {
                            View.ConsoleWriteLine("Something's wrong");
                            continue;
                        }
                        CategoryController.OpenCategory(selected);
                        break;
                }
                View.ConsoleWriteLine("");
            }
        }
    }
}

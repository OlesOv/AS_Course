using System;
using System.IO;

namespace Task2.Controller
{
    class Core : MVCConnect { }
    class MainController
    {
        public static string Path = AppDomain.CurrentDomain.BaseDirectory + "Data";
        public static void Main()
        {
            Core core = new Core();
            CategoryController categoryController = new CategoryController(core);
            IngredientController ingredientController = new IngredientController(core);
            RecipeController recipeController = new RecipeController(core);

            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }

            while (true)
            {
                core.View.ConsoleWriteLine("Enter /help to learn commands", ConsoleColor.Yellow);
                if (core.UnitOfWork.Categories.Count() > 0) core.View.ShowCategories(core.UnitOfWork.Categories, false);
                else core.View.ConsoleWriteLine("There is no data. Add new category with /add_category");
                string t = core.inputController.Input();
                switch (t)
                {
                    case "/exit":
                        core.UnitOfWork.Save();
                        return;

                    case "/help":
                        core.View.ConsoleWriteLine("/exit - to exit\n/add_category - to add category\n/add_recipe - to add recipe\n/add_ingredient - to add ingredient\nNow you can navigate through categories, subcategories and recipes using IDs");
                        break;

                    case "/add_category":
                        categoryController.AddCategory();
                        break;

                    case "/add_recipe":
                        recipeController.AddRecipe();
                        break;

                    case "/add_ingredient":
                        ingredientController.AddIngredient();
                        break;

                    default:
                        int selected;
                        try
                        {
                            selected = Convert.ToInt32(t);
                        }
                        catch
                        {
                            core.View.ConsoleWriteLine("Something's wrong");
                            continue;
                        }
                        categoryController.OpenCategory(selected);
                        break;
                }
                core.View.ConsoleWriteLine("");
            }
        }
    }
}

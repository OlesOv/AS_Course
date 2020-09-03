using System;
using System.IO;
using Model;

namespace Task2.Controller
{
    class Core : MVCConnect { }
    class MainController
    {
        public const string Path = @"C:\Users\paren\Documents\GitHub\altexsoft-lab-2020\Task2\Data";
        public static void Main()
        {
            Core Core = new Core();
            View View = new View(Core);
            CategoryController CategoryController = new CategoryController(View);
            IngredientController IngredientController = new IngredientController(View);
            RecipeController RecipeController = new RecipeController(View);
            Core.Path = Path;

            if (!Directory.Exists(Core.Path))
            {
                Directory.CreateDirectory(Core.Path);
            }

            while (true)
            {
                View.ConsoleWriteLine("Enter /help to learn commands", ConsoleColor.Yellow);
                if (Core.UnitOfWork.Categories.Count() > 0) View.ShowCategories(false);
                else View.ConsoleWriteLine("There is no data. Add new category with /add_category");
                string t = Core.InputController.Input();
                switch (t)
                {
                    case "/exit":
                        Core.UnitOfWork.Save();
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
                        View.OpenCategory(selected);
                        break;
                }
                View.ConsoleWriteLine("");
            }
        }
    }
}

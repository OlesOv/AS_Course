using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Task2
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
            string jsonString;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (File.Exists(CategoryController.categories.Path))
            {
                jsonString = File.ReadAllText(CategoryController.categories.Path);
                var t = CategoryController.categories.Path;
                CategoryController.categories = new MyDict<Category>(JsonSerializer.Deserialize<List<Category>>(jsonString).ToDictionary(p => p.ID));
                CategoryController.categories.Path = t;
            }

            if (File.Exists(RecipeController.recipes.Path))
            {
                jsonString = File.ReadAllText(RecipeController.recipes.Path);
                var t = RecipeController.recipes.Path;
                RecipeController.recipes = new MyDict<Recipe>(JsonSerializer.Deserialize<List<Recipe>>(jsonString).ToDictionary(p => p.ID));
                RecipeController.recipes.Path = t;
            }

            if (File.Exists(IngredientController.ingredients.Path))
            {
                jsonString = File.ReadAllText(IngredientController.ingredients.Path);
                var t = IngredientController.ingredients.Path;
                IngredientController.ingredients = new MyDict<Ingredient>(JsonSerializer.Deserialize<List<Ingredient>>(jsonString).ToDictionary(p => p.ID));
                IngredientController.ingredients.Path = t;
            }
            while (true)
            {
                View.ConsoleWriteLine("Enter /help to learn commands");
                if (CategoryController.categories.Count > 0) CategoryView.ShowCategories();
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
                        int selected = Convert.ToInt32(t);
                        CategoryController.OpenCategory(selected);
                        break;
                }
                View.ConsoleWriteLine("");
            }
        }
    }
}

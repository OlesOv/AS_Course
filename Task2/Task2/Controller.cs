using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Task2
{
    abstract class Controller
    {
        public static Dictionary<int, Category> categories = new Dictionary<int, Category>();
        public static Dictionary<int, Recipe> recipes = new Dictionary<int, Recipe>();
        public static Dictionary<int, Ingredient> ingredients = new Dictionary<int, Ingredient>();
        public static string path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\Data";

        public static string Input()
        {
            return Console.ReadLine();
        }

        public static void Main()
        {
            if (!Directory.Exists(@path))
            {
                Directory.CreateDirectory(@path);
            }

            string jsonString;
            if (File.Exists(@path + "\\Categories.json"))
            {
                jsonString = File.ReadAllText(@path + "\\Categories.json");
                categories = JsonSerializer.Deserialize<List<Category>>(jsonString).ToDictionary(p => p.ID);
            }

            if (File.Exists(@path + "\\Recipes.json"))
            {
                jsonString = File.ReadAllText(@path + "\\Recipes.json");
                recipes = JsonSerializer.Deserialize<List<Recipe>>(jsonString).ToDictionary(p => p.ID);
            }

            if (File.Exists(@path + "\\Ingredients.json"))
            {
                jsonString = File.ReadAllText(@path + "\\Ingredients.json");
                ingredients = JsonSerializer.Deserialize<List<Ingredient>>(jsonString).ToDictionary(p => p.ID);
            }
            while (true)
            {
                View.ConsoleWriteLine("Enter /help to learn commands");
                if (categories.Count > 0) View.ShowCategories();
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
                        AddCategory();
                        break;

                    case "/add_recipe":
                        AddRecipe();
                        break;

                    case "/add_ingredient":
                        AddIngredient();
                        break;

                    default:
                        int selected = Convert.ToInt32(t);
                        OpenCategory(selected);
                        break;
                }
                View.ConsoleWriteLine("");
            }
        }
        public static void AddIngredient()
        {
            View.ConsoleWriteLine("What's name if Ingredient?");
            Ingredient newIng = new Ingredient
            {
                Name = Input(),
                ID = ingredients.Count() + 1
            };
            ingredients.Add(newIng.ID, newIng);
            string jsonString = JsonSerializer.Serialize(ingredients.Values);
            File.WriteAllText(@path + "\\Ingredients.json", jsonString);
            View.ConsoleWriteLine("Done!");
        }
        public static Category AddCategory()
        {
            View.ConsoleWriteLine("Name of category: ");
            Category newCat = new Category
            {
                Name = Input(),
                ID = categories.Count() + 1
            };
            List<int> membersIDs = new List<int>();
            if (recipes.Count > 0)
            {
                View.ShowRecipes();
                View.ConsoleWriteLine("What recipes to add here?\n(separated by commas with space)");
                string[] strmID = Input().Split(", ");
                if (strmID.Length > 0)
                {
                    foreach (string p in strmID)
                    {
                        int t = Convert.ToInt32(p);
                        membersIDs.Add(t);
                        if (recipes[t].ParentIDs == null) recipes[t].ParentIDs = new List<int>();
                        recipes[t].ParentIDs.Add(newCat.ID);
                    }
                }
                newCat.CategoryMembers = membersIDs;
            }
            if (categories.Count > 0)
            {
                List<int> parentIDs = new List<int>();
                View.ShowCategories();
                View.ConsoleWriteLine("You can make a subcategory from this category. Enter an ID's of parent categories, or leave it empty\n(separated by commas with space)");
                string[] strpID = Input().Split(", ");
                if(strpID.Length > 0 && strpID[0] != "")
                {
                    foreach (string p in strpID)
                    {
                        int t = Convert.ToInt32(p);
                        parentIDs.Add(t);
                        if (categories[t].CategoryMembers == null) categories[t].CategoryMembers = new List<int>();
                        categories[t].CategoryMembers.Add(-newCat.ID);
                    }
                    newCat.ParentIDs = parentIDs;
                }
            }
            categories.Add(newCat.ID, newCat);
            string jsonString = JsonSerializer.Serialize(categories.Values);
            File.WriteAllText(@path + "\\Categories.json", jsonString);
            jsonString = JsonSerializer.Serialize(recipes.Values);
            File.WriteAllText(@path + "\\Recipes.json", jsonString);
            View.ConsoleWriteLine("Done!");
            return newCat;
        }
        public static void AddRecipe()
        {
            View.ConsoleWriteLine("Name of recipe: ");
            string Name = Input();
            View.ConsoleWriteLine("Description of recipe: ");
            string Description = Input();
            View.ConsoleWriteLine("Instruction for recipe: ");
            string Instruction = Input();
            List<Composition> composition = new List<Composition>();
            if (ingredients.Count > 0)
            {
                View.ConsoleWriteLine("Adding ingredients. Write /done to finish");
                View.ShowIngredients();
                while (true)
                {
                    View.ConsoleWriteLine("Enter ID of ingredient:");
                    string t = Input();
                    if (t == "/done") break;
                    int ingID = Convert.ToInt32(t);
                    View.ConsoleWriteLine("Enter amount of this ingredient:");
                    double amount = Convert.ToDouble(Input());
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
            if (categories.Count > 0)
            {
                List<int> parentIDs = new List<int>();
                View.ShowCategories();
                View.ConsoleWriteLine("Which categories this recipe belongs?\n(separated by commas with space)");
                string[] strpID = Input().Split(", ");
                if(strpID.Length > 0 && strpID[0] != "")
                {
                    foreach (string p in strpID)
                    {
                        int t = Convert.ToInt32(p);
                        parentIDs.Add(t);
                        if (categories[t].CategoryMembers == null) categories[t].CategoryMembers = new List<int>();
                        categories[t].CategoryMembers.Add(newRec.ID);
                    }
                }
                newRec.ParentIDs = parentIDs;
            }
            recipes.Add(newRec.ID, newRec);
            string jsonString = JsonSerializer.Serialize(recipes.Values);
            File.WriteAllText(@path + "\\Recipes.json", jsonString);
            jsonString = JsonSerializer.Serialize(categories.Values);
            File.WriteAllText(@path + "\\Categories.json", jsonString);
            View.ConsoleWriteLine("Done!");
        }
        public static void OpenCategory(int selected)
        {
            if (categories.ContainsKey(selected))
            {
                View.ShowCategory(selected);
                selected = Convert.ToInt32(Input());
                if (selected < 0)
                {
                    OpenCategory(-selected);
                }
                else if (recipes.ContainsKey(selected))
                {
                    View.ShowRecipe(selected);
                }
                else
                {
                    View.ConsoleWriteLine("Wrong ID");
                }
            }
            else View.ConsoleWriteLine("Wrong ID");
        }
    }
}

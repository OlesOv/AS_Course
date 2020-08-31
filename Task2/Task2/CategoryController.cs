using System;
using System.Collections.Generic;
using Model;
using System.Linq;

namespace Task2
{
    class CategoryController
    {
        public static MyDict<Category> categories = new MyDict<Category>(Controller.path + "\\Categories.json");
        public static Category AddCategory()
        {
            View.ConsoleWriteLine("Name of category: ");
            Category newCat = new Category
            {
                Name = Controller.Input(),
                ID = categories.Count() + 1
            };
            List<int> membersIDs = new List<int>();
            if (RecipeController.recipes.Count > 0)
            {
                RecipeView.ShowRecipes();
                View.ConsoleWriteLine("What recipes to add here?\n(separated by commas with space)");
                string[] strmID = Controller.Input().Split(", ");
                if (strmID.Length > 0)
                {
                    foreach (string p in strmID)
                    {
                        int t = Convert.ToInt32(p);
                        membersIDs.Add(t);
                        if (RecipeController.recipes[t].ParentIDs == null) RecipeController.recipes[t].ParentIDs = new List<int>();
                        RecipeController.recipes[t].ParentIDs.Add(newCat.ID);
                    }
                }
                newCat.CategoryMembers = membersIDs;
            }
            if (categories.Count > 0)
            {
                List<int> parentIDs = new List<int>();
                CategoryView.ShowCategories();
                View.ConsoleWriteLine("You can make a subcategory from this category. Enter an ID's of parent categories, or leave it empty\n(separated by commas with space)");
                string[] strpID = Controller.Input().Split(", ");
                if (strpID.Length > 0 && strpID[0] != "")
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
            categories.UpdateJSON();
            RecipeController.recipes.UpdateJSON();
            View.ConsoleWriteLine("Done!");
            return newCat;
        }
        public static void OpenCategory(int selected)
        {
            if (categories.ContainsKey(selected))
            {
                CategoryView.ShowCategory(selected);
                selected = Convert.ToInt32(Controller.Input());
                if (selected < 0)
                {
                    OpenCategory(-selected);
                }
                else if (RecipeController.recipes.ContainsKey(selected))
                {
                    RecipeView.ShowRecipe(selected);
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

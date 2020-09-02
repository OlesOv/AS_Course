using System;
using System.Collections.Generic;
using Model;

namespace Task2.Controller
{
    class CategoryController
    {
        public static IRepository<Category> categories = new JsonCategoryRepository();
        public static Category AddCategory()
        {
            View.ConsoleWriteLine("Name of category: ");
            Category newCat = new Category
            {
                Name = Controller.Input(),
                ID = categories.Count() + 1
            };
            List<int> membersIDs = new List<int>();
            if (RecipeController.recipes.Count() > 0)
            {
                RecipeView.ShowRecipes();
                View.ConsoleWriteLine("What recipes to add here?\n(separated by commas with space)");
                string[] strmID = Controller.Input().Split(", ", StringSplitOptions.RemoveEmptyEntries);
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
            if (categories.Count() > 0)
            {
                List<int> parentIDs = new List<int>();
                CategoryView.ShowCategories(true);
                View.ConsoleWriteLine("You can make a subcategory from this category. Enter an ID's of parent categories, or leave it empty\n(separated by commas with space)");
                string[] strpID = Controller.Input().Split(", ", StringSplitOptions.RemoveEmptyEntries);
                if (strpID.Length > 0)
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
            categories.Add(newCat);
            categories.Save();
            RecipeController.recipes.Save();
            View.ConsoleWriteLine("Done!");
            return newCat;
        }
        public static void OpenCategory(int selected)
        {
            if (categories.GetElement(selected) != null)
            {
                CategoryView.ShowCategory(selected);
                try
                {
                    selected = Convert.ToInt32(Controller.Input());
                }
                catch
                {
                    View.ConsoleWriteLine("Something's wrong");
                    return;
                }
                if (selected < 0)
                {
                    OpenCategory(-selected);
                }
                else if (RecipeController.recipes.GetElement(selected) != null)
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

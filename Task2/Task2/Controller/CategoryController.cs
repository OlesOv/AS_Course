using System;
using System.Collections.Generic;
using Model;

namespace Task2.Controller
{
    class CategoryController
    {
        public static Category AddCategory()
        {
            View.ConsoleWriteLine("Name of category: ");
            Category newCat = new Category
            {
                Name = MainController.Input(),
                ID = MainController.UnitOfWork.Categories.Count() + 1
            };
            List<int> membersIDs = new List<int>();
            if (MainController.UnitOfWork.Recipes.Count() > 0)
            {
                RecipeView.ShowRecipes();
                View.ConsoleWriteLine("What recipes to add here?\n(separated by commas with space)");
                string[] strmID = MainController.Input().Split(", ", StringSplitOptions.RemoveEmptyEntries);
                if (strmID.Length > 0)
                {
                    foreach (string p in strmID)
                    {
                        int t = Convert.ToInt32(p);
                        membersIDs.Add(t);
                        if (MainController.UnitOfWork.Recipes[t].ParentIDs == null) MainController.UnitOfWork.Recipes[t].ParentIDs = new List<int>();
                        MainController.UnitOfWork.Recipes[t].ParentIDs.Add(newCat.ID);
                    }
                }
                newCat.CategoryMembers = membersIDs;
            }
            if (MainController.UnitOfWork.Categories.Count() > 0)
            {
                List<int> parentIDs = new List<int>();
                CategoryView.ShowCategories(true);
                View.ConsoleWriteLine("You can make a subcategory from this category. Enter an ID's of parent categories, or leave it empty\n(separated by commas with space)");
                string[] strpID = MainController.Input().Split(", ", StringSplitOptions.RemoveEmptyEntries);
                if (strpID.Length > 0)
                {
                    foreach (string p in strpID)
                    {
                        int t = Convert.ToInt32(p);
                        parentIDs.Add(t);
                        if (MainController.UnitOfWork.Categories[t].CategoryMembers == null) MainController.UnitOfWork.Categories[t].CategoryMembers = new List<int>();
                        MainController.UnitOfWork.Categories[t].CategoryMembers.Add(-newCat.ID);
                    }
                    newCat.ParentIDs = parentIDs;
                }
            }
            MainController.UnitOfWork.Categories.Add(newCat);
            View.ConsoleWriteLine("Done!");
            return newCat;
        }
        public static void OpenCategory(int selected)
        {
            if (MainController.UnitOfWork.Categories.GetElement(selected) != null)
            {
                CategoryView.ShowCategory(selected);
                try
                {
                    selected = Convert.ToInt32(MainController.Input());
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
                else if (MainController.UnitOfWork.Recipes.GetElement(selected) != null)
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

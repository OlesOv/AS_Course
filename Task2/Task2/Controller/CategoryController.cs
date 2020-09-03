using System;
using System.Collections.Generic;
using Model;

namespace Task2.Controller
{
    class CategoryController : MVCConnect
    {
        View View;
        public CategoryController(View view)
        {
            View = view;
        }
        public Category AddCategory()
        {
            View.ConsoleWriteLine("Name of category: ");
            Category newCat = new Category
            {
                Name = InputController.Input(),
                ID = UnitOfWork.Categories.Count() + 1
            };
            List<int> membersIDs = new List<int>();
            if (UnitOfWork.Recipes.Count() > 0)
            {
                View.ShowRecipes();
                View.ConsoleWriteLine("What recipes to add here?\n(separated by commas with space)");
                string[] strmID = InputController.Input().Split(", ", StringSplitOptions.RemoveEmptyEntries);
                if (strmID.Length > 0)
                {
                    foreach (string p in strmID)
                    {
                        int t = Convert.ToInt32(p);
                        membersIDs.Add(t);
                        if (UnitOfWork.Recipes[t].ParentIDs == null) UnitOfWork.Recipes[t].ParentIDs = new List<int>();
                        UnitOfWork.Recipes[t].ParentIDs.Add(newCat.ID);
                    }
                }
                newCat.CategoryMembers = membersIDs;
            }
            if (UnitOfWork.Categories.Count() > 0)
            {
                List<int> parentIDs = new List<int>();
                View.ShowCategories(true);
                View.ConsoleWriteLine("You can make a subcategory from this category. Enter an ID's of parent categories, or leave it empty\n(separated by commas with space)");
                string[] strpID = InputController.Input().Split(", ", StringSplitOptions.RemoveEmptyEntries);
                if (strpID.Length > 0)
                {
                    foreach (string p in strpID)
                    {
                        int t = Convert.ToInt32(p);
                        parentIDs.Add(t);
                        if (UnitOfWork.Categories[t].CategoryMembers == null) UnitOfWork.Categories[t].CategoryMembers = new List<int>();
                        UnitOfWork.Categories[t].CategoryMembers.Add(-newCat.ID);
                    }
                    newCat.ParentIDs = parentIDs;
                }
            }
            UnitOfWork.Categories.Add(newCat);
            View.ConsoleWriteLine("Done!");
            return newCat;
        }
        
    }
}

using System;

namespace Task2.Controller
{
    public class CategoryView : View
    {
        public static void ShowCategories(bool isShowingSubCats)
        {
            ConsoleWriteLine("ID | Name");
            foreach (var p in MainController.UnitOfWork.Categories.GetList())
            {
                if (isShowingSubCats ? true : p.Value.ParentIDs == null)
                {
                    ConsoleWriteLine(p.Key + " | " + p.Value.Name);
                }
            }
        }
        public static void ShowCategory(int index)
        {
            ConsoleWriteLine(MainController.UnitOfWork.Categories[index].Name, ConsoleColor.Green);
            ConsoleWriteLine("ID | Name");
            try
            {

                foreach (var p in MainController.UnitOfWork.Categories[index].CategoryMembers)
                {
                    if (p < 0)
                    {
                        ConsoleWriteLine(p + " | " + MainController.UnitOfWork.Categories[-p].Name, ConsoleColor.Cyan);
                    }
                    else
                    {
                        ConsoleWriteLine(p + " | " + MainController.UnitOfWork.Recipes[p].Name);
                    }
                }
            }
            catch
            {
                ConsoleWriteLine("I think there is no recipes. Try /add_recipe");
            }
        }
    }
}

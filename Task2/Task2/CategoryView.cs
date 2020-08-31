using System;
using System.Collections.Generic;
using System.Text;

namespace Task2
{
    public class CategoryView : View
    {
        public static void ShowCategories()
        {
            ConsoleWriteLine("ID | Name");
            foreach (var p in Controller.categories)
            {
                if (p.Value.ParentIDs == null)
                {
                    ConsoleWriteLine(p.Key + " | " + p.Value.Name);
                }
            }
        }
        public static void ShowCategory(int index)
        {
            ConsoleWriteLine(Controller.categories[index].Name, ConsoleColor.Green);
            ConsoleWriteLine("ID | Name");
            try
            {

                foreach (var p in Controller.categories[index].CategoryMembers)
                {
                    if (p < 0)
                    {
                        ConsoleWriteLine(p + " | " + Controller.categories[-p].Name, ConsoleColor.Cyan);
                    }
                    else
                    {
                        ConsoleWriteLine(p + " | " + Controller.recipes[p].Name);
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

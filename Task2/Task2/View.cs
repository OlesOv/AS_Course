using System;
using Model;

namespace Task2
{
    class View
    {
        private UnitOfWork UnitOfWork;
        private Controller.InputController InputController;
        public View (MVCConnect core)
        {
            UnitOfWork = MVCConnect.UnitOfWork;
            InputController = core.InputController;
        }
        public void ConsoleWriteLine(string text)
        {
            Console.WriteLine(text);
        }
        public void ConsoleWriteLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public void ShowCategories(bool isShowingSubCats)
        {
            ConsoleWriteLine("ID | Name");
            foreach (var p in UnitOfWork.Categories.GetList())
            {
                if (isShowingSubCats ? true : p.Value.ParentIDs == null)
                {
                    ConsoleWriteLine(p.Key + " | " + p.Value.Name);
                }
            }
        }
        public void ShowCategory(int index)
        {
            ConsoleWriteLine(UnitOfWork.Categories[index].Name, ConsoleColor.Green);
            ConsoleWriteLine("ID | Name");
            try
            {

                foreach (var p in UnitOfWork.Categories[index].CategoryMembers)
                {
                    if (p < 0)
                    {
                        ConsoleWriteLine(p + " | " + UnitOfWork.Categories[-p].Name, ConsoleColor.Cyan);
                    }
                    else
                    {
                        ConsoleWriteLine(p + " | " + UnitOfWork.Recipes[p].Name);
                    }
                }
            }
            catch
            {
                ConsoleWriteLine("I think there is no recipes. Try /add_recipe");
            }
        }
        public void OpenCategory(int selected)
        {
            if (UnitOfWork.Categories.GetElement(selected) != null)
            {
                ShowCategory(selected);
                try
                {
                    selected = Convert.ToInt32(InputController.Input());
                }
                catch
                {
                    ConsoleWriteLine("Something's wrong");
                    return;
                }
                if (selected < 0)
                {
                    OpenCategory(-selected);
                }
                else if (UnitOfWork.Recipes.GetElement(selected) != null)
                {
                    ShowRecipe(selected);
                }
                else
                {
                    ConsoleWriteLine("Wrong ID");
                }
            }
            else ConsoleWriteLine("Wrong ID");
        }

        public void ShowIngredients()
        {
            string res = "";
            res += "ID | Name\n";
            foreach (var p in UnitOfWork.Ingredients.GetList())
            {
                res += p.Key + " | " + p.Value.Name + '\n';
            }
            ConsoleWriteLine(res);
        }

        public void ShowRecipe(int index)
        {
            ConsoleWriteLine(UnitOfWork.Recipes[index].Name, ConsoleColor.Green);
            ConsoleWriteLine("Description: ", ConsoleColor.Green);
            ConsoleWriteLine(UnitOfWork.Recipes[index].Description);
            ConsoleWriteLine("Instruction: ", ConsoleColor.Green);
            ConsoleWriteLine(UnitOfWork.Recipes[index].Instruction);
            ConsoleWriteLine("Ingredients: ", ConsoleColor.Green);
            int i = 0;
            foreach (var p in UnitOfWork.Recipes[index].Composition)
            {
                i++;
                ConsoleWriteLine(String.Format("  {0}. {1} ({2})", i, UnitOfWork.Ingredients[p.IngredientID].Name, p.Amount));
            }
        }
        public void ShowRecipes()
        {
            string res = "";
            res += "ID | Name\n";
            foreach (var p in UnitOfWork.Recipes.GetList())
            {
                res += p.Key + " | " + p.Value.Name + '\n';
            }
            ConsoleWriteLine(res);
        }
    }
}

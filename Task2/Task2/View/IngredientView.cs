namespace Task2.Controller
{
    public class IngredientView : View
    {
        public static void ShowIngredients()
        {
            string res = "";
            res += "ID | Name\n";
            foreach (var p in MainController.UnitOfWork.Ingredients.GetList())
            {
                res += p.Key + " | " + p.Value.Name + '\n';
            }
            ConsoleWriteLine(res);
        }
    }
}

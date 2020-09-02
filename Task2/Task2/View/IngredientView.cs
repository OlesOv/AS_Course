namespace Task2.Controller
{
    public class IngredientView : View
    {
        public static void ShowIngredients()
        {
            string res = "";
            res += "ID | Name\n";
            foreach (var p in IngredientController.ingredients.GetList())
            {
                res += p.Key + " | " + p.Value.Name + '\n';
            }
            ConsoleWriteLine(res);
        }
    }
}

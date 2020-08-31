namespace Task2
{
    public class IngredientView : View
    {
        public static void ShowIngredients()
        {
            string res = "";
            res += "ID | Name\n";
            foreach (var p in Controller.ingredients)
            {
                res += p.Key + " | " + p.Value.Name + '\n';
            }
            ConsoleWriteLine(res);
        }
    }
}

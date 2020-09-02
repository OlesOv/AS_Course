using Model;
using System.Linq;

namespace Task2.Controller
{
    class IngredientController
    {
        public static IRepository<Ingredient> ingredients = new JsonIngredientRepository();
        public static void AddIngredient()
        {
            View.ConsoleWriteLine("What's name if Ingredient?");
            Ingredient newIng = new Ingredient
            {
                Name = Controller.Input(),
                ID = ingredients.Count() + 1
            };
            ingredients.Add(newIng);
            ingredients.Save();
        }
    }
}

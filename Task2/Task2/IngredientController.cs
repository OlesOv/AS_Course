using Model;
using System.Linq;

namespace Task2
{
    class IngredientController
    {
        public static MyDict<Ingredient> ingredients = new MyDict<Ingredient>(Controller.path + "\\Ingredients.json");
        public static void AddIngredient()
        {
            View.ConsoleWriteLine("What's name if Ingredient?");
            Ingredient newIng = new Ingredient
            {
                Name = Controller.Input(),
                ID = ingredients.Count() + 1
            };
            ingredients.Add(newIng.ID, newIng);
            ingredients.UpdateJSON();
        }
    }
}

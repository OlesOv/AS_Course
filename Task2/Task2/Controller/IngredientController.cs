using Model;

namespace Task2.Controller
{
    class IngredientController
    {
        public static void AddIngredient()
        {
            View.ConsoleWriteLine("What's name if Ingredient?");
            Ingredient newIng = new Ingredient
            {
                Name = MainController.Input(),
                ID = MainController.UnitOfWork.Ingredients.Count() + 1
            };
            MainController.UnitOfWork.Ingredients.Add(newIng);
            View.ConsoleWriteLine("Done!");
        }
    }
}

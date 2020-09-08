using Model;

namespace Task2.Controller
{
    class IngredientController : MVCConnect
    {
        public IngredientController(MVCConnect core) : base(core) { }
        public void AddIngredient()
        {
            View.ConsoleWriteLine("What's name if Ingredient?");
            Ingredient newIng = new Ingredient
            {
                Name = inputController.Input(),
                ID = UnitOfWork.Ingredients.Count() + 1
            };
            UnitOfWork.Ingredients.Add(newIng);
            View.ConsoleWriteLine("Done!");
        }
    }
}

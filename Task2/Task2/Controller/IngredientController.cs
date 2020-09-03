using Model;

namespace Task2.Controller
{
    class IngredientController : MVCConnect
    {
        View View;
        public IngredientController(View view)
        {
            View = view;
        }
        public void AddIngredient()
        {
            View.ConsoleWriteLine("What's name if Ingredient?");
            Ingredient newIng = new Ingredient
            {
                Name = InputController.Input(),
                ID = UnitOfWork.Ingredients.Count() + 1
            };
            UnitOfWork.Ingredients.Add(newIng);
            View.ConsoleWriteLine("Done!");
        }
    }
}

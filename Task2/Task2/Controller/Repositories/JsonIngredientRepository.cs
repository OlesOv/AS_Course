using Model;

namespace Task2.Controller
{
    class JsonIngredientRepository : JsonRepository<Ingredient>
    {
        public JsonIngredientRepository() : base(Controller.path + "\\Ingredients.json") { }
    }
}

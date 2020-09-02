using Model;

namespace Task2.Controller
{
    class JsonRecipeRepository : JsonRepository<Recipe>
    {
        public JsonRecipeRepository() : base(Controller.path + "\\Recipes.json") { }
    }
}

namespace Model
{
    class JsonRecipeRepository : JsonRepository<Recipe>
    {
        public JsonRecipeRepository() : base(Task2.Controller.MainController.path + "\\Recipes.json") { }
    }
}

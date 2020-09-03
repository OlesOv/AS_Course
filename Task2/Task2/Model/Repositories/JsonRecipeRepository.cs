namespace Model
{
    class JsonRecipeRepository : JsonRepository<Recipe>
    {
        public JsonRecipeRepository() : base(Task2.Controller.MainController.Path + "\\Recipes.json") { }
    }
}

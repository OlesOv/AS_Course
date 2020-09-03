namespace Model
{
    class JsonIngredientRepository : JsonRepository<Ingredient>
    {
        public JsonIngredientRepository() : base(Task2.Controller.MainController.Path + "\\Ingredients.json") { }
    }
}

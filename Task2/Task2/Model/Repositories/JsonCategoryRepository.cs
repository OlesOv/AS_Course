namespace Model
{
    class JsonCategoryRepository : JsonRepository<Category>
    {
        public JsonCategoryRepository() : base(Task2.Controller.MainController.path + "\\Categories.json") { }
    }
}

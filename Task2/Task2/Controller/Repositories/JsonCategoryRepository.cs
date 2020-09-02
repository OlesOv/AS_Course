using Model;

namespace Task2.Controller
{
    class JsonCategoryRepository : JsonRepository<Category>
    {
        public JsonCategoryRepository() : base(Controller.path + "\\Categories.json") { }
    }
}

using System;

namespace Model
{
    class UnitOfWork : IDisposable
    {
        public UnitOfWork()
        {
            Categories = new JsonCategoryRepository();
            Ingredients = new JsonIngredientRepository();
            Recipes = new JsonRecipeRepository();
        }

        public IRepository<Category> Categories { get; }
        public IRepository<Ingredient> Ingredients { get; }
        public IRepository<Recipe> Recipes { get; }

        public void Save()
        {
            Categories.Save();
            Ingredients.Save();
            Recipes.Save();
        }
        public void Dispose() { }
    }
}

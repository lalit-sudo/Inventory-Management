using Inventory_Management.Models;

namespace InventoryManagement.Services
{
    public class CategoryService
    {
        private List<Category> _categories;
        public CategoryService()
        {
            _categories =  new List<Category>();
        }

        public string AddCategory(Category category)
        {
            foreach(Category cat in _categories)
            {
                if(cat.CategoryId == category.CategoryId)
                {
                    return $"Category with Category Id: {category.CategoryId} already exists";
                }
            }
            _categories.Add(category);
            return $"Cateory with Category Id: {category.CategoryId} added successfully";
        }

        public List<Category> GetCategories()
        {
            List<Category> categoryList = new List<Category>();
            foreach(Category cat in _categories)
            {
                categoryList.Add(cat);
            }
            return categoryList;
        }

        public Category? FindCategoryById(int categoryId)
        {
            return _categories.FirstOrDefault(c => c.CategoryId == categoryId);
        }

    }
}

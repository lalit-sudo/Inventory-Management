using Inventory_Management.Models;
using InventoryManagement.Models;

namespace InventoryManagement.Services
{
    public class CategoryService
    {
        private InventoryManagementContext _context;
        public CategoryService(InventoryManagementContext context)
        {
            _context = context;
        }

        //private List<Category> _categories;
        //public CategoryService()
        //{
        //    _categories =  new List<Category>();
        //}

        public string AddCategory(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return $"Cateory with Category Id: {category.CategoryId} added successfully";
            }
            catch(Exception ex)
            {
                return $"Error occured while adding category with error message:-  {ex.ToString()}";
            }

            //foreach(Category cat in _categories)
            //{
            //    if(cat.CategoryId == category.CategoryId)
            //    {
            //        return $"Category with Category Id: {category.CategoryId} already exists";
            //    }
            //}
            //_categories.Add(category);
            //return $"Cateory with Category Id: {category.CategoryId} added successfully";
        }

        public List<Category> GetCategories()
        {
            List<Category> categoryList = new List<Category>();
            categoryList = _context.Categories.ToList();

            //foreach(Category cat in _categories)
            //{
            //    categoryList.Add(cat);
            //}
            return categoryList;
        }

        public Category? FindCategoryById(int categoryId)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
            //return _categories.FirstOrDefault(c => c.CategoryId == categoryId);
        }

    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using CNewsProject.Data;
using CNewsProject.Models.DataBase;

namespace CNewsProject.Service
{
    public class CategoryService : ICategoryService
    {

        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;

        public CategoryService(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public bool CategoryExists(string name)
        {
            return _db.Category.Any(c => c.Name == name);
        }

        public List<Category> GetAllCategory()
        {
            return _db.Category.OrderBy(c => c.Id).ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _db.Category.Single(c => c.Id == id);
        }
        public Category GetCategoryByName(string name)
        {
            return _db.Category.FirstOrDefault(c => c.Name == name)!;
        }

        public void AddCategory(string name)
        {
            Category newCategory = new() { Name = name };
            _db.Category.Add(newCategory);
            _db.SaveChanges();

        }
        public void RemoveCategory(Category category)
        {
            _db.Category.Remove(_db.Category.Single(c => c.Id == category.Id));
            _db.SaveChanges();

        }

        public void EditCategory(Category category)
        {
            GetCategoryById(category.Id).Name = category.Name;
           
            _db.SaveChanges();
        }


    }

}
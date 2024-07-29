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


        public List<Category> GetAllCategory()
        {
            return _db.Category.OrderBy(o => o.Id).ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _db.Category.FirstOrDefault(o => o.Id == id);
        }


        public void AddCategory(Category category)
        {
            _db.Category.Add(category);
            _db.SaveChanges();

        }
        public void RemoveCategory(Category category)
        {
            _db.Category.Remove(_db.Category.FirstOrDefault(m => m.Id == category.Id));
            _db.SaveChanges();

        }

        public void EditCategory(Category category)
        {
            GetCategoryById(category.Id).Name = category.Name;
           
            _db.SaveChanges();
        }


    }

}
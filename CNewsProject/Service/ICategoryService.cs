using Microsoft.AspNetCore.Mvc.Rendering;
using CNewsProject.Models.DataBase;

namespace CNewsProject.Service
{
    public interface ICategoryService
    {
        public List<Category> GetAllCategory();
        public Category GetCategoryById(int id);
        public Category GetCategoryByName(string name);
        public void AddCategory(string name);
        public void RemoveCategory(Category category);
        public void EditCategory(Category category);
        public bool CategoryExists(string name);
    }
}

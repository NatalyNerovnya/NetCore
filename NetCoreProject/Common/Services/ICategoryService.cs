using Common.Models;
using System.Collections.Generic;

namespace Common.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategories();
    }
}

using Common.EntityFramework;
using System.Collections.Generic;

namespace Common.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Categories> GetAllCategories();
    }
}

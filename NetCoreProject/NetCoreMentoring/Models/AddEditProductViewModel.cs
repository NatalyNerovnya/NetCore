using NetCoreProject.Common;
using System.Collections.Generic;

namespace NetCoreMentoring.Models
{
    public class AddEditProductViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }
        public Product Product { get; set; }
    }
}

using System.Collections.Generic;
using Common.EntityFramework;
using Product = Common.Models.Product;

namespace NetCoreMentoring.Models
{
    public class AddEditProductViewModel
    {
        public IEnumerable<Categories> Categories { get; set; }
        public IEnumerable<Suppliers> Suppliers { get; set; }
        public Product Product { get; set; }
    }
}

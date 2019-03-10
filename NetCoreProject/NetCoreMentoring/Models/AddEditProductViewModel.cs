using System.Collections.Generic;
using Common.EntityFramework;
using Product = Common.Models.Product;

namespace NetCoreMentoring.Models
{
    public class AddEditProductViewModel
    {
        //TODO: use DB entity models on UI is bad practice
        public IEnumerable<Categories> Categories { get; set; }
        public IEnumerable<Suppliers> Suppliers { get; set; }
        public Product Product { get; set; }
    }
}

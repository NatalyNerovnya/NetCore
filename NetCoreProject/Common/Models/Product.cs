using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please fill the Name")]
        public string Name { get; set; }
        public string SupplierName { get; set; }
        public string CategoryName { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        [StringLength(20, ErrorMessage = "Only 20 symbols are allowed for free")]
        public string QuantityPerUnit { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Unit price can't be negative")]
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    //TODO: Remove data annotations, better use Fluent Validation
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

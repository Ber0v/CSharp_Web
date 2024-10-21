using System.ComponentModel.DataAnnotations;
using static DeskMarket.Constants.ModelConstants;

namespace DeskMarket.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength), MinLength(CategoryNameMinLength)]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}

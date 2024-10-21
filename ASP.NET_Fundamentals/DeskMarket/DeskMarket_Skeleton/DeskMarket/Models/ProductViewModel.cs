using DeskMarket.Data.Models;
using System.ComponentModel.DataAnnotations;
using static DeskMarket.Constants.ModelConstants;


namespace DeskMarket.Models
{
    public class ProductViewModel
    {
        public int Id { get; internal set; }

        [Required]
        [MaxLength(NameMaxLength), MinLength(NameMinLength)]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [Range((double)PriceMinValue, (double)PriceMaxValue)]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength), MinLength(DescriptionMinLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string AddedOn { get; set; } = DateTime.Today.ToString("dd-MM-yyyy");

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string SellerId { get; set; }

        public List<Category> Categories { get; set; } = new List<Category>();
    }
}

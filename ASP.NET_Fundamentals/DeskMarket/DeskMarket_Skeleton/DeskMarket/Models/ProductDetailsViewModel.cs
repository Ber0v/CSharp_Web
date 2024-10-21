using System.ComponentModel.DataAnnotations;
using static DeskMarket.Constants.ModelConstants;


namespace DeskMarket.Models
{
    public class ProductDetailsViewModel
    {
        public int Id { get; set; }

        [MaxLength(NameMaxLength), MinLength(NameMinLength)]
        public required string ProductName { get; set; }

        public string? ImageUrl { get; set; }

        [Range((double)PriceMinValue, (double)PriceMaxValue)]
        public decimal Price { get; set; }

        [MaxLength(DescriptionMaxLength), MinLength(DescriptionMinLength)]
        public required string Description { get; set; }

        public string CategoryName { get; set; }

        public bool HasBought { get; set; }

        public required string AddedOn { get; set; } = DateTime.Today.ToString("dd-MM-yyyy");

        public required string Category { get; set; }

        public required string Seller { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static DeskMarket.Constants.ModelConstants;

namespace DeskMarket.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength), MinLength(NameMinLength)]
        public string ProductName { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength), MinLength(DescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        [Range((double)PriceMinValue, (double)PriceMaxValue)]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public string SellerId { get; set; }

        [Required]
        public IdentityUser Seller { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime AddedOn { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public Category Category { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<ProductClient> ProductsClients { get; set; } = new List<ProductClient>();
    }

}

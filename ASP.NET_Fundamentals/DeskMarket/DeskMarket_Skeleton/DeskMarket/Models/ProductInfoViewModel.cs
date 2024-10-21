using System.ComponentModel.DataAnnotations;

namespace DeskMarket.Models
{
    public class ProductInfoViewModel
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public bool IsSeller { get; set; }

        [Required]
        public bool HasBought { get; set; }

        public string Category { get; internal set; }

        public string Seller { get; internal set; }

        public string AddedOn { get; internal set; }
    }
}

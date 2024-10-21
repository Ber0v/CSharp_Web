using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DeskMarket.Data.Models
{
    public class ProductClient
    {
        [Key]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Key]
        public string ClientId { get; set; }
        public IdentityUser Client { get; set; }
    }

}

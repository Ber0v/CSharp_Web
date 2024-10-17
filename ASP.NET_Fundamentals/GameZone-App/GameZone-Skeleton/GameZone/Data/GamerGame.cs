using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GameZone.Data
{
    public class GamerGame
    {

        [Required]
        public int GameId { get; set; }

        public Game Game { get; set; }

        [Required]
        public string GamerId { get; set; }

        public IdentityUser Gamer { get; set; }
    }
}

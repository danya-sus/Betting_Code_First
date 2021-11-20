using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betting.Data.Models
{
    public class Team
    {
        public Team()
        {
            this.Players = new HashSet<Player>();
            this.HomeGames = new HashSet<Game>();
            this.AwayGames = new HashSet<Game>();
        }

        [Key]
        public int TeamId { get; set; }

        [Required]
        public decimal Budjet { get; set; }

        [Required]
        public string Name { get; set; }
        public string Initials { get; set; }

        [Required, Column(TypeName = "varchar(max)")]
        public string LogoUrl { get; set; }

        [ForeignKey("PrimaryKitColorID")]
        public int? PrimaryKitColorId { get; set; }
        public virtual Color PrimaryColor { get; set; }

        [ForeignKey("SecondaryKitColorID")]
        public int? SecondaryKitColorId { get; set; }
        public virtual Color SecondaryColor { get; set; }

        [ForeignKey("TownID")]
        public int? TownId { get; set; }
        public virtual Town Town { get; set; }

        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<Game> HomeGames { get; set; }
        public virtual ICollection<Game> AwayGames { get; set; }
    }
}

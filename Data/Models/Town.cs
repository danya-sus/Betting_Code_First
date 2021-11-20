using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betting.Data.Models
{
    public class Town
    {
        public Town()
        {
            this.Teams = new HashSet<Team>();
        }

        [Key]
        public int TownId { get; set; }

        [ForeignKey("CountryID")]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        [Required, MaxLength(64)]
        public string Name { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betting.Data.Models
{
    public class Player
    {
        public Player()
        {
            this.Statistics = new HashSet<PlayerStatistic>();
        }

        [Key]
        public int PlayerId { get; set; }

        [Required, MaxLength(64)]
        public string Name { get; set; }

        [ForeignKey("PositionID")]
        public int? PositionId { get; set; }
        public virtual Position Position { get; set; }

        public int SquadNumber { get; set; }

        [ForeignKey("TeamId")]
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }

        public virtual ICollection<PlayerStatistic> Statistics { get; set; }
    }
}

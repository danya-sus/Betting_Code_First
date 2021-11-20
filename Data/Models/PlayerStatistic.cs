using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betting.Data.Models
{
    public class PlayerStatistic
    {
        [ForeignKey("PlayerID")]
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }

        [ForeignKey("GameID")]
        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        public int Assists { get; set; }

        public decimal MinutesPlayed { get; set; }

        public int ScoredGoals { get; set; }
    }
}

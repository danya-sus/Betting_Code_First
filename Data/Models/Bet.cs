using Betting.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betting.Data.Models
{
    public class Bet
    {
        [Key]
        public int BetId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [ForeignKey("GameID")]
        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        [Required]
        public PredictionType Prediction { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [ForeignKey("UserID")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}

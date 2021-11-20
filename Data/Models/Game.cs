using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Betting.Data.Models.Enums;

namespace Betting.Data.Models
{
    public class Game
    {
        public Game()
        {
            this.Bets = new HashSet<Bet>();
            this.Statistics = new HashSet<PlayerStatistic>();
        }

        [Key]
        public int GameId { get; set; }

        [Required]
        public decimal AwayTeamBetRate { get; set; }

        [Required]
        public int AwayTeamGoals { get; set; }

        [ForeignKey("AwayTeamID")]
        public int AwayTeamId { get; set; }
        public virtual Team AwayTeam { get; set; }

        [Required]
        public decimal DrawBetRate { get; set; }

        [Required]
        public decimal HomeTeamBetRate { get; set; }

        [Required]
        public int HomeTeamGoals { get; set; }

        [ForeignKey("HomeTeamID")]
        public int HomeTeamId { get; set; }
        public virtual Team HomeTeam { get; set; }

        public PredictionType Result { get; set; }

        [Required]
        public DateTime dateTime { get; set; }

        public virtual ICollection<Bet> Bets { get; set; }
        public virtual ICollection<PlayerStatistic> Statistics { get; set; }
    }
}

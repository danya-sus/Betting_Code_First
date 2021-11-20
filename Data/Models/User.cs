using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betting.Data.Models
{
    public class User
    {
        public User()
        {
            this.Bets = new HashSet<Bet>();
        }

        [Key]
        public int UserId { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [Required, MaxLength(64)]
        public string Email { get; set; }

        [Required, MaxLength(128)]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required, MaxLength(128)]
        public string Username { get; set; }

        public virtual ICollection<Bet> Bets { get; set; }
    }
}

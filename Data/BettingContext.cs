using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Betting.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Betting.Data
{
    public class BettingContext : DbContext
    {
        public BettingContext()
        {

        }
        public BettingContext(DbContextOptions contextOptions) : base(contextOptions)
        {

        }

        public virtual DbSet<Bet> Bets {get;set;}
        public virtual DbSet<Color> Colors {get;set;}
        public virtual DbSet<Country> Countries {get;set;}
        public virtual DbSet<Game> Games {get;set;}
        public virtual DbSet<Player> Players {get;set;}
        public virtual DbSet<PlayerStatistic> PlayerStatistics {get;set;}
        public virtual DbSet<Position> Positions {get;set;}
        public virtual DbSet<Team> Teams {get;set;}
        public virtual DbSet<Town> Towns {get;set;}
        public virtual DbSet<User> Users {get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.;Database=BettingTest;Integrated Security=True;");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bet>(entity =>
            {
                entity.HasKey(e => e.BetId);
                entity.Property(e => e.BetId).HasColumnName("BetID");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)")
                    .HasColumnName("Amount");

                entity.Property(e => e.DateTime).HasColumnType("smalldatetime");
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.HasKey(e => e.ColorId);
                entity.Property(e => e.ColorId).HasColumnName("ColorID");

                entity.Property(e => e.Name).HasColumnName("Name")
                    .HasMaxLength(64)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasMany(c => c.PrimaryKitTeams)
                    .WithOne(t => t.PrimaryColor)
                    .HasForeignKey(t => t.PrimaryKitColorId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_PColors_PTeams");

                entity.HasMany(c => c.SecondaryKitTeams)
                    .WithOne(t => t.SecondaryColor)
                    .HasForeignKey(t => t.SecondaryKitColorId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_SColors_STeams");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.CountryId);
                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.Name).HasColumnName("Name")
                    .HasMaxLength(64)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasMany(c => c.Towns)
                    .WithOne(t => t.Country)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasForeignKey(t => t.CountryId)
                    .HasConstraintName("FK_Countries_Towns");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasKey(e => e.GameId);
                entity.Property(e => e.GameId).HasColumnName("GameID");

                entity.Property(e => e.AwayTeamBetRate).HasColumnType("decimal(18, 2)")
                    .HasColumnName("AwayTeamBetRate")
                    .IsRequired();

                entity.Property(e => e.AwayTeamGoals).HasColumnType("int")
                    .HasColumnName("AwayTeamGoals")
                    .IsRequired();

                entity.Property(e => e.HomeTeamBetRate).HasColumnType("decimal(18, 2)")
                    .HasColumnName("HomeTeamBetRate")
                    .IsRequired();

                entity.Property(e => e.HomeTeamGoals).HasColumnType("int")
                    .HasColumnName("HomeTeamGoals")
                    .IsRequired();

                entity.Property(e => e.dateTime).HasColumnType("smalldatetime")
                    .HasColumnName("DateTime")
                    .IsRequired();

                entity.HasMany(g => g.Bets)
                    .WithOne(b => b.Game)
                    .HasForeignKey(b => b.GameId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Games_Bets");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(e => e.PlayerId);
                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

                entity.Property(e => e.Name).HasColumnName("Name")
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.SquadNumber).HasColumnType("int")
                    .HasColumnName("SquadNumber");
            });

            modelBuilder.Entity<PlayerStatistic>(entity =>
            {
                entity.HasKey(sc => new { sc.GameId, sc.PlayerId });
                entity.Property(e => e.GameId).HasColumnName("GameID");
                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

                entity.HasOne(sc => sc.Game)
                    .WithMany(g => g.Statistics)
                    .HasForeignKey(sc => sc.GameId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_PlayerStatistics_Games");

                entity.HasOne(sc => sc.Player)
                    .WithMany(p => p.Statistics)
                    .HasForeignKey(sc => sc.PlayerId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_PlayerStatistics_Players");

                entity.Property(e => e.Assists).HasColumnType("int")
                    .HasColumnName("Assists");

                entity.Property(e => e.MinutesPlayed).HasColumnType("decimal(18, 2)")
                    .HasColumnName("MinutesPlayed");

                entity.Property(e => e.ScoredGoals).HasColumnType("int")
                    .HasColumnName("ScoredGoals");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(e => e.PositionId);
                entity.Property(e => e.PositionId).HasColumnName("PositionID");

                entity.Property(e => e.Name).HasColumnName("Name")
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasMany(po => po.Players)
                    .WithOne(pl => pl.Position)
                    .HasForeignKey(pl => pl.PositionId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Positions_Players");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.TeamId);
                entity.Property(e => e.TeamId).HasColumnName("TeamID");

                entity.Property(e => e.Budjet).HasColumnType("decimal(18, 2)")
                    .HasColumnName("Budjet")
                    .IsRequired();

                entity.Property(e => e.Initials).HasColumnName("Initials");

                entity.Property(e => e.LogoUrl).HasColumnType("varchar(max)")
                    .HasColumnName("LogoUrl")
                    .IsRequired();

                entity.Property(e => e.Name).HasColumnName("Name")
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasMany(t => t.AwayGames)
                    .WithOne(g => g.AwayTeam)
                    .HasForeignKey(g => g.AwayTeamId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_ATeams_AGames");

                entity.HasMany(t => t.HomeGames)
                    .WithOne(g => g.HomeTeam)
                    .HasForeignKey(g => g.HomeTeamId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_HTeams_MGames");

                entity.HasMany(t => t.Players)
                    .WithOne(p => p.Team)
                    .HasForeignKey(p => p.TeamId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Teams_Players");
            });

            modelBuilder.Entity<Town>(entity =>
            {
                entity.HasKey(e => e.TownId);
                entity.Property(e => e.TownId).HasColumnName("TownID");

                entity.Property(e => e.Name)
                    .HasMaxLength(64)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasMany(to => to.Teams)
                    .WithOne(te => te.Town)
                    .HasForeignKey(te => te.TownId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Towns_Teams");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)")
                    .HasColumnName("Balance")
                    .IsRequired();

                entity.Property(e => e.Email).HasColumnName("Email")
                    .HasMaxLength(64)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasColumnName("Name")
                    .HasMaxLength(128)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Password).HasColumnName("Password")
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Username).HasColumnName("Username")
                    .HasMaxLength(128)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasMany(u => u.Bets)
                    .WithOne(b => b.User)
                    .HasForeignKey(b => b.UserId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Users_Bets");
            });
        }
    }
}

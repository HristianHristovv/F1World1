using F1_World.Models;
using F1_World;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace F1World2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Circuit
            modelBuilder.Entity<Circuit>()
                .HasKey(c => c.CircuitId);

            // Configure Pilot
            modelBuilder.Entity<Pilot>()
                .HasKey(p => p.PilotId);

            modelBuilder.Entity<Pilot>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Pilots)
                .HasForeignKey(p => p.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Race
            modelBuilder.Entity<Race>()
                .HasKey(r => r.RaceId);

            modelBuilder.Entity<Race>()
                .HasMany(r => r.RaceResults)
                .WithOne(rr => rr.Race)
                .HasForeignKey(rr => rr.RaceId);

            // Configure RaceResult
            modelBuilder.Entity<RaceResult>()
                .HasKey(rr => rr.RaceResultId);

            modelBuilder.Entity<RaceResult>()
                .HasOne(rr => rr.Race)
                .WithMany(r => r.RaceResults)
                .HasForeignKey(rr => rr.RaceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RaceResult>()
                .HasOne(rr => rr.Pilot)
                .WithMany()
                .HasForeignKey(rr => rr.PilotId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Season
            modelBuilder.Entity<Season>()
                .HasKey(s => s.SeasonId);

            modelBuilder.Entity<Season>()
                .HasOne(s => s.ChampionPilot)
                .WithMany()
                .HasForeignKey(s => s.ChampionPilotId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Season>()
                .HasOne(s => s.ChampionTeam)
                .WithMany()
                .HasForeignKey(s => s.ChampionTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Team
            modelBuilder.Entity<Team>()
                .HasKey(t => t.TeamId);

            // Configure any additional fields or properties
            modelBuilder.Entity<Team>()
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);
        }

        // Define DbSets for each entity
        public DbSet<Circuit> Circuits { get; set; }
        public DbSet<Pilot> Pilots { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<RaceResult> RaceResults { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}

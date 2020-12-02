using Microsoft.EntityFrameworkCore;
using Server_PHP_For_Business.Models;

namespace Server_PHP_For_Business.Data
{
  public class CommanderContext : DbContext
  {
    public CommanderContext(DbContextOptions<CommanderContext> opt) : base(opt)
    {

    }

    public DbSet<Command> Commands { get; set; }
    public DbSet<Bracelet> Bracelets { get; set; }
    public DbSet<Hall> Halls { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Business> Businesses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      //User & Bracelet one to one
      // modelBuilder.Entity<User>()
      //   .HasOne(u => u.Bracelet)
      //   .WithOne(b => b.User)
      //   .HasForeignKey<Bracelet>(b => b.UserId)
      //   .OnDelete(DeleteBehavior.SetNull);

      modelBuilder.Entity<Bracelet>()
        .HasOne(b => b.User)
        .WithOne(u => u.Bracelet)
        .HasForeignKey<User>(u => u.BraceletId)
        .OnDelete(DeleteBehavior.SetNull);

      // Business & Hall one to many
      modelBuilder.Entity<Business>()
        .HasMany(b => b.Halls)
        .WithOne(h => h.Business)
        .HasForeignKey(h => h.BusinessId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);

      //Saving Seats list as JSON string
      modelBuilder.Entity<Hall>()
        .Property(h => h._Seats)
        .HasColumnName("Seats");
    }
  }
}

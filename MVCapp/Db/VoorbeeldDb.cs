using Microsoft.EntityFrameworkCore;
using MVCapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCapp.Db
{
    public class VoorbeeldDb : DbContext
    {
        public VoorbeeldDb(DbContextOptions<VoorbeeldDb> options)
        : base(options)
        {
        }

        // Voeg een DbSet toe voor Organisator (Evenementen)
        public DbSet<Organisator> Evenementen { get; set; }

        public DbSet<Voorbeeld> Voorbeelden { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) // Zorg ervoor dat je dit toevoegt zodat de connectionstring niet opnieuw wordt geconfigureerd als die al is ingesteld
            {
                string connection = @"Data Source=.;Initial Catalog=MVCapp;Integrated Security=true;Trust Server Certificate=True;";
                optionsBuilder.UseSqlServer(connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Voeg specificaties en constraints toe aan het Organisator model
            modelBuilder.Entity<Organisator>()
                .Property(e => e.Title)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Organisator>()
                .Property(e => e.Location)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<Organisator>()
                .Property(e => e.Description)
                .HasMaxLength(500)
                .IsRequired();

            // Specificeer andere constraints voor het Organisator model indien nodig

            // Seed data voor het Voorbeeld model (zoals je al had)
            Voorbeeld voorbeeldEntity = new Voorbeeld()
            {
                Id = 1,
                Name = "Test"
            };
            modelBuilder.Entity<Voorbeeld>().HasData(voorbeeldEntity);
        }
    }
}

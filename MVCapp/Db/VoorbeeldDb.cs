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
        public DbSet<Voorbeeld> Voorbeelden { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connection = @"Data Source=.;Initial Catalog=testdbweek2;Integrated Security=true;Trust Server Certificate=True;";
            optionsBuilder.UseSqlServer(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //specificeer
            modelBuilder.Entity<Voorbeeld>()
                .Property(v => v.Name)
                .HasMaxLength(30);

            //data seed
            Voorbeeld voorbeeldEntity = new Voorbeeld()
            {
                Id = 1,
                Name = "Test"
            };
            modelBuilder.Entity<Voorbeeld>()
                .HasData(voorbeeldEntity);
        }

    }
}

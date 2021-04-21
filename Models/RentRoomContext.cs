using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentRoom.Models
{
    public class RentRoomContext : DbContext
    {
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RentRoomDB;Trusted_Connection=True;");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Build> Builds { get; set; }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfessorsSSU.Models;


namespace ProfessorsSSU.Data
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=professors.db");
        }

        public DbSet<Editor> Editors { get; set; }
        public DbSet<Professor> Professors { get; set; }
    }
}

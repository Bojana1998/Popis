using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Popis.Models;
using Popis.ViewModel;

namespace Popis.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly DbContextOptions _options;
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }
        public DbSet<Korisnik> Korisnik { get; set; }

        public DbSet<Lokacija> Lokacija { get; set; }

        public DbSet<Sredstvo> Sredstvo { get; set; }

        public DbSet<Inventar> Inventar { get; set; }

        

       
       
    }
}

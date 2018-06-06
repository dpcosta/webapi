﻿using Alura.WebAPI.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Alura.WebAPI.API.Data
{
    public class LeituraContext : DbContext
    {
        public DbSet<Livro> Livros { get; set; }

        public LeituraContext(DbContextOptions<LeituraContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration<Livro>(new LivroConfiguration());
        }
    }
}

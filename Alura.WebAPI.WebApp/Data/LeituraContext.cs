using Alura.WebAPI.Model;
using Alura.WebAPI.WebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Alura.WebAPI.WebApp.Data
{
    public class LeituraContext : IdentityDbContext<Usuario>
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
            modelBuilder.ApplyConfiguration<Usuario>(new UsuarioConfiguration());
        }
    }
}

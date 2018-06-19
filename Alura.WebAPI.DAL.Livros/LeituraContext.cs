using Alura.ListaLeitura.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Alura.ListaLeitura.Persistencia
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

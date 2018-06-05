using Alura.WebAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alura.WebAPI.WebApp.Data
{
    internal class LivroConfiguration : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder
                .Property(l => l.Titulo)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder
                .Property(l => l.Subtitulo)
                .HasColumnType("nvarchar(75)");

            builder
                .Property(l => l.Resumo)
                .HasColumnType("nvarchar(500)");

            builder
                .Property(l => l.Autor)
                .HasColumnType("nvarchar(75)");

            builder
                .Property(l => l.ImagemCapa);

            builder
                .Property(l => l.Lista)
                .HasColumnType("nvarchar(10)")
                .IsRequired()
                .HasConversion<string>(
                    tipo => tipo.ParaString(),
                    texto => texto.ParaTipo()
                );

            builder.HasData(
                new Livro { Id = 1, Titulo = "Harry Potter 1", Subtitulo = "E a Pedra Filosofal", Autor = "J.K. Rowling", Lista = TipoListaLeitura.ParaLer },
                new Livro { Id = 2, Titulo = "Harry Potter 2", Subtitulo = "E a Câmara Secreta", Autor = "J.K. Rowling", Lista = TipoListaLeitura.ParaLer },
                new Livro { Id = 3, Titulo = "Harry Potter 3", Subtitulo = "E o Prisioneiro de Askaban", Autor = "J.K. Rowling", Lista = TipoListaLeitura.ParaLer },
                new Livro { Id = 4, Titulo = "Harry Potter 4", Subtitulo = "E o Cálice Sagrado", Autor = "J.K. Rowling", Lista = TipoListaLeitura.ParaLer }
            );

        }
    }
}
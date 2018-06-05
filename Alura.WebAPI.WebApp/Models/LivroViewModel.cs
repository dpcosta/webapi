using Alura.WebAPI.Model;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Alura.WebAPI.WebApp.Models
{

    public static class LivroViewModelExtensions
    {
        public static byte[] ConvertToBytes(IFormFile image)
        {
            using (var inputStream = image.OpenReadStream())
            using (var stream = new MemoryStream())
            {
                inputStream.CopyTo(stream);
                return stream.ToArray();
            }
        }

        public static LivroViewModel ToModel(this Livro livro)
        {
            return new LivroViewModel
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Subtitulo = livro.Subtitulo,
                Resumo = livro.Resumo,
                Autor = livro.Autor,
                Lista = livro.Lista
            };
        }

        public static Livro ToLivro(this LivroViewModel model)
        {
            return new Livro
            {
                Id = model.Id,
                Titulo = model.Titulo,
                Subtitulo = model.Subtitulo,
                Resumo = model.Resumo,
                Autor = model.Autor,
                ImagemCapa = ConvertToBytes(model.Capa),
                Lista = model.Lista
            };
        }
    }

    public class LivroViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public string Autor { get; set; }
        public string Resumo { get; set; }
        public IFormFile Capa { get; set; }
        public TipoListaLeitura Lista { get; set; }
    }
}

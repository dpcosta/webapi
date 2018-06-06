using Alura.WebAPI.Model;
using System.Xml.Serialization;

namespace Alura.WebAPI.API.Models
{
    public static class LivroApiExtensions
    {
        public static LivroApi ToApi(this Livro livro)
        {
            return new LivroApi
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Subtitulo = livro.Subtitulo,
                Resumo = livro.Resumo,
                Autor = livro.Autor,
                Capa = $"/api/capas/{livro.Id}",
                Lista = livro.Lista.ParaString()
            };
        }
    }

    [XmlType("Livro")]
    public class LivroApi
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public string Autor { get; set; }
        public string Resumo { get; set; }
        public string Capa { get; set; }
        public string Lista { get; set; }
    }
}

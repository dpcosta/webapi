using System.ComponentModel.DataAnnotations;

namespace Alura.WebAPI.Model
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public string Resumo { get; set; }
        public string Capa { get; set; }
        public string Autor { get; set; }
        public TipoListaLeitura Lista { get; set; }
    }
}

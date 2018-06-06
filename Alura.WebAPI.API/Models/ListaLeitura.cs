using System.Collections.Generic;

namespace Alura.WebAPI.API.Models
{
    public class ListaLeitura
    {
        public string Tipo { get; set; }
        public IEnumerable<LivroApi> Livros { get; set; }
    }
}

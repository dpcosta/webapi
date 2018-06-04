using Alura.WebAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.WebAPI.WebApp.Models
{
    public class ListaLeitura
    {
        public TipoListaLeitura Tipo { get; set; }
        public IEnumerable<Livro> Livros { get; set; }
    }
}

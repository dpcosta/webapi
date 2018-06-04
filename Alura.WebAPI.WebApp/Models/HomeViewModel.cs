using Alura.WebAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.WebAPI.WebApp.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Livro> ParaLer { get; set; }
        public IEnumerable<Livro> Lendo { get; set; }
        public IEnumerable<Livro> Lidos { get; set; }
    }
}

using Alura.WebAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.WebAPI.WebApp.Models
{
    public class HomeViewModel
    {
        public IEnumerable<LivroApi> ParaLer { get; set; }
        public IEnumerable<LivroApi> Lendo { get; set; }
        public IEnumerable<LivroApi> Lidos { get; set; }
    }
}

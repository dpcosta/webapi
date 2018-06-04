using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alura.WebAPI.Model;
using Alura.WebAPI.WebApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace Alura.WebAPI.WebApp.Controllers
{
    public class ListaLeituraController : Controller
    {
        private readonly IRepository<Livro> _repo;

        public ListaLeituraController(IRepository<Livro> repository)
        {
            _repo = repository;
        }

        private IEnumerable<Livro> LivrosDoTipo(TipoListaLeitura tipo)
        {
            return _repo
                .All
                .Where(l => l.Lista == tipo)
                .ToList();
        }

        public IActionResult ParaLer()
        {
            var lista = LivrosDoTipo(TipoListaLeitura.ParaLer);
            return Ok(lista);
        }

        public IActionResult Lidos()
        {
            var lista = LivrosDoTipo(TipoListaLeitura.Lidos);
            return Ok(lista);
        }

        public IActionResult Lendo()
        {
            var lista = LivrosDoTipo(TipoListaLeitura.Lendo);
            return Ok(lista);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alura.WebAPI.Model;
using Alura.WebAPI.WebApp.Data;
using Alura.WebAPI.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alura.WebAPI.WebApp.Controllers
{
    [Authorize]
    [Route("[controller]")]
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

        private ListaLeitura CreateFor(TipoListaLeitura tipo)
        {
            return new ListaLeitura
            {
                Tipo = tipo,
                Livros = LivrosDoTipo(tipo)
            };
        }

        [HttpGet]
        public IActionResult Get()
        {
            var paraLer = CreateFor(TipoListaLeitura.ParaLer);
            var lendo = CreateFor(TipoListaLeitura.Lendo);
            var lidos = CreateFor(TipoListaLeitura.Lidos);
            return Ok(new List<ListaLeitura> { paraLer, lendo, lidos });
        }

        [HttpGet("{tipo}")]
        public IActionResult Get(TipoListaLeitura tipo)
        {
            var lista = new ListaLeitura
            {
                Tipo = tipo,
                Livros = LivrosDoTipo(tipo)
            };
            return Ok(lista);
        }
    }
}
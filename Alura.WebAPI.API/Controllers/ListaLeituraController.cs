using System.Collections.Generic;
using System.Linq;
using Alura.ListaLeitura.Modelos;
using Alura.ListaLeitura.Persistencia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alura.ListaLeitura.Api.Livros
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class ListaLeituraController : Controller
    {
        private readonly IRepository<Livro> _repo;

        public ListaLeituraController(IRepository<Livro> repository)
        {
            _repo = repository;
        }

        private IEnumerable<LivroApi> LivrosDoTipo(TipoListaLeitura tipo)
        {
            return _repo
                .All
                .Where(l => l.Lista == tipo)
                .Select(l => l.ToApi())
                .ToList();
        }

        private Modelos.ListaLeitura CreateFor(TipoListaLeitura tipo)
        {
            return new Modelos.ListaLeitura
            {
                Tipo = tipo.ParaString(),
                Livros = LivrosDoTipo(tipo)
            };
        }

        [HttpGet]
        public IActionResult Get()
        {
            var paraLer = CreateFor(TipoListaLeitura.ParaLer);
            var lendo = CreateFor(TipoListaLeitura.Lendo);
            var lidos = CreateFor(TipoListaLeitura.Lidos);
            return Ok(new List<Modelos.ListaLeitura> { paraLer, lendo, lidos });
        }

        [HttpGet("{tipo}")]
        public IActionResult Get(TipoListaLeitura tipo)
        {
            var lista = new Modelos.ListaLeitura
            {
                Tipo = tipo.ParaString(),
                Livros = LivrosDoTipo(tipo)
            };
            return Ok(lista);
        }
    }
}
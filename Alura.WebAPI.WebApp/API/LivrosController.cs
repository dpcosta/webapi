using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alura.WebAPI.Model;
using Alura.WebAPI.WebApp.Data;
using Alura.WebAPI.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alura.WebAPI.WebApp.API
{
    //[Authorize]
    [Route("api/[controller]")]
    [FormatFilter]
    public class LivrosController : Controller
    {
        private readonly IRepository<Livro> _repo;

        public LivrosController(IRepository<Livro> repositorio)
        {
            _repo = repositorio;
        }

        [HttpGet()]
        public IActionResult Get()
        {
            var livros = _repo.All
                .Select(l => l.ToApi())
                .ToList();
            return Ok(livros);
        }

        [HttpGet("{id}.{format?}")]
        public IActionResult Get(int id)
        {
            var livro = _repo.Find(id);
            if (livro == null)
            {
                return NotFound();
            }
            return Ok(livro.ToApi());
        }
    }
}
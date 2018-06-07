using System.Linq;
using Alura.WebAPI.API.Data;
using Alura.WebAPI.API.Models;
using Alura.WebAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alura.WebAPI.WebApp.API
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
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

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var livro = _repo.Find(id);
            if (livro == null)
            {
                return NotFound();
            }
            return Ok(livro.ToApi());
        }

        [HttpPost]
        public IActionResult Post([FromBody] LivroCapaAsMultipartData model)
        {
            if (ModelState.IsValid)
            {
                var livro = model.ToLivro();
                _repo.Incluir(livro);
                var uri = Url.Action("Get", new { Id = livro.Id });
                return Created(uri, livro);
            }
            return BadRequest();
        }

        [HttpPut]
        public IActionResult Put([FromBody] LivroCapaAsMultipartData model)
        {
            if (ModelState.IsValid)
            {
                var livro = model.ToLivro();
                _repo.Alterar(livro);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var livro = _repo.Find(id);
            if (livro != null)
            {
                _repo.Excluir(livro);
                return NoContent();
            }
            return NotFound();
        }
    }
}
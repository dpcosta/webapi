using System.IO;
using System.Linq;
using Alura.ListaLeitura.Modelos;
using Alura.ListaLeitura.Persistencia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alura.ListaLeitura.Api.Livros
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

        private byte[] ConvertToBytes(IFormFile file)
        {
            if (file == null)
            {
                return null;
            }
            using (var inputStream = file.OpenReadStream())
            using (var stream = new MemoryStream())
            {
                inputStream.CopyTo(stream);
                return stream.ToArray();
            }
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
        public IActionResult Post([FromForm] LivroUpload model)
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
        public IActionResult Put([FromForm] LivroUpload model)
        {
            if (ModelState.IsValid)
            {
                var livro = _repo.Find(model.Id);
                livro.Titulo = model.Titulo;
                livro.Subtitulo = model.Subtitulo;
                livro.Resumo = model.Resumo;
                livro.Autor = model.Autor;
                livro.Lista = model.Lista;
                if (model.Capa != null)
                {
                    livro.ImagemCapa = ConvertToBytes(model.Capa);
                }
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
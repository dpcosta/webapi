using System.Linq;
using Alura.ListaLeitura.Modelos;
using Alura.ListaLeitura.Persistencia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alura.ListaLeitura.Api.Livros
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class CapasController : Controller
    {
        private readonly IRepository<Livro> _repo;

        public CapasController(IRepository<Livro> repositorio)
        {
            _repo = repositorio;
        }

        [HttpGet("{livroId}")]
        [Produces("image/png")]
        public IActionResult Get(int livroId)
        {
            byte[] img = _repo.All
                .Where(l => l.Id == livroId)
                .Select(l => l.ImagemCapa)
                .FirstOrDefault();
            if (img != null)
            {
                return File(img, "image/png");
            }
            return File("~/images/capas/capa-vazia.png", "image/png");
        }

        //[AllowAnonymous]
        [HttpPost("{livroId}")]
        public IActionResult Post(int livroId)
        {
            if (livroId == 123)
            {
                throw new System.Exception("Teste de tratamento de erros na API...");
            }
            return Ok(livroId);
        }
    }
}
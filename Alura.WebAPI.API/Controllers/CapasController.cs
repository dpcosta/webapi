using System.Diagnostics;
using System.IO;
using System.Linq;
using Alura.WebAPI.API.Data;
using Alura.WebAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Alura.WebAPI.API.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class CapasController : Controller
    {
        private readonly IRepository<Livro> _repo;
        private readonly ILogger _logger;

        public CapasController(IRepository<Livro> repositorio, ILogger<CapasController> logger)
        {
            _repo = repositorio;
            _logger = logger;
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
    }
}
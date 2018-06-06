﻿using System.Linq;
using Alura.WebAPI.API.Data;
using Alura.WebAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alura.WebAPI.API.Controllers
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
    }
}
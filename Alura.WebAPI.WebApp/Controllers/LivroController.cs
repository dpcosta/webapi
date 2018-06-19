﻿using System.Threading.Tasks;
using Alura.ListaLeitura.Modelos;
using Alura.ListaLeitura.Servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alura.WebAPI.WebApp.Controllers
{
    [Authorize]
    public class LivroController : Controller
    {
        private readonly LivrosService _service;

        public LivroController(LivrosService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Novo()
        {
            return View(new LivroUpload());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Novo(LivroUpload model)
        {
            if (ModelState.IsValid)
            {
                await _service.NovoLivroAsync(model.ToLivro());
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ImagemCapa(int id)
        {
            byte[] img = await _service.GetCapaAsync(id);
            if (img != null)
            {
                return File(img, "image/png");
            }
            return File("~/images/capas/capa-vazia.png", "image/png");
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(int id)
        {
            var livro = await _service.GetLivroAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            return View(livro.ToModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Detalhes(LivroUpload model)
        {
            if (ModelState.IsValid)
            {
                await _service.AtualizaLivroAsync(model.ToLivro());
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remover(int id)
        {
            var model = await _service.GetLivroAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            await _service.RemoveLivroAsync(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
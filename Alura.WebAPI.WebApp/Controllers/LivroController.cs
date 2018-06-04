using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Alura.WebAPI.Model;
using Alura.WebAPI.WebApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Alura.WebAPI.WebApp.Controllers
{
    [Authorize]
    public class LivroController : Controller
    {
        private readonly IRepository<Livro> _repo;
        private readonly IHostingEnvironment _environment;

        public LivroController(IRepository<Livro> repository, IHostingEnvironment environment)
        {
            _repo = repository;
            _environment = environment;
        }

        private IEnumerable<string> Capas
        {
            get
            {
                var diretorio = Path.Combine(_environment.WebRootPath, "images/capas");
                var dirInfo = new DirectoryInfo(diretorio);
                var lista = new List<string>();
                foreach (var capa in dirInfo.GetFiles())
                {
                    lista.Add(capa.Name);
                }
                return lista;
            }
        }

        [HttpGet]
        public IActionResult Novo()
        {
            ViewData["Capas"] = this.Capas;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Novo(Livro model)
        {
            ViewData["Capas"] = this.Capas;
            if (ModelState.IsValid)
            {
                _repo.Incluir(model);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Detalhes(int id)
        {
            ViewData["Capas"] = this.Capas;
            var model = _repo.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Detalhes(Livro model)
        {
            ViewData["Capas"] = this.Capas;
            if (ModelState.IsValid)
            {
                _repo.Alterar(model);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remover(int id)
        {
            var model = _repo.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            _repo.Excluir(model);
            return RedirectToAction("Index", "Home");
        }
    }
}
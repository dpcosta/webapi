﻿using Alura.WebAPI.DAL.Livros;
using Alura.WebAPI.Model;
using Alura.WebAPI.WebAppTradicional.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Alura.WebAPI.WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IRepository<Livro> _repo;

        public HomeController(IRepository<Livro> repository)
        {
            _repo = repository;
        }

        private IEnumerable<LivroApi> ListaDoTipo(TipoListaLeitura tipo)
        {
            return _repo.All
                .Where(l => l.Lista == tipo)
                .Select(l => l.ToApi())
                .ToList();
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
                ParaLer = ListaDoTipo(TipoListaLeitura.ParaLer),
                Lendo = ListaDoTipo(TipoListaLeitura.Lendo),
                Lidos = ListaDoTipo(TipoListaLeitura.Lidos)
            };
            return View(model);
        }
    }
}
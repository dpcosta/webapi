using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Alura.WebAPI.Model;
using Alura.WebAPI.Seguranca;
using Alura.WebAPI.Services;

namespace Alura.WebAPI.ConsoleApp
{
    class TokenViaString : ITokenFactory
    {
        public string Token { get; }

        public TokenViaString(string token)
        {
            Token = token;
        }
    }

    class Program
    {
        public static void Main()
        {
            Task.Run(async () => {
                //logar e obter o token
                var authHttpClient = new HttpClient
                {
                    BaseAddress = new Uri("http://localhost:5000/")
                };
                var authService = new ListaLeituraAuthService(authHttpClient);
                var loginModel = new LoginModel
                {
                    Login = "admin",
                    Password = "123"
                };
                var result = await authService.LoginAsync(loginModel);
                if (result.Succeeded)
                {
                    var apiHttpClient = new HttpClient
                    {
                        BaseAddress = new Uri("http://localhost:6000/api/")
                    };
                    var token = new TokenViaString(result.Content.AccessToken);

                    //enviar requisições para pegar as listas de livros usando o token
                    var listaLeituraService = new ListaLeituraService(apiHttpClient, token);
                    var paraLer = await listaLeituraService.GetListaAsync(TipoListaLeitura.ParaLer);
                    var lendo = await listaLeituraService.GetListaAsync(TipoListaLeitura.Lendo);
                    var lidos = await listaLeituraService.GetListaAsync(TipoListaLeitura.Lidos);


                    ImprimeLivros(lista: paraLer, tipo: TipoListaLeitura.ParaLer);
                    ImprimeLivros(lendo, TipoListaLeitura.Lendo);
                    ImprimeLivros(lidos, TipoListaLeitura.Lidos);
                }

            })
                .GetAwaiter()
                .GetResult();
        }

        private static void ImprimeLivros(IEnumerable<LivroApi> lista, TipoListaLeitura tipo)
        {
            Console.WriteLine($"Livros {tipo}");
            Console.WriteLine("===========================");
            foreach (var livro in lista)
            {
                Console.WriteLine($"{livro.Titulo} - {livro.Autor}");
            }
        }
    }
}

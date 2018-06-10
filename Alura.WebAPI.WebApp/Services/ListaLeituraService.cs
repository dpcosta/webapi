using Alura.WebAPI.Model;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Alura.WebAPI.WebApp.Services
{
    public class ListaLeituraService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;
        private string _token => _httpContextAccessor.HttpContext
            .User.Claims.FirstOrDefault(c => c.Type == "Token").Value;

        public ListaLeituraService(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClient = client;
        }

        public async Task<IEnumerable<LivroApi>> GetListaAsync(TipoListaLeitura tipo)
        {
            _httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", _token);
            var response = await _httpClient.GetAsync($"listaleitura/{tipo}");
            //if (response.StatusCode == HttpStatusCode.Unauthorized)
            //{
            //    throw new ListaLeituraUnauthorizedException();
            //}
            var lista = await response.Content.ReadAsAsync<ListaLeitura>();
            return lista.Livros;
        }
    }
}

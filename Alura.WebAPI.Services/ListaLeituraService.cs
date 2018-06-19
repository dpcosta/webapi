using Alura.ListaLeitura.Modelos;
using Alura.ListaLeitura.Seguranca;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.Servicos
{
    public class ListaLeituraService
    {
        private readonly HttpClient _httpClient;
        private readonly string _token;

        public ListaLeituraService(HttpClient client, ITokenFactory factory)
        {
            _httpClient = client;
            _token = factory.Token;
        }

        private void AddBearerToken()
        {
            var authHeader = new AuthenticationHeaderValue("Bearer", _token);
            _httpClient.DefaultRequestHeaders.Authorization = authHeader;
        }

        public async Task<IEnumerable<LivroApi>> GetListaAsync(TipoListaLeitura tipo)
        {
            AddBearerToken();
            var response = await _httpClient.GetAsync($"listaleitura/{tipo}");
            //if (response.StatusCode == HttpStatusCode.Unauthorized)
            //{
            //    throw new ListaLeituraUnauthorizedException();
            //}
            var lista = await response.Content.ReadAsAsync<Modelos.ListaLeitura>();
            return lista.Livros;
        }
    }
}

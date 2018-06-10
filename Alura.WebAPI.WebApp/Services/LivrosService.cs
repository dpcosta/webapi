using Alura.WebAPI.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Alura.WebAPI.WebApp.Services
{
    public class LivrosService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LivrosService(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = client;
            _httpContextAccessor = httpContextAccessor;
        }

        private void AddBearerToken()
        {
            var tokenClaim = _httpContextAccessor.HttpContext.User
                .Claims.FirstOrDefault(c => c.Type == "Token");
            if (tokenClaim != null)
            {
                var authHeader = new AuthenticationHeaderValue("Bearer", tokenClaim.Value);
                _httpClient.DefaultRequestHeaders.Authorization = authHeader;
                return;
            }
            throw new SystemException(); //ver oq fazer aqui!
        }

        private MultipartFormDataContent CreateLivroContent(Livro livro)
        {

            var content = new MultipartFormDataContent();
            content.Add(new StringContent(livro.Titulo), "\"titulo\"");
            content.Add(new StringContent(livro.Lista.ParaString()), "\"lista\"");

            if (livro.Id > 0)
            {
                content.Add(new StringContent(Convert.ToString(livro.Id)), "\"id\"");
            }
            if (!string.IsNullOrEmpty(livro.Subtitulo))
            {
                content.Add(new StringContent(livro.Subtitulo), "\"subtitulo\"");
            }
            if (!string.IsNullOrEmpty(livro.Resumo))
            {
                content.Add(new StringContent(livro.Resumo), "\"resumo\"");
            }
            if (!string.IsNullOrEmpty(livro.Autor))
            {
                content.Add(new StringContent(livro.Autor), "\"autor\"");
            }
            if (livro.ImagemCapa != null)
            {
                var imageContent = new ByteArrayContent(livro.ImagemCapa);
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/png");
                content.Add(imageContent, "\"capa\"", "\"capa.png\"");

            }
            return content;
        }

        public async Task<IEnumerable<Livro>> GetLivrosAsync()
        {
            AddBearerToken();
            var response = await _httpClient.GetAsync("livros");
            response.EnsureSuccessStatusCode();
            return await response
                .Content
                .ReadAsAsync<IEnumerable<Livro>>();
        }

        public async Task<Livro> GetLivroAsync(int id)
        {
            AddBearerToken();
            var response = await _httpClient.GetAsync($"livros/{id}");
            response.EnsureSuccessStatusCode();
            return await response
                .Content
                .ReadAsAsync<Livro>();
        }

        public async Task NovoLivroAsync(Livro livro)
        {
            AddBearerToken();
            var content = CreateLivroContent(livro);
            var response = await _httpClient.PostAsync("livros", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task AtualizaLivroAsync(Livro livro)
        {
            AddBearerToken();
            var content = CreateLivroContent(livro);
            var response = await _httpClient.PutAsync("livros", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task RemoveLivroAsync(int id)
        {
            AddBearerToken();
            var response = await _httpClient.DeleteAsync($"livros/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<byte[]> GetCapaAsync(int id)
        {
            AddBearerToken();
            return await _httpClient.GetByteArrayAsync($"capas/{id}");
        }
    }
}

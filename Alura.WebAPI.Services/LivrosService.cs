using Alura.ListaLeitura.Modelos;
using Alura.ListaLeitura.Seguranca;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.Servicos
{
    public class LivrosService
    {
        private readonly HttpClient _httpClient;
        private readonly string _token;

        public LivrosService(HttpClient client, ITokenFactory factory)
        {
            _httpClient = client;
            _token = factory.Token;
        }

        private void AddBearerToken()
        {
            var authHeader = new AuthenticationHeaderValue("Bearer", _token);
            _httpClient.DefaultRequestHeaders.Authorization = authHeader;
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

using Alura.WebAPI.Model;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Alura.WebAPI.WebApp.Services
{
    public class TokenResult
    {
        public bool Authenticated { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expiration { get; set; }
        public string AccessToken { get; set; }
        public string Message { get; set; }
    }

    public class LoginResult
    {

        public bool Succeeded { get; }
        public TokenResult Content { get; }

        public LoginResult(HttpStatusCode statusCode, TokenResult content)
        {
            Succeeded = (statusCode==HttpStatusCode.OK);
            Content = content;
        }
    }

    public class ListaLeituraAuthService
    {
        private readonly HttpClient _httpClient;

        public ListaLeituraAuthService(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<LoginResult> LoginAsync(LoginModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/login", model);
            return new LoginResult(response.StatusCode, await response.Content.ReadAsAsync<TokenResult>());
        }
    }
}

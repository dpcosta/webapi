using System.Linq;
using Alura.ListaLeitura.Seguranca;
using Microsoft.AspNetCore.Http;

namespace Alura.WebAPI.WebApp.Seguranca
{
    public class TokenViaHttpContext : ITokenFactory
    {
        private readonly IHttpContextAccessor _accessor;

        public TokenViaHttpContext(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Token
        {
            get
            {
                //adicionar código de validação...
                return _accessor.HttpContext
                    .User.Claims
                    .FirstOrDefault(c => c.Type == "Token")
                    .Value;
            }
        }
    }
}

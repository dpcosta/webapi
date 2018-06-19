using Alura.ListaLeitura.Servicos;
using Alura.ListaLeitura.Seguranca;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using Alura.WebAPI.WebApp.Seguranca;

namespace Alura.ListaLeitura.WebApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ITokenFactory, TokenViaHttpContext>();

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    //configurando a página de autenticação qdo requisição for anônima
                    options.LoginPath = "/Usuario/Login";
                    options.ExpireTimeSpan = TimeSpan.FromSeconds(600);
                });

            services.AddMvc(options => {
                //impede que o cliente envie media types diferentes do aceitável
                //exemplo: text/css irá retornar 406
                options.ReturnHttpNotAcceptable = true;
                //adiciona a opção de serializar a resposta em XML
                options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                //e se eu quisesse serializar em um formato novo? Ex. CSV
            });

            services.AddHttpClient<ListaLeituraService>(c => {
                c.BaseAddress = new System.Uri(Configuration["Services:ApiBaseAddress"]);
                c.DefaultRequestHeaders.Add("User-Agent", "Alura.WebAPI.WebApp");
            });

            services.AddHttpClient<ListaLeituraAuthService>(c => {
                c.BaseAddress = new System.Uri(Configuration["Services:AuthProviderBaseAddress"]);
                c.DefaultRequestHeaders.Add("User-Agent", "Alura.WebAPI.WebApp");
            });

            services.AddHttpClient<LivrosService>(c => {
                c.BaseAddress = new System.Uri(Configuration["Services:ApiBaseAddress"]);
                c.DefaultRequestHeaders.Add("User-Agent", "Alura.WebAPI.WebApp");
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

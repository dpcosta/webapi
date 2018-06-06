using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Alura.WebAPI.WebApp.Data;
using Alura.WebAPI.Model;
using Alura.WebAPI.WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Alura.WebAPI.WebApp
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
            //injetando o contexto do entity
            services.AddDbContext<LeituraContext>( options =>
                options.UseSqlServer(Configuration.GetConnectionString("ListaLeitura"))
            );

            services
                .AddIdentity<Usuario, IdentityRole>(
                options =>
                {
                    //para facilitar a criação de um usuário com senha fraca
                    options.Password.RequiredLength = 3;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<LeituraContext>();

            services.ConfigureApplicationCookie(
                //configurando a página de autenticação qdo requisição for anônima
                options => options.LoginPath = "/Usuario/Login");

            //injetando o repositório de livros (transiente = sempre que necessário)
            services.AddTransient<IRepository<Livro>, RepositorioBaseEF<Livro>>();

            services.AddMvc(options => {
                //impede que o cliente envie media types diferentes do aceitável
                //exemplo: text/css irá retornar 406
                options.ReturnHttpNotAcceptable = true;
                //adiciona a opção de serializar a resposta em XML
                options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                //e se eu quisesse serializar em um formato novo? Ex. CSV
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

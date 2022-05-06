using BlogPessoalVS.src.data;
using BlogPessoalVS.src.repositorios;
using BlogPessoalVS.src.repositorios.implementacoes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoalVS
{
    public class Startup
    {
        public Startup(IConfiguration configuration) // configuration = config
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set;  }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configuração Banco de Dados
            IConfigurationRoot config = new ConfigurationBuilder() // reconhece o que esta no appsetting
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            services.AddDbContext<BlogPessoalVSContext>(opt => opt.UseSqlServer(config.GetConnectionString("DefaultConnection"))); // string de conexao, pega os dados das classes / criamos o contexto do bando de dados que vai criar um construtor (blog pessoal vs context) / tera conexao com o codigo default que fizemos ontem

            // Configiração Controladores
            services.AddControllers();
            services.AddScoped<IUsuario, UsuarioRepositorio>(); // criando escopo para colocar futuramente as dependencias
            services.AddScoped<ITema, TemaRepositorio>();
            services.AddScoped<IPostagem, PostagemRepositorio>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BlogPessoalVSContext contexto)
        {
            if (env.IsDevelopment())
            {
                contexto.Database.EnsureCreated(); // se nao tem um banco de dados, CRIE
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

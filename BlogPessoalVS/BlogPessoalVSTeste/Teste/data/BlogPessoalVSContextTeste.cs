using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPessoalVS.src.data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using BlogPessoalVS.src.modelos;
using BlogPessoalVS.src.utilidades;

namespace BlogPessoalVSTeste.Teste.data
{
    [TestClass]
    public class BlogPessoalVSContextTeste
    {
        private BlogPessoalVSContext _contexto;

        [TestInitialize]
        public void inicio()
        {
            var opt = new DbContextOptionsBuilder<BlogPessoalVSContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal")
                .Options;

            _contexto = new BlogPessoalVSContext(opt);
        }

        [TestMethod]
        public void InserirNovoUsuarioNoBancoRetornaUsuario()
        {
            UsuarioModelo usuario = new UsuarioModelo();

            usuario.Nome = "Leticia Zimerer";
            usuario.Email = "leticiazimerer@yahoo.com.br";
            usuario.Senha = "123456";
            usuario.Foto = "aqui estará o link da foto";
            usuario.Tipo = TipoUsuario.NORMAL;

            _contexto.Usuarios.Add(usuario); // adicionando usuario

            _contexto.SaveChanges(); // commita criação e salva no banco de dados

            Assert.IsNotNull(_contexto.Usuarios.FirstOrDefault(usuario => usuario.Email == "leticiazimerer@yahoo.com.br"));
        }
    }
}

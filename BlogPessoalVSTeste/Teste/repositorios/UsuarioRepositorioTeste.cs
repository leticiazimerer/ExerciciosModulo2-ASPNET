using BlogPessoalVS.src.repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPessoalVS.src.data;
using BlogPessoalVS.src.dtos;
using BlogPessoalVS.src.repositorios.implementacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlogPessoalVS.src.utilidades;

namespace BlogPessoalVSTeste.Teste.repositorios
{
    [TestClass]
    public class UsuarioRepositorioTeste
    {
        private IUsuario _repositorio;
        private BlogPessoalVSContext _contexto;

        [TestInitialize]
        public void ConfigurationInicial()
        {
            var opt = new DbContextOptionsBuilder<BlogPessoalVSContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal")
                .Options;
            _contexto = new BlogPessoalVSContext(opt);
            _repositorio = new UsuarioRepositorio(_contexto);
        }

        [TestMethod]
        public async Task CriarQuatroUsuariosNoBancoRetornaQuatroUsuarios()
        {
            //GIVEN - Dado que registro 4 usuarios no banco
            await _repositorio.NovoUsuarioAsync(
                 new NovoUsuarioDTO(
                     "Leticia Zimerer",
                     "leticiazimerer@gmail.com",
                     "123456",
                     "URLFOTO", 
                     TipoUsuario.NORMAL
                     ));

            await _repositorio.NovoUsuarioAsync(
                 new NovoUsuarioDTO(
                     "Cleiton Ferreira de Moraes",
                     "cleitonmoraes1709@gmail.com",
                     "123456",
                     "URLFOTO", 
                     TipoUsuario.NORMAL));

            await _repositorio.NovoUsuarioAsync(
                 new NovoUsuarioDTO(
                     "Arthur Zimerer de Moraes",
                     "arthurzimerer@gmail.com",
                     "123456",
                     "URLFOTO",
                     TipoUsuario.NORMAL));

            await _repositorio.NovoUsuarioAsync(
                 new NovoUsuarioDTO(
                     "Jade Zimerer de Moraes",
                     "jadezimerer@gmail.com",
                     "123456",
                     "URLFOTO", 
                     TipoUsuario.NORMAL));

            //WHEN - Quando pesquiso lista total
            //THEN - Então recebo 4 usuarios
            Assert.AreEqual(4, _contexto.Usuarios.Count());
        }
        [TestMethod]
        public async Task PegarUsuarioPeloEmailRetornaNaoNulo()
        {
            //GIVEN - Dado que registro um usuario no banco
            await _repositorio.NovoUsuarioAsync(
                new NovoUsuarioDTO(
                    "Estela Zimerer de Moraes",
                    "estelazimerer@gmail.com",
                    "123456",
                    "URLFOTO", 
                    TipoUsuario.NORMAL));

            //WHEN - Quando pesquiso pelo email deste usuario
            var user = await _repositorio.PegarUsuarioPeloEmailAsync("estelazimerer@gmail.com");

            //THEN - Então obtenho um usuario
            Assert.IsNotNull(user);

        }
        [TestMethod]
        public async Task PegarUsuarioPeloIdRetornaNaoNuloENomeDoUsuario()
        {
            //GIVEN - Dado que registro um usuario no banco
            await _repositorio.NovoUsuarioAsync(
            new NovoUsuarioDTO(
                "Fernanda Zimerer de Moraes",
                "fernandazimerer@gmail.com",
                "123456",
                "URLFOTO",
                TipoUsuario.NORMAL));

            //WHEN - Quando pesquiso pelo id 6
            var user = await _repositorio.PegarUsuarioPeloIdAsync(6);

            //THEN - Então, deve me retornar um elemento não nulo
            Assert.IsNotNull(user);

            //THEN - Então, o elemento deve ser Neusa Boaz
            Assert.AreEqual("Fernanda Zimerer de Moraes", user.Nome);
        }

        [TestMethod]
        public async Task AtualizarUsuarioRetornaUsuarioAtualizado()
        {
            //GIVEN - Dado que registro um usuario no banco
            await _repositorio.NovoUsuarioAsync(
            new NovoUsuarioDTO(
                "Clara Zimerer",
                "clarazimerer@gmail.com",
                "123456",
                "URLFOTO",
                TipoUsuario.NORMAL));

            //WHEN - Quando atualizamos o usuario
            var antigo =
            await _repositorio.PegarUsuarioPeloEmailAsync("clarazimerer@gmail.com");
            await _repositorio.AtualizarUsuarioAsync(new AtualizarUsuarioDTO(
                8,
            "Clara Zimerer",
            "123456",
            "URLFOTO"));

            //THEN - Então, quando validamos pesquisa deve retornar nome
            Assert.AreEqual(
            "Clara Zimerer",
            _contexto.Usuarios.FirstOrDefault(u => u.Id == antigo.Id).Nome);

            //THEN - Então, quando validamos pesquisa deve retornar senha 123456
            Assert.AreEqual(
            "123456",
            _contexto.Usuarios.FirstOrDefault(u => u.Id ==
            antigo.Id).Senha);
        }
    }
}

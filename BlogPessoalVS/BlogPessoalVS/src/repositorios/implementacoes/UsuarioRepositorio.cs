using BlogPessoalVS.src.data;
using BlogPessoalVS.src.dtos;
using BlogPessoalVS.src.modelos;
using System.Collections.Generic;
using System.Linq;

namespace BlogPessoalVS.src.repositorios.implementacoes
{
    public class UsuarioRepositorio : IUsuario // enquanto nao implementar o iusuario, vai ficar dando erro
    {
        #region Atributos //deixa o codigo separado, e precisa ser finalizado (endregion) para nao dar problema

        private readonly BlogPessoalVSContext _contexto; 

        #endregion Atributos

        #region Construtores

        public UsuarioRepositorio(BlogPessoalVSContext context)
        {
            _contexto = context;
        }

        #endregion Construtores

        #region Metotos

        public void AtualizarUsuario(AtualizarUsuarioDTO usuario)
        {
            var usuarioExistente = PegarUsuarioPeloId(usuario.Id);
            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.Senha = usuario.Senha;
            usuarioExistente.Foto = usuario.Foto;
            _contexto.Usuarios.Update(usuarioExistente);
            _contexto.SaveChanges();
        }

        public void DeletarUsuario(int id)
        {
            _contexto.Usuarios.Remove(PegarUsuarioPeloId(id));
            _contexto.SaveChanges();
        }

        public void NovoUsuario(NovoUsuarioDTO usuario)
        {
            _contexto.Usuarios.Add(new UsuarioModelo
            {
                Email = usuario.Email,
                Nome = usuario.Nome,
                Senha = usuario.Senha,
                Foto = usuario.Foto,
                Tipo = usuario.Tipo
            });
            _contexto.SaveChanges();
        }

        public UsuarioModelo PegarUsuarioPeloEmail(string email)
        {
            return _contexto.Usuarios.FirstOrDefault(u => u.Email == email);
        }

        public UsuarioModelo PegarUsuarioPeloId(int id)
        {
            return _contexto.Usuarios.FirstOrDefault(u => u.Id == id);
        }

        public List<UsuarioModelo> PegarUsuarioPeloNome(string nome)
        {
            throw new System.NotImplementedException();
        }

        public List<UsuarioModelo> PegarUsuariosPeloNome(string nome)
        {
            return _contexto.Usuarios
                        .Where(u => u.Nome.Contains(nome))
                        .ToList();
        }
        #endregion Metodos
    }
}

using BlogPessoalVS.src.dtos;
using BlogPessoalVS.src.modelos;
using BlogPessoalVS.src.repositorios;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogPessoalVS.src.servicos.implementacoes
{
    public class AutenticacaoServicos : IAutenticacao
    {
        #region Atributos
        private readonly IUsuario _repositorio;
        public IConfiguration Configuracao { get; }
        #endregion

        #region Construtores
        public AutenticacaoServicos(IUsuario repositorio, IConfiguration configuration)
        {
            _repositorio = repositorio;
            Configuracao = configuration;
        }
        #endregion

        #region Metodos
        public string CodificarSenha(string senha)
        {
            var bytes = Encoding.UTF8.GetBytes(senha);
            return Convert.ToBase64String(bytes);
        }

        public void CriarUsuarioSemDuplicar(NovoUsuarioDTO dto)
        {
            var usuario = _repositorio.PegarUsuarioPeloEmail(dto.Email);
            if (usuario != null) throw new Exception("Este email já está sendo utilizado"); // se o usuario for diferente de nulo, ou seja, existir, nao sera criado
            dto.Senha = CodificarSenha(dto.Senha);
            _repositorio.NovoUsuario(dto);
        }

        public string GerarToken(UsuarioModelo usuario)
        {
            var tokenManipulador = new JwtSecurityTokenHandler(); // manipulador cria o token
            var chave = Encoding.ASCII.GetBytes(Configuracao["Settings:Secret"]);
            var tokenDescricao = new SecurityTokenDescriptor // descriptor, manda a descricao para o manipulador criar o token
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Email, usuario.Email.ToString()),
                        new Claim(ClaimTypes.Role, usuario.Tipo.ToString())
                    }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(chave),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenManipulador.CreateToken(tokenDescricao);
            return tokenManipulador.WriteToken(token);
        }

        public AutorizacaoDTO PegarAutorizacao(AutenticarDTO dto)
        {
            var usuario = _repositorio.PegarUsuarioPeloEmail(dto.Email);
            if (usuario == null) throw new Exception("Usuário não encontrado");
            if (usuario.Senha != CodificarSenha(dto.Senha)) throw new Exception("Senha incorreta");
            return new AutorizacaoDTO(usuario.Id, usuario.Email, usuario.Tipo, GerarToken(usuario));
        }
        #endregion
    }
}
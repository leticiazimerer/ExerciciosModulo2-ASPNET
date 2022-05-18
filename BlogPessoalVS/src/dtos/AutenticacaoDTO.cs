using BlogPessoalVS.src.utilidades;
using System.ComponentModel.DataAnnotations;

namespace BlogPessoalVS.src.dtos
{
    /// <summary>
    /// <para>Resumo> Criando AutenticaçãoDTO</para>
    /// <para>Criado por: Leticia Zimerer</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 10/05/2022</para>
    /// </summary>
    public class AutenticarDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
        public AutenticarDTO(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }
    }
    public class AutorizacaoDTO // devolve o token
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public TipoUsuario Tipo { get; set; }
        public string Token { get; set; }
        public AutorizacaoDTO(int id, string email, TipoUsuario tipo, string token)
        {
            Id = id;
            Email = email;
            Tipo = tipo;
            Token = token;
        }
    }
}

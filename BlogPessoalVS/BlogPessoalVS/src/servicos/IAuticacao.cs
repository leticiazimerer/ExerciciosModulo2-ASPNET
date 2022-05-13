using BlogPessoalVS.src.dtos;
using BlogPessoalVS.src.modelos;
using System.Threading.Tasks;

namespace BlogPessoalVS.src.servicos
{
    public interface IAutenticacao
    {
        string CodificarSenha(string senha);
        Task CriarUsuarioSemDuplicarAsync(NovoUsuarioDTO dto);
        string GerarToken(UsuarioModelo usuario);
        Task <AutorizacaoDTO> PegarAutorizacaoAsync(AutenticarDTO dto);
    }
}

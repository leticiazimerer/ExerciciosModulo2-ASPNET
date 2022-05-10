using BlogPessoalVS.src.dtos;
using BlogPessoalVS.src.modelos;

namespace BlogPessoalVS.src.servicos
{
    public interface IAutenticacao
    {
        string CodificarSenha(string senha);
        void CriarUsuarioSemDuplicar(NovoUsuarioDTO dto);
        string GerarToken(UsuarioModelo usuario);
        AutorizacaoDTO PegarAutorizacao(AutenticarDTO dto);
    }
}

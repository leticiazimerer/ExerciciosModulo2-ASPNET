using BlogPessoalVS.src.dtos;
using BlogPessoalVS.src.modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPessoalVS.src.repositorios
{
    /// <summary>
    /// <para>Resumo> Responsavel por representar ações de CRUD de usuario</para> // é "para" até para o inglês
    /// <para>Criado por: Leticia Zimerer</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public interface IUsuario
    {
        Task NovoUsuarioAsync(NovoUsuarioDTO usuario); // DTO: DATA TRANSFER OBJECT / precisamos criar uma classe "UsuarioDTO" onde colocaremos as classes (AtualizarUsuarioDTO e NovoUsuarioDTO) por isso criamos a pasta dtos dentro da pasta scr
        Task AtualizarUsuarioAsync(AtualizarUsuarioDTO usuario); // precisamos criar uma classe "AtualizarUsuarioDTO"
        Task DeletarUsuarioAsync(int id);
        Task <UsuarioModelo> PegarUsuarioPeloIdAsync(int id);
        Task<List<UsuarioModelo>> PegarUsuarioPeloNomeAsync(string nome);
        Task<UsuarioModelo> PegarUsuarioPeloEmailAsync(string email);

    }
} 

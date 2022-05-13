using BlogPessoalVS.src.dtos;
using BlogPessoalVS.src.modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPessoalVS.src.repositorios
{
    /// <summary>
    /// <para>Resumo> Responsavel por representar ações de CRUD de tema</para> // é "para" até para o inglês
    /// <para>Criado por: Leticia Zimerer</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public interface ITema
    {
        Task NovoTemaAsync(NovoTemaDTO tema); // DTO: DATA TRANSFER OBJECT / precisamos criar uma classe "UsuarioDTO" onde colocaremos as classes (AtualizarUsuarioDTO e NovoUsuarioDTO) por isso criamos a pasta dtos dentro da pasta scr
        Task AtualizarTemaAsync(AtualizarTemaDTO tema); // precisamos criar uma classe "AtualizarUsuarioDTO"
        Task DeletarTemaAsync(int id);
        Task<TemaModelo> PegarTemaPeloIdAsync(int id);
        Task<List<TemaModelo>> PegarTemaPelaDescricaoAsync(string descricao); // quando pesquisar algo, teremos muitos temas
        Task<List<TemaModelo>> PegarTodosTemasAsync();
    }
}

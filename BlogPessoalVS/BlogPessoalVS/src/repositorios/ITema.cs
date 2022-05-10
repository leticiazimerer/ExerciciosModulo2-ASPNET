using BlogPessoalVS.src.dtos;
using BlogPessoalVS.src.modelos;
using System.Collections.Generic;

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
        void NovoTema(NovoTemaDTO tema); // DTO: DATA TRANSFER OBJECT / precisamos criar uma classe "UsuarioDTO" onde colocaremos as classes (AtualizarUsuarioDTO e NovoUsuarioDTO) por isso criamos a pasta dtos dentro da pasta scr
        void AtualizarTema(AtualizarTemaDTO tema); // precisamos criar uma classe "AtualizarUsuarioDTO"
        void DeletarTema(int id);
        TemaModelo PegarTemaPeloId(int id);
        List<TemaModelo> PegarTemaPelaDescricao(string descricao); // quando pesquisar algo, teremos muitos temas
        List<TemaModelo> PegarTodosTemas();
    }
}

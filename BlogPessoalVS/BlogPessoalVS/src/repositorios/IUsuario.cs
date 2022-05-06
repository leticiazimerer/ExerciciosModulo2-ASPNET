using BlogPessoalVS.src.dtos;
using BlogPessoalVS.src.modelos;
using System.Collections.Generic;

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
        void NovoUsuario(NovoUsuarioDTO usuario); // DTO: DATA TRANSFER OBJECT / precisamos criar uma classe "UsuarioDTO" onde colocaremos as classes (AtualizarUsuarioDTO e NovoUsuarioDTO) por isso criamos a pasta dtos dentro da pasta scr
        void AtualizarUsuario(AtualizarUsuarioDTO usuario); // precisamos criar uma classe "AtualizarUsuarioDTO"
        void DeletarUsuario(int id);
        UsuarioModelo PegarUsuarioPeloId(int id);
        List<UsuarioModelo> PegarUsuarioPeloNome(string nome);
        UsuarioModelo PegarUsuarioPeloEmail(string email);

    }
} 

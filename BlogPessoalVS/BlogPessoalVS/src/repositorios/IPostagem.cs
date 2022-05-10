using BlogPessoalVS.src.dtos;
using BlogPessoalVS.src.modelos;
using System.Collections.Generic;

namespace BlogPessoalVS.src.repositorios
{
    /// <summary>
    /// <para>Resumo> Responsavel por representar ações de CRUD de postagem</para> // é "para" até para o inglês
    /// <para>Criado por: Leticia Zimerer</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public interface IPostagem
    {
        void NovaPostagem(NovaPostagemDTO postagem);
        void AtualizarPostagem(AtualizarPostagemDTO postagem);
        void DeletarPostagem(int id); // deleta a postagem pelo id
        PostagemModelo PegarPostagemPeloId(int id); // retorna uma postagem
        List<PostagemModelo> PegarTodasPostagens(); // nao precisa de parametro "();" pq eh so ir lá e pegar as postagens
        List<PostagemModelo> PegarPostagemPorPesquisa(string titulo, string descricaoTema, string nomeCriador); // precisa de parametro pq pegara do titulo
    }
}

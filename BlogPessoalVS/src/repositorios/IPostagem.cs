using BlogPessoalVS.src.dtos;
using BlogPessoalVS.src.modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task NovaPostagemAsync(NovaPostagemDTO postagem);
        Task AtualizarPostagemAsync(AtualizarPostagemDTO postagem);
        Task DeletarPostagemAsync(int id); // deleta a postagem pelo id
        Task<PostagemModelo> PegarPostagemPeloIdAsync(int id); // retorna uma postagem
        Task<List<PostagemModelo>> PegarTodasPostagensAsync(); // nao precisa de parametro "();" pq eh so ir lá e pegar as postagens
        Task<List<PostagemModelo>> PegarPostagemPorPesquisaAsync(string titulo, string descricaoTema, string nomeCriador); // precisa de parametro pq pegara do titulo
    }
}

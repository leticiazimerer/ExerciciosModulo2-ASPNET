
using BlogPessoalVS.src.data;
using BlogPessoalVS.src.dtos;
using BlogPessoalVS.src.modelos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoalVS.src.repositorios.implementacoes
{
    public class PostagemRepositorio : IPostagem
    {
        #region Atributos

        private readonly BlogPessoalVSContext _contexto;

        #endregion Atributos

        #region Construtores

        public PostagemRepositorio(BlogPessoalVSContext context)
        {
            _contexto = context;
        }

        #endregion Construtores

        #region Metodos

        /// <summary>
        /// Pegar todas as postagens
        /// </summary>
        /// <returns>List</returns>
        public async Task<List<PostagemModelo>> PegarTodasPostagensAsync()
        {
            return await _contexto.Postagens
                .Include(p => p.Criador)
                .Include(p => p.Tema)
                .ToListAsync();
        }

        /// <summary>
        /// <para>Resumo> Método assíncrono para pegar uma postagem pelo Id</para>
        /// </summary>
        /// <param nome="id">Id da postagem</param>
        /// <return>PostagemModelo</return>
        public async Task<PostagemModelo> PegarPostagemPeloIdAsync(int id)
        {
            return await _contexto.Postagens.FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <summary>
        /// <para>Resumo> Método assíncrono para pegar uma postagem por pesquisa</para>
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="descricaoTema"></param>
        /// <param name="nomeCriador"></param>
        /// <returns>List</returns>
        public async Task<List<PostagemModelo>> PegarPostagemPorPesquisaAsync(
            string titulo,
            string descricaoTema,
            string nomeCriador)
        {
            switch (titulo, descricaoTema, nomeCriador)
            {
                case (null, null, null):
                    return await PegarTodasPostagensAsync();

                case (null, null, _):
                    return await _contexto.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p => p.Criador.Nome.Contains(nomeCriador))
                        .ToListAsync();

                case (null, _, null):
                    return await _contexto.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p => p.Tema.Descricao.Contains(descricaoTema))
                        .ToListAsync();

                case (_, null, null):
                    return await _contexto.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p => p.Titulo.Contains(titulo))
                        .ToListAsync();

                case (_, _, null):
                    return await _contexto.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p =>
                        p.Titulo.Contains(titulo) &
                        p.Tema.Descricao.Contains(descricaoTema))
                        .ToListAsync();

                case (null, _, _):
                    return await _contexto.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p =>
                        p.Tema.Descricao.Contains(descricaoTema) &
                        p.Criador.Nome.Contains(nomeCriador))
                        .ToListAsync();

                case (_, null, _):
                    return await _contexto.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p =>
                        p.Titulo.Contains(titulo) &
                        p.Criador.Nome.Contains(nomeCriador))
                        .ToListAsync();

                case (_, _, _):
                    return await _contexto.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p =>
                        p.Titulo.Contains(titulo) |
                        p.Tema.Descricao.Contains(descricaoTema) |
                        p.Criador.Nome.Contains(nomeCriador))
                        .ToListAsync();
            }
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar uma nova postagem</para>
        /// </summary>
        /// <param name="postagem">NovaPostagemDTO</param>
        public async Task NovaPostagemAsync(NovaPostagemDTO postagem)
        {
            await _contexto.Postagens.AddAsync(new PostagemModelo
            {
                Titulo = postagem.Titulo,
                Descricao = postagem.Descricao,
                Foto = postagem.Foto,
                Criador = _contexto.Usuarios.FirstOrDefault(u => u.Email == postagem.EmailCriador),
                Tema = _contexto.Temas.FirstOrDefault(t => t.Descricao == postagem.DescricaoTema)
            });
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para atualizar a postagem</para>
        /// </summary>
        /// <param name="postagem">AtualizarPostagemDTO</param>
        public async Task AtualizarPostagemAsync(AtualizarPostagemDTO postagem)
        {
            var postagemExistente = await PegarPostagemPeloIdAsync(postagem.Id);
            postagemExistente.Titulo = postagem.Titulo;
            postagemExistente.Descricao = postagem.Descricao;
            postagemExistente.Foto = postagem.Foto;
            postagemExistente.Tema = _contexto.Temas.FirstOrDefault(t => t.Descricao == postagem.DescricaoTema);

            _contexto.Postagens.Update(postagemExistente);
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para deletar uma postagem</para>
        /// </summary>
        /// <param name="id">Id da postagem</param>
        public async Task DeletarPostagemAsync(int id)
        {
            _contexto.Postagens.Remove(await PegarPostagemPeloIdAsync(id));
            await _contexto.SaveChangesAsync();
        }
        #endregion Métodos
    }
}

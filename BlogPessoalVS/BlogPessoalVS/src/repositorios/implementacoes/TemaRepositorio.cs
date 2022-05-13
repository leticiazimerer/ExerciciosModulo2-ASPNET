using BlogPessoalVS.src.data;
using BlogPessoalVS.src.dtos;
using BlogPessoalVS.src.modelos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoalVS.src.repositorios.implementacoes
{
    /// <summary>
    /// <para>Resumo> Classe responsavel por implementar ITema</para>
    /// <para>Criado por: Leticia Zimerer</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
    public class TemaRepositorio : ITema
    {

        #region Atributos

        private readonly BlogPessoalVSContext _contexto;

        #endregion Atributos

        #region Construtores

        public TemaRepositorio(BlogPessoalVSContext contexto)
        {
            _contexto = contexto;
        }

        #endregion Construtores

        #region Metodos

        /// <summary>
        /// Pegar todos os temas
        /// </summary>
        /// <returns>List</returns>
        public async Task<List<TemaModelo>> PegarTodosTemasAsync()
        {
            return await _contexto.Temas.ToListAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para atualizar o tema</para>
        /// </summary>
        /// <param name="tema">AtualizarTemaDTO</param>
        public async Task AtualizarTemaAsync(AtualizarTemaDTO tema)
        {
            var temaExistente = await PegarTemaPeloIdAsync(tema.Id);
            temaExistente.Descricao = tema.Descricao;
            _contexto.Temas.Update(temaExistente);
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para deletar um tema</para>
        /// </summary>
        /// <param name="id">Id do tema</param>
        public async Task DeletarTemaAsync(int id)
        {
            _contexto.Temas.Remove(await PegarTemaPeloIdAsync(id));
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar um novo tema</para>
        /// </summary>
        /// <param name="tema">NovoTemaDTO</param>
        public async Task NovoTemaAsync(NovoTemaDTO tema)
        {
            await _contexto.Temas.AddAsync(new TemaModelo
            {
                Descricao = tema.Descricao
            });
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo> Método assíncrono para pegar um tema pelo Id</para>
        /// </summary>
        /// <param nome="descricao">Id do tema</param>
        /// <return>TemaModelo</return>
        public async Task<List<TemaModelo>> PegarTemaPelaDescricaoAsync(string descricao)
        {
            return await _contexto.Temas
                .Where(u => u.Descricao.Contains(descricao))
                .ToListAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar um tema pelo Id</para>
        /// </summary>
        /// <param name="id">Id do tema</param>
        /// <return>TemaModelo</return>
        public async Task<TemaModelo> PegarTemaPeloIdAsync(int id)
        {
            return await _contexto.Temas.FirstOrDefaultAsync(t => t.Id == id);
        }
        #endregion Metodos
    }
}

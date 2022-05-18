﻿using BlogPessoalVS.src.data;
using BlogPessoalVS.src.dtos;
using BlogPessoalVS.src.modelos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoalVS.src.repositorios.implementacoes
{
    /// <summary>
    /// <para>Resumo> Classe responsavel por implementar IUsuario</para>
    /// <para>Criado por: Leticia Zimerer</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
    public class UsuarioRepositorio : IUsuario // enquanto nao implementar o iusuario, vai ficar dando erro
    {
        #region Atributos //deixa o codigo separado, e precisa ser finalizado (endregion) para nao dar problema

        private readonly BlogPessoalVSContext _contexto; 

        #endregion Atributos

        #region Construtores

        public UsuarioRepositorio(BlogPessoalVSContext context)
        {
            _contexto = context;
        }

        #endregion Construtores

        #region Metotos

        /// <summary>
        /// <para>Resumo: Método assíncrono para atualizar um usuario</para>
        /// </summary>
        /// <param name="usuario">AtualizarUsuarioDTO</param>
        public async Task AtualizarUsuarioAsync(AtualizarUsuarioDTO usuario)
        {
            var usuarioExistente = await PegarUsuarioPeloIdAsync(usuario.Id);
            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.Senha = usuario.Senha;
            usuarioExistente.Foto = usuario.Foto;
            _contexto.Usuarios.Update(usuarioExistente);
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para deletar um usuario</para>
        /// </summary>
        /// <param name="id">Id do usuario</param>
        public async Task DeletarUsuarioAsync(int id)
        {
            _contexto.Usuarios.Remove(await PegarUsuarioPeloIdAsync(id));
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar um novo usuario</para>
        /// </summary>
        /// <param name="usuario">NovoUsuarioDTO</param>
        public async Task NovoUsuarioAsync(NovoUsuarioDTO usuario)
        {
            await _contexto.Usuarios.AddAsync(new UsuarioModelo
            {
                Email = usuario.Email,
                Nome = usuario.Nome,
                Senha = usuario.Senha,
                Foto = usuario.Foto,
                Tipo = usuario.Tipo
            });
            await _contexto.SaveChangesAsync();
        }
        /// <summary>
        /// <para>Resumo> Método assíncrono para pegar um usuario pelo Email</para>
        /// </summary>
        /// <param email="id">Id do usuario</param>
        public async Task<UsuarioModelo> PegarUsuarioPeloEmailAsync(string email)
        {
            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }
        /// <summary>
        /// <para>Resumo> Método assíncrono para pegar um usuario pelo Id</para>
        /// </summary>
        /// <param nome="id">Id do usuario</param>
        /// <return>UsuarioModelo</return>
        public async Task<UsuarioModelo> PegarUsuarioPeloIdAsync(int id)
        {
            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }
        /// <summary>
        /// <para>Resumo> Método assíncrono para pegar um usuario pelo Nome</para>
        /// </summary>
        /// <param nome="nome">Id do usuario</param>
        /// <return>UsuarioModelo</return>
        public async Task<List<UsuarioModelo>> PegarUsuariosPeloNomeAsync(string nome)
        {
            return await _contexto.Usuarios
                        .Where(u => u.Nome.Contains(nome))
                        .ToListAsync();
        }
        public Task<List<UsuarioModelo>> PegarUsuarioPeloNomeAsync(string nome)
        {
            throw new System.NotImplementedException();
        }
        #endregion Metodos
    }
}
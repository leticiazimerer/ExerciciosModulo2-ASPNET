﻿using BlogPessoalVS.src.dtos;
using BlogPessoalVS.src.servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogPessoalVS.src.controladores
{ 
    [ApiController]
    [Route("api/Autenticacao")]
    [Produces("application/json")]
    public class AutenticacaoControlador : ControllerBase
    {
        #region Atributos
        private readonly IAutenticacao _servicos;
        #endregion
        #region Construtores
        public AutenticacaoControlador(IAutenticacao servicos)
        {
            _servicos = servicos;
        }
        #endregion
        #region Métodos
        [HttpPost]
        [AllowAnonymous] // qlq pessoa (anomina) pode autenticar
        public async Task<ActionResult> AutenticarAsync([FromBody] AutenticarDTO autenticacao)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var autorizacao = await _servicos.PegarAutorizacaoAsync(autenticacao);
                return Ok(autorizacao);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        #endregion
    }
}
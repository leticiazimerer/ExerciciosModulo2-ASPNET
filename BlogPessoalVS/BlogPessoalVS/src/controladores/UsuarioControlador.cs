using BlogPessoalVS.src.dtos;
using BlogPessoalVS.src.repositorios;
using BlogPessoalVS.src.servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogPessoalVS.src.controladores
{
    [ApiController] //vai se comportar como controlador, esperando requisicoes e dando respostas
    [Route("api/Usuarios")] // api/Usuarios (tem que ser no plural): endpoint principal que não será alterado / vai determinar quais seram os resultados
    [Produces("application/json")] // quais respostas seram dadas no arq json
    public class UsuarioControlador : ControllerBase // herda do controlador base
    {
        #region Atributos
        private readonly IUsuario _repositorio; // toda vez que usamos o iusuario, ele tbem usa o usuariorepositorio
        private readonly IAutenticacao _servicos;
        #endregion

        #region Construtores
        public UsuarioControlador(IUsuario repositorio, IAutenticacao servico)
        {
            _repositorio = repositorio; // acessa os metodos e as consultas de cada endpoind que criarmos]
            _servicos = servico;
        }
        #endregion

        #region Métodos
        [HttpGet("id/{idUsuario}")] // pega (get) algo do iusuario (parametro id)
        [Authorize(Roles ="NORMAL,ADMINISTRADOR")]
        public async Task<ActionResult> PegarUsuarioPeloIdAsync([FromRoute] int idUsuario) // fromroute, coloca o parametro de pesquisa na rota
        {
            var usuario = await _repositorio.PegarUsuarioPeloIdAsync(idUsuario);
            if (usuario == null) return NotFound(); // se o usuario for nulo ira retornar erro 404, not found
            return Ok(usuario); // se nao for nulo, retornara ok, erro 200
        }

        [HttpGet]
        [Authorize(Roles = "NORMAL,ADMINISTRADOR")]
        public async Task<ActionResult> PegarUsuarioPeloNomeAsync([FromQuery] string nomeUsuario) // from query, extrai o elemento do parametro
        {
            var usuarios = await _repositorio.PegarUsuarioPeloNomeAsync(nomeUsuario);
            if (usuarios.Count < 1) return NoContent(); // < 1 = 0 - NoContent = erro 204
            return Ok(usuarios); // retorna uma lista, por isso esta no plural
        }

        [HttpGet("email/{emailUsuario}")]
        [Authorize(Roles = "NORMAL,ADMINISTRADOR")]
        public async Task<ActionResult> PegarUsuarioPeloEmailAsync([FromRoute] string emailUsuario) 
        {
            var usuario = await _repositorio.PegarUsuarioPeloEmailAsync(emailUsuario);
            if (usuario == null) return NotFound(); 
            return Ok(usuario); 
        }

        [HttpPost] // cadastra o novo usuario
        [AllowAnonymous]
        public async Task<ActionResult> NovoUsuarioAsync([FromBody] NovoUsuarioDTO usuario) // frombody = usado para cadastrar novas coisas no formulario
        {
            if (!ModelState.IsValid) return BadRequest(); // querendo saber se nao é valido o que estamos pegando do novousuarioDTO? / BadRequest = erro 400 / ! = negativa do que esta sendo falado
           
            try
            {
                await _servicos.CriarUsuarioSemDuplicarAsync(usuario); // nao deixa criar usuario duplicado
                return Created($"api/Usuarios/email/{usuario.Email}", usuario);
            }
            catch(Exception ex) // ex = exception
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "NORMAL,ADMINISTRADOR")] // autoriza os dois usuarios acessarem esses endpoints
        public async Task<ActionResult> AtualizarUsuarioAsync([FromBody] AtualizarUsuarioDTO usuario)
        {
            if (!ModelState.IsValid) return BadRequest();
            usuario.Senha = _servicos.CodificarSenha(usuario.Senha);
            await _repositorio.AtualizarUsuarioAsync(usuario);
            return Ok(usuario);
        }

        [HttpDelete("deletar/{idUsuario}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> DeletarUsuarioAsync([FromRoute] int idUsuario)
        {
            await _repositorio.DeletarUsuarioAsync(idUsuario);
            return NoContent();
        }
        #endregion
    }
}

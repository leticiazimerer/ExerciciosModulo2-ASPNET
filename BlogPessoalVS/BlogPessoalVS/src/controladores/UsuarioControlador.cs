using BlogPessoalVS.src.dtos;
using BlogPessoalVS.src.modelos;
using BlogPessoalVS.src.repositorios;
using BlogPessoalVS.src.servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Pegar Usuário pelo Id
        /// </summary>
        /// <param name="idUsuario">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o usuario</response>
        /// <response code="404">Usuario não existente</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsuarioModelo))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{idUsuario}")] // pega (get) algo do iusuario (parametro id)
        [Authorize(Roles ="NORMAL,ADMINISTRADOR")]
        public async Task<ActionResult> PegarUsuarioPeloIdAsync([FromRoute] int idUsuario) // fromroute, coloca o parametro de pesquisa na rota
        {
            var usuario = await _repositorio.PegarUsuarioPeloIdAsync(idUsuario);
            if (usuario == null) return NotFound(); // se o usuario for nulo ira retornar erro 404, not found
            return Ok(usuario); // se nao for nulo, retornara ok, erro 200
        }

        /// <summary>
        /// Pegar usuario pelo Nome
        /// </summary>
        /// <param name="nomeUsuario">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o usuario</response>
        /// <response code="204">Nome não existe</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsuarioModelo))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [Authorize(Roles = "NORMAL,ADMINISTRADOR")]
        public async Task<ActionResult> PegarUsuarioPeloNomeAsync([FromQuery] string nomeUsuario) // from query, extrai o elemento do parametro
        {
            var usuarios = await _repositorio.PegarUsuarioPeloNomeAsync(nomeUsuario);
            if (usuarios.Count < 1) return NoContent(); // < 1 = 0 - NoContent = erro 204
            return Ok(usuarios); // retorna uma lista, por isso esta no plural
        }

        /// <summary>
        /// Pegar usuario pelo Email
        /// </summary>
        /// <param name="emailUsuario">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o usuario</response>
        /// <response code="404">Email não existente</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsuarioModelo))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("email/{emailUsuario}")]
        [Authorize(Roles = "NORMAL,ADMINISTRADOR")]
        public async Task<ActionResult> PegarUsuarioPeloEmailAsync([FromRoute] string emailUsuario) 
        {
            var usuario = await _repositorio.PegarUsuarioPeloEmailAsync(emailUsuario);
            if (usuario == null) return NotFound(); 
            return Ok(usuario); 
        }

        /// <summary>
        /// Criar novo Usuario
        /// </summary>
        /// <param name="usuario">NovoUsuarioDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Usuarios
        ///     {
        ///        "nome": "Leticia Zimerer",
        ///        "email": "leticia@gmail.com",
        ///        "senha": "1234",
        ///        "foto": "URLFOTO",
        ///        "tipo": "NORMAL"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna usuario criado</response>
        /// <response code="400">Erro na requisição</response>
        /// <response code="401">E-mail ja cadastrado</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UsuarioModelo))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        /// <summary>
        /// Atualizar Usuario
        /// </summary>
        /// <param name="usuario">AtualizarUsuarioDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /api/Usuarios
        ///     {
        ///        "id": 1,    
        ///        "nome": "Leticia Zimerer",
        ///        "senha": "1234",
        ///        "foto": "URLFOTO"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Retorna usuario atualizado</response>
        /// <response code="400">Erro na requisição</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Authorize(Roles = "NORMAL,ADMINISTRADOR")] // autoriza os dois usuarios acessarem esses endpoints
        public async Task<ActionResult> AtualizarUsuarioAsync([FromBody] AtualizarUsuarioDTO usuario)
        {
            if (!ModelState.IsValid) return BadRequest();
            usuario.Senha = _servicos.CodificarSenha(usuario.Senha);
            await _repositorio.AtualizarUsuarioAsync(usuario);
            return Ok(usuario);
        }

        /// <summary>
        /// Deletar usuario pelo Id
        /// </summary>
        /// <param name="idUsuario">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Usuario deletado</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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

using BlogPessoalVS.src.dtos;
using BlogPessoalVS.src.modelos;
using BlogPessoalVS.src.repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogPessoalVS.src.controladores
{
    [ApiController]
    [Route("api/Postagens")]
    [Produces("application/json")]
    public class PostagemControlador : ControllerBase
    {
        #region Atributos
        private readonly IPostagem _repositorio;
        #endregion

        #region Construtores
        public PostagemControlador(IPostagem repositorio)
        {
            _repositorio = repositorio;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Pegar Postagem pelo Id
        /// </summary>
        /// <param name="idPostagem">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna a postagem</response>
        /// <response code="404">Postagem não existente</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostagemModelo))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{idPostagem}")]
        [Authorize]
        public async Task<ActionResult> PegarPostagemPeloIdAsync([FromRoute] int idPostagem)
        {
            var postagem = await _repositorio.PegarPostagemPeloIdAsync(idPostagem);
            if (postagem == null) return NotFound();
            return Ok(postagem);
        }

        /// <summary>
        /// Pegar todas as postagens
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Lista de Postagem</response>
        /// <response code="204">Lista vazia</response>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> PegarTodasPostagensAsync()
        {
            var lista = await _repositorio.PegarTodasPostagensAsync();
            if (lista.Count < 1) return NoContent();
            return Ok(lista);
        }

        /// <summary>
        /// Criar nova Posyagem
        /// </summary>
        /// <param name="postagem">NovaPostagemDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Postagens
        ///     {
        ///        "titulo": "BBB21",
        ///        "descricaoTema": "Arthur Aguiar trai a esposa pela 18ª antes de entrar no BBB21",      
        ///        "nomeCriador": "Leticia Zimerer",      
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna postagem criada</response>
        /// <response code="400">Erro na requisição</response>
        /// <response code="401">DescricaoTema ja cadastrado</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PostagemModelo))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> NovaPostagemAsync([FromBody] NovaPostagemDTO postagem)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _repositorio.NovaPostagemAsync(postagem);
            return Created($"api/Postagens", postagem);
        }

        /// <summary>
        /// Atualizar Postagem
        /// </summary>
        /// <param name="postagem">AtualizarPostagemDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /api/Postagens
        ///     {
        ///        "titulo": "BBB22",
        ///        "descricao": "ERRATA: Arthur Aguiar trai a esposa pela 18ª antes de entrar no BBB22",
        ///        "foto": "URLFOTO",
        ///        "tema": "Realits Shows"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Retorna postagem atualizada</response>
        /// <response code="400">Erro na requisição</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> AtualizarPostagem([FromBody] AtualizarPostagemDTO postagem)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _repositorio.AtualizarPostagemAsync(postagem);
            return Ok(postagem);
        }

        /// <summary>
        /// Deletar Postagem pelo Id
        /// </summary>
        /// <param name="idPostagem">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Postagem deletada</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("deletar/{idUsuario}")]
        [HttpDelete("deletar/{idPostagem}")]
        [Authorize]
        public async Task<ActionResult> DeletarPostagemAsync([FromRoute] int idPostagem)
        {
            await _repositorio.DeletarPostagemAsync(idPostagem);
            return NoContent();
        }
        #endregion
    }
}


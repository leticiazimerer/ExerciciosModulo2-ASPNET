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
    [Route("api/Temas")]
    [Produces("application/json")]
    public class TemaControlador : ControllerBase
    {
        #region Atributos
        private readonly ITema _repositorio;
        #endregion

        #region Construtores
        public TemaControlador(ITema repositorio)
        {
            _repositorio = repositorio;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Pegar Tema pelo Id
        /// </summary>
        /// <param name="idTema">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o tema</response>
        /// <response code="404">Tema não existente</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TemaModelo))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{idTema}")]
        [Authorize]
        public async Task<ActionResult> PegarTemaPeloIdAsync([FromRoute] int idTema)
        {
            var tema = await _repositorio.PegarTemaPeloIdAsync(idTema);
            if (tema == null) return NotFound();
            return Ok(tema);
        }

        /// <summary>
        /// Pegar Tema pela Descrição
        /// </summary>
        /// <param name="descricaoTema">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna a Tema</response>
        /// <response code="204">Descrição não existe</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TemaModelo))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("pesquisa")]
        [Authorize]
        public async Task<ActionResult> PegarTemaPelaDescricaoAsync([FromQuery] string descricaoTema)
        {
            var temas = await _repositorio.PegarTemaPelaDescricaoAsync(descricaoTema);
            if (temas.Count < 1) return NoContent();
            return Ok(temas);
        }

        /// <summary>
        /// Criar novo Tema
        /// </summary>
        /// <param name="tema">NovoTemaDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Temas
        ///     {
        ///        "descricao": "Arthur Aguiar trai a esposa pela 18ª antes de entrar no BBB21",
        ///        "postagensRelacionadas": "BBB21 - Tudo sobre Arthur",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna tema criado</response>
        /// <response code="400">Erro na requisição</response>
        /// <response code="401">Tema ja cadastrado</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TemaModelo))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> NovoTemaAsync([FromBody] NovoTemaDTO tema)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _repositorio.NovoTemaAsync(tema);
            return Created($"api/Temas", tema);
        }

        /// <summary>
        /// Atualizar Tema
        /// </summary>
        /// <param name="tema">AtualizarTemaDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /api/Temas
        ///     {
        ///        "descricao": "Arthur Aguiar trai a esposa pela 18ª antes de entrar no BBB21",
        ///        "postagensRelacionadas": "BBB22 - Tudo sobre Juliette",
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Retorna usuario atualizado</response>
        /// <response code="400">Erro na requisição</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Authorize(Roles ="ADMINISTRADOR")]
        public async Task<ActionResult> AtualizarTemaAsync([FromBody] AtualizarTemaDTO tema)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _repositorio.AtualizarTemaAsync(tema);
            return Ok(tema);
        }

        /// <summary>
        /// Deletar Tema pelo Id
        /// </summary>
        /// <param name="idTema">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Tema deletado</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("deletar/{idTema}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> DeletarTemaAsync([FromRoute] int idTema)
        {
            await _repositorio.DeletarTemaAsync(idTema);
            return NoContent();
        }

        /// <summary>
        /// Pegar todos os temas
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Lista de Temas</response>
        /// <response code="204">Lista vazia</response>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> PegarTodosTemasAsync()
        {
            var lista = await _repositorio.PegarTodosTemasAsync();
            if (lista.Count < 1) return NoContent();
            return Ok(lista);
        }
        #endregion
    }
}


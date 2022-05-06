using BlogPessoalVS.src.dtos;
using BlogPessoalVS.src.repositorios;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("id/{idPostagem}")]
        public IActionResult PegarPostagemPeloId([FromRoute] int idPostagem)
        {
            var postagem = _repositorio.PegarPostagemPeloId(idPostagem);

            if (postagem == null) return NotFound();
            return Ok(postagem);
        }

        [HttpGet]
        public IActionResult PegarTemaPelaDescricao([FromQuery] string descricaopostagem)
        {
            var postagem = _repositorio.PegarPostagemPelaDescricao(descricaopostagem);
            if (postagem.Count < 1) return NoContent();
            return Ok(postagem);
        }

        [HttpPost]
        public IActionResult NovaPostagem([FromBody] NovaPostagemDTO postagem)
        {
            if (!ModelState.IsValid) return BadRequest();
            _repositorio.NovaPostagem(postagem);
            return Created($"api/Postagem/{postagem.Descricao}", postagem);
        }

        [HttpPut]
        public IActionResult AtualizarPostagem([FromBody] AtualizarPostagemDTO postagem)
        {
            if (!ModelState.IsValid) return BadRequest();
            _repositorio.AtualizarPostagem(postagem);
            return Ok(postagem);
        }

        [HttpDelete("deletar/{idPostagem}")]
        public IActionResult DeletarPostagem([FromRoute] int idPostagem)
        {
            _repositorio.DeletarPostagem(idPostagem);
            return NoContent();
        }

        #endregion
    }
}


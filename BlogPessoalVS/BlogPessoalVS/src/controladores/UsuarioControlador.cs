using BlogPessoalVS.src.dtos;
using BlogPessoalVS.src.repositorios;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoalVS.src.controladores
{
    [ApiController] //vai se comportar como controlador, esperando requisicoes e dando respostas
    [Route("api/Usuarios")] // api/Usuarios (tem que ser no plural): endpoint principal que não será alterado / vai determinar quais seram os resultados
    [Produces("application/json")] // quais respostas seram dadas no arq json
    public class UsuarioControlador : ControllerBase // herda do controlador base
    {
        #region Atributos
        private readonly IUsuario _repositorio; // toda vez que usamos o iusuario, ele tbem usa o usuariorepositorio
        #endregion

        #region Construtores
        public UsuarioControlador(IUsuario repositorio)
        {
            _repositorio = repositorio; // acessa os metodos e as consultas de cada endpoind que criarmos
        }
        #endregion

        #region Métodos
        [HttpGet("id/{idUsuario}")] // pega (get) algo do iusuario (parametro id)
        public IActionResult PegarUsuarioPeloId([FromRoute] int idUsuario) // fromroute, coloca o parametro de pesquisa na rota
        {
            var usuario = _repositorio.PegarUsuarioPeloId(idUsuario);

            if (usuario == null) return NotFound(); // se o usuario for nulo ira retornar erro 404, not found
            return Ok(usuario); // se nao for nulo, retornara ok, erro 200
        }

        [HttpGet]
        public IActionResult PegarUsuarioPeloNome([FromQuery] string nomeUsuario) // from query, extrai o elemento do parametro
        {
            var usuarios = _repositorio.PegarUsuarioPeloNome(nomeUsuario);
            if (usuarios.Count < 1) return NoContent(); // < 1 = 0 - NoContent = erro 204
            return Ok(usuarios); // retorna uma lista, por isso esta no plural
        }

        [HttpGet("email/{emailUsuario}")]
        public IActionResult PegarUsuarioPeloEmail([FromRoute] string emailUsuario) 
        {
            var usuario = _repositorio.PegarUsuarioPeloEmail(emailUsuario);

            if (usuario == null) return NotFound(); 
            return Ok(usuario); 
        }

        [HttpPost] // cadastra o novo usuario
        public IActionResult NovoUsuario([FromBody] NovoUsuarioDTO usuario) // frombody = usado para cadastrar novas coisas no formulario
        {
            if (!ModelState.IsValid) return BadRequest(); // querendo saber se nao é valido o que estamos pegando do novousuarioDTO? / BadRequest = erro 400 / ! = negativa do que esta sendo falado
            return Created($"api/Usuarios/{usuario.Email}", usuario);
        }

        [HttpPut]
        public IActionResult AtualizarUsuario([FromBody] AtualizarUsuarioDTO usuario)
        {
            if (!ModelState.IsValid) return BadRequest();
            _repositorio.AtualizarUsuario(usuario);
            return Ok(usuario);
        }

        [HttpDelete("deletar/{idUsuario}")]
        public IActionResult DeletarUsuario([FromRoute] int idUsuario)
        {
            _repositorio.DeletarUsuario(idUsuario);
            return NoContent();
        }

        #endregion
    }
}

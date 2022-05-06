using BlogPessoalVS.src.data;
using BlogPessoalVS.src.dtos;
using BlogPessoalVS.src.modelos;
using System.Collections.Generic;
using System.Linq;

namespace BlogPessoalVS.src.repositorios.implementacoes
{
    public class TemaRepositorio : ITema
    {

        #region Atributos

        private readonly BlogPessoalVSContext _contexto;

        #endregion Atributos

        #region Construtores

        public TemaRepositorio(BlogPessoalVSContext contexto)
        {
            contexto = _contexto;
        }

        #endregion Construtores

        #region Metodos
        public List<TemaModelo> PegarTodosTemas()
        {
            return _contexto.Temas.ToList();
        }

        public void AtualizarTema(AtualizarTemaDTO Tema)
        {
            var temaExistente = PegarTemaPeloId(Tema.Id);
            temaExistente.Descricao = Tema.Descricao;
            _contexto.Temas.Update(temaExistente);
            _contexto.SaveChanges();
        }

        public void DeletarTema(int id)
        {
            _contexto.Temas.Remove(PegarTemaPeloId(id));
            _contexto.SaveChanges();
        }

        public void NovoTema(NovoTemaDTO tema)
        {
            _contexto.Temas.Add(new TemaModelo
            {
                Descricao = tema.Descricao,
            });
               _contexto.SaveChanges();
        }

        public List<TemaModelo> PegarTemaPelaDescricao(string descricao)
        {
            return _contexto.Temas
                .Where(u => u.Descricao.Contains(descricao))
                .ToList();
        }

        public TemaModelo PegarTemaPeloId(int id)
        {
            return _contexto.Temas.FirstOrDefault(u => u.Id == id);
        }


        #endregion Metodos
    }
}

using BlogPessoalVS.src.modelos;
using Microsoft.EntityFrameworkCore; // orm mapeador/organizador para relacao de banco de dados por ex: de Java para SQL

namespace BlogPessoalVS.src.data
{
    /// <summary>
    /// <para>Resumo> Classe contexto, responsavel por carregar contexto e definir Dbsets</para>
    /// <para>Criado por: Leticia Zimerer</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
    public class BlogPessoalVSContext : DbContext // heranca ":"
    {
        public DbSet<UsuarioModelo> Usuarios { get; set; } // DbSet: identidica que sera tabela e demostra como fazer alguma seleção
        public DbSet<TemaModelo> Temas { get; set; }
        public DbSet<PostagemModelo> Postagens { get; set; }

        public BlogPessoalVSContext(DbContextOptions<BlogPessoalVSContext> opt) : base(opt) //  public BlogPessoalVSContext (criando construtor) recebe um parametro (DbContextOptions<BlogPessoalVSContext> options) que recebe um contexto de banco de dados para passar a string de conexao
        {
            
        }

    }
}

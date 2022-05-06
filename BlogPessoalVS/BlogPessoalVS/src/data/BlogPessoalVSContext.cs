using BlogPessoalVS.src.modelos;
using Microsoft.EntityFrameworkCore; // orm mapeador/organizador para relacao de banco de dados por ex: de Java para SQL

namespace BlogPessoalVS.src.data
{
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

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPessoalVS.src.modelos
{
    [Table("tb_postagens")] // uma tabela tem diversas postagens, por isso esta no plural
    public class PostagemModelo
    {
        [Key] // primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // define o numero do id que sera gerado automaticamente
        public int Id { get; set; }

        [Required] // nome seja obrigatio (not null)
        [StringLength(30)] // tamanho da string 50
        public string Titulo { get; set; }

        [Required,StringLength(100)]
        public string Descricao { get; set; }

        public string Foto { get; set; }

        [ForeignKey("fk_usuario")] // chave estrangeira de usuario / colocamos o nome de fk_usuario
        public UsuarioModelo Criador { get; set; } // criador eh o usuario / é uma chave estrangeira foeignkey

        [ForeignKey("fk_tema")]
        public TemaModelo Tema { get; set; }
    }
}

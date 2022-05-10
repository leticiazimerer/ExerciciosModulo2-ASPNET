using BlogPessoalVS.src.utilidades;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogPessoalVS.src.modelos
{
    [Table("tb_usuarios")] // tem que ficar acima de onde ela faz referencia (usuario modelo)
    public class UsuarioModelo
    {
        [Key] // primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // define o numero do id que sera gerado automaticamente
        public int Id { get; set; }

        [Required] // nome seja obrigario (not null)
        [StringLength(50)] // tamanho da string 50
        public string Nome { get; set; }

        [Required]
        [StringLength(30)] // ou [Required, StringLenght(30)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Senha { get; set; }

        public string Foto { get; set; } // foto tambem é string e nao precisa colocar que tem limitação de string

        [Required]
        public TipoUsuario Tipo { get; set; } // api security

        [JsonIgnore] // o usuario le a postagem e a postagem le o usuario, pode dar um looping, por isso precisamos ignorar / se nao vamos ler o postagem pelo usuario, colocamos "json"
        public List<PostagemModelo> MinhasPostagens { get; set; } // conexao com a classe postagem / minhas postagens é o nome que demos
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogPessoalVS.src.modelos
{
    /// <summary>
    /// <para>Resumo> Classe responsavel por representar tb_temas no banco</para>
    /// <para>Criado por: Leticia Zimerer</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
    [Table("tb_temas")]

    public class TemaModelo
    {
        [Key] // primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // define o numero do id que sera gerado automaticamente
        public int Id { get; set; }

        [Required] // nome seja obrigatio (not null)
        [StringLength(20)] // tamanho da string 20
        public string Descricao { get; set; }

        [JsonIgnore]
        public List<PostagemModelo> PostagemRelacionadas { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BlogPessoalVS.src.dtos
{
    /// <summary>
    /// <para>Resumo> Classe espelho para criar um novo Tema</para> // é "para" até para o inglês
    /// <para>Criado por: Leticia Zimerer</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class NovoTemaDTO
    {
        [Required]
        public int Id { get; set; }

        [Required] 
        [StringLength(20)] 
        public string Descricao { get; set; }
        public NovoTemaDTO(string descricao) // para criar o construtor: CTRL+. > GERAR CONSTRUTOR ...
        {
            Descricao = descricao;
        }
    }

    /// <summary>
    /// <para>Resumo> Classe espelho para atualizar um novo tema</para>
    /// <para>Criado por: Leticia Zimerer</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class AtualizarTemaDTO
    {
        [Required]
        public int Id { get; set; }

        [Required] 
        [StringLength(20)] 
        public string Descricao { get; set; }
        public AtualizarTemaDTO(int id, string descricao) // para criar o construtor: CTRL+. > GERAR CONSTRUTOR ...
        {
            Id = id;
            Descricao = descricao;
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BlogPessoalVS.src.dtos
{
    /// <summary>
    /// <para>Resumo> Classe responsavel por representar tb_postagens no banco</para>
    /// <para>Criado por: Leticia Zimerer</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 12/05/2022</para>
    /// </summary>
    public class NovaPostagemDTO
    {
        [Required] 
        [StringLength(30)] 
        public string Titulo { get; set; }

        [Required, StringLength(100)]
        public string Descricao { get; set; }

        public string Foto { get; set; }

        [Required, StringLength(30)]
        public string EmailCriador { get; set; } // ATRIBUTOS para garantir que a pessoa que criou a postagem, seja msm ela, por isso, precisamos saber o id e o email do criador da postagem

        [Required]
        public string DescricaoTema { get; set; } // para garantir que a pessoa que criou a postagem, seja msm ela, por isso, precisamos saber o id e o email do criador da postagem

        public NovaPostagemDTO(string titulo, string descricao, string foto, string emailCriador, string descricaoTema)
        {
            Titulo = titulo;
            Descricao = descricao;
            Foto = foto;
            EmailCriador = emailCriador;
            DescricaoTema = descricaoTema;
        }
    }

    /// <summary>
    /// <para>Resumo> Classe espelho para atualizar uma Postagem</para>
    /// <para>Criado por: Leticia Zimerer</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class AtualizarPostagemDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Titulo { get; set; }

        [Required, StringLength(100)]
        public string Descricao { get; set; }

        public string Foto { get; set; }

        [Required]
        public string DescricaoTema { get; set; }
        public AtualizarPostagemDTO(string titulo, string descricao, string foto, string descricaoTema)
        {
            Titulo = titulo;
            Descricao = descricao;
            Foto = foto;
            DescricaoTema = descricaoTema;
        }
    }
}

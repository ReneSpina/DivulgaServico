namespace DIVULGA_SERVICOS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CAD_AVALIACAO
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CD_PES_JURIDICA { get; set; }

        [Key]
        [Column(Order = 1)]
        public long SQ_AVALIACAO { get; set; }

        public string CD_AVALIADOR { get; set; }

        public string NM_NOME_AVALIADOR { get; set; }

        public int PRECO_QUALIDADE { get; set; }

        public int PONTUALIDADE { get; set; }

        public int ORGANIZACAO { get; set; }

        public int INDICACAO { get; set; }

        public int SATISFACAO_SERVICO { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string DS_DESCRICAO { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string NM_ASSUNTO { get; set; }

        public DateTime DIA_AVALIACAO { get; set; }

        public virtual CAD_PES_JURIDICA CAD_PES_JURIDICA { get; set; }
    }
}

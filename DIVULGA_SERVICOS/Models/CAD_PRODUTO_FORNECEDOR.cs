namespace DIVULGA_SERVICOS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CAD_PRODUTO_FORNECEDOR
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CD_PESSOA { get; set; }

        [Key]
        [Column(Order = 1)]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SQ_PRODUTO { get; set; }

        [StringLength(100)]
        public string NM_PRODUTO { get; set; }

        [StringLength(255)]
        public string DS_DESCRICAO { get; set; }

        public float VALOR_PRODUTO { get; set; }

        public DateTime DT_CRIACAO { get; set; }

        public bool ATIVO { get; set; }

        [StringLength(1000)]
        public string TAGS { get; set; }

        public virtual CAD_PES_FORNECEDOR CAD_PES_FORNECEDOR { get; set; }
    }
}

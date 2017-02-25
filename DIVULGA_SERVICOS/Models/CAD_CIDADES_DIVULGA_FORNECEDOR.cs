namespace DIVULGA_SERVICOS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CAD_CIDADES_DIVULGA_FORNECEDOR
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CD_PESSOA { get; set; }

        [Key]
        [Column(Order = 1)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SQ_CIDADE { get; set; }

        [Required]
        [StringLength(255)]
        public string NM_CIDADE { get; set; }
        
        [Required]
        [StringLength(255)]
        public string NM_ESTADO { get; set; }
        
        public bool BRASIL { get; set; }

        public virtual CAD_PES_FORNECEDOR CAD_PES_FORNECEDOR { get; set; }
    }
}

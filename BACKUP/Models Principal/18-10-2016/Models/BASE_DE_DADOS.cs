namespace DIVULGA_SERVICOS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BASE_DE_DADOS
    {
        [Key]
        public long CD_BASE_DE_DADOS { get; set; }

        [Required]
        [StringLength(255)]
        public string NM_NOME_CATEGORIA { get; set; }

        [Required]
        [StringLength(255)]
        public string NM_NOME_SUBCATEGORIA { get; set; }

        [Required]
        [StringLength(255)]
        public string NM_NOME_ATIVIDADE { get; set; }
    }
}

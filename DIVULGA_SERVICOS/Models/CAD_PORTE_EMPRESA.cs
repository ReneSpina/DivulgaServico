namespace DIVULGA_SERVICOS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    public class CAD_PORTE_EMPRESA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CD_PESSOA { get; set; }

        [Required]
        public bool PESSOA_FISICA { get; set; }

        [Required]
        public bool MICRO_EMPRESA { get; set; }

        [Required]
        public bool PEQUENAS_EMPRESAS { get; set; }

        [Required]
        public bool EMPRESA_GRANDE_PORTE { get; set; }

        public virtual CAD_PES_JURIDICA CAD_PES_JURIDICA { get; set; }
    }
}
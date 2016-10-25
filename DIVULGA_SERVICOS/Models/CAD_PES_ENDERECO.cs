namespace DIVULGA_SERVICOS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CAD_PES_ENDERECO
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CD_PESSOA { get; set; }

        [Key]
        [Column(Order = 1)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SQ_ENDERECO { get; set; }

        [StringLength(255)]
        public string NM_CIDADE { get; set; }

        [StringLength(255)]
        public string NM_LOGRADOURO { get; set; }

        [StringLength(255)]
        public string NM_BAIRRO { get; set; }

        [StringLength(255)]
        public string NM_ESTADO { get; set; }

        [Required]
        [StringLength(20)]
        public string CD_CEP { get; set; }
        

        public int NUMERO { get; set; }

        //[StringLength(20)]
        //public string TP_TIPO_LOGRADOURO { get; set; }

        [StringLength(100)]
        public string CD_LAT { get; set; }

        [StringLength(100)]
        public string CD_LONG { get; set; }

        public virtual CAD_PESSOA CAD_PESSOA { get; set; }
    }
}

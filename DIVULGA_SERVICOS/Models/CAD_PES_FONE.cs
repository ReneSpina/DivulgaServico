namespace DIVULGA_SERVICOS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CAD_PES_FONE
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CD_PESSOA { get; set; }

        [Key]
        [Column(Order = 1)]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SQ_FONE { get; set; }

        [StringLength(30)]
        public string CD_FIXO { get; set; }

        [StringLength(30)]
        public string CD_CELULAR { get; set; }

        public virtual CAD_PESSOA CAD_PESSOA { get; set; }
    }
}

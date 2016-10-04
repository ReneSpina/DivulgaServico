namespace DIVULGA_SERVICOS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CAD_SUB_CAT_ATIV
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long CD_SUB_CAT_ATIV { get; set; }

        public long CD_SUB_CATEGORIA { get; set; }

        public long CD_CATEGORIA { get; set; }

        public long CD_ATIVIDADE { get; set; }

        public virtual CAD_ATIVIDADE CAD_ATIVIDADE { get; set; }

        public virtual CAD_SUB_CATEGORIA CAD_SUB_CATEGORIA { get; set; }
    }
}

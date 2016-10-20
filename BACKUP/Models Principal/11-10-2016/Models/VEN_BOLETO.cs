namespace DIVULGA_SERVICOS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VEN_BOLETO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long CD_ORCAMENTO { get; set; }

        public virtual VEN_ORCAMENTO VEN_ORCAMENTO { get; set; }
    }
}

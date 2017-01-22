namespace DIVULGA_SERVICOS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    public class CAD_FORMA_PAGAMENTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CD_FORMA_PAGAMENTO { get; set; }

        [Required]
        public bool DINHEIRO { get; set; }

        [Required]
        public bool CHEQUE { get; set; }

        [Required]
        public bool DEBITO { get; set; }

        [Required]
        public bool CREDITO { get; set; }

        [Required]
        public bool OUTROS { get; set; }

        public virtual CAD_PES_JURIDICA CAD_PES_JURIDICA { get; set; }
    }
}
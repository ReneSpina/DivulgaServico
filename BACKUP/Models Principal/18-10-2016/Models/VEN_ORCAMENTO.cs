namespace DIVULGA_SERVICOS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VEN_ORCAMENTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VEN_ORCAMENTO()
        {
            CAD_SUB_CATEGORIA = new HashSet<CAD_SUB_CATEGORIA>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CD_ORCAMENTO { get; set; }

        public string CD_PES_USUARIO { get; set; }

        public string CD_PES_JURIDICO { get; set; }

        public virtual CAD_PES_JURIDICA CAD_PES_JURIDICA { get; set; }

        public virtual CAD_PES_USUARIO CAD_PES_USUARIO { get; set; }

        public virtual VEN_BOLETO VEN_BOLETO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAD_SUB_CATEGORIA> CAD_SUB_CATEGORIA { get; set; }
    }
}

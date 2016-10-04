namespace DIVULGA_SERVICOS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CAD_SUB_CATEGORIA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CAD_SUB_CATEGORIA()
        {
            CAD_SUB_CAT_ATIV = new HashSet<CAD_SUB_CAT_ATIV>();
            VEN_ORCAMENTO = new HashSet<VEN_ORCAMENTO>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long CD_SUB_CATEGORIA { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long CD_CATEGORIA { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string NM_NOME { get; set; }

        public virtual CAD_CATEGORIA CAD_CATEGORIA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAD_SUB_CAT_ATIV> CAD_SUB_CAT_ATIV { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VEN_ORCAMENTO> VEN_ORCAMENTO { get; set; }
    }
}

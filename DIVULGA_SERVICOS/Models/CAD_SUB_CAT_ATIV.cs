namespace DIVULGA_SERVICOS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CAD_SUB_CAT_ATIV
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CAD_SUB_CAT_ATIV()
        {
            CAD_ATIVIDADE = new HashSet<CAD_ATIVIDADE>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long CD_SUB_CAT_ATIV { get; set; }

        public long CD_SUB_CATEGORIA { get; set; }

        public long CD_CATEGORIA { get; set; }

        public long CD_ATIVIDADE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAD_ATIVIDADE> CAD_ATIVIDADE { get; set; }

        public virtual CAD_SUB_CATEGORIA CAD_SUB_CATEGORIA { get; set; }
    }
}

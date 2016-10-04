namespace DIVULGA_SERVICOS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CAD_ATIVIDADE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CAD_ATIVIDADE()
        {
            //CAD_SUB_CAT_ATIV = new HashSet<CAD_SUB_CAT_ATIV>();
        }

        [Key]
        public long CD_ATIVIDADE { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string DS_DESCRICAO { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string NM_NOME { get; set; }

        public virtual CAD_SUB_CAT_ATIV CAD_SUB_CAT_ATIV { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<CAD_SUB_CAT_ATIV> CAD_SUB_CAT_ATIV { get; set; }
    }
}

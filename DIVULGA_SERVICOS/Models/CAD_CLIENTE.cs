namespace DIVULGA_SERVICOS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CAD_CLIENTE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CAD_CLIENTE()
        {
            CAD_IMAGEM = new HashSet<CAD_IMAGEM>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CD_PESSOA { get; set; }

        [Key]
        [Column(Order = 1)]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SQ_CLIENTE { get; set; }

        [Required]
        [StringLength(255)]
        public string NM_NOME { get; set; }

        public virtual CAD_PES_JURIDICA CAD_PES_JURIDICA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAD_IMAGEM> CAD_IMAGEM { get; set; }
    }
}
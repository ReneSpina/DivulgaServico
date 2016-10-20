namespace DIVULGA_SERVICOS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq.Expressions;
    using System.Web;
    using System.Web.Mvc;
    public partial class CAD_IMAGEM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CAD_IMAGEM()
        {
            CAD_CLIENTE = new HashSet<CAD_CLIENTE>();
            CAD_DICA = new HashSet<CAD_DICA>();
            CAD_SLIDESHOW = new HashSet<CAD_SLIDESHOW>();
        }

        [Key]
        public long CD_IMAGEM { get; set; }

        [Required]
        [StringLength(255)]
        public string NM_NOME { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAD_CLIENTE> CAD_CLIENTE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAD_DICA> CAD_DICA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAD_SLIDESHOW> CAD_SLIDESHOW { get; set; }
    }
    
}

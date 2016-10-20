namespace DIVULGA_SERVICOS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CAD_CATEGORIA
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public CAD_CATEGORIA()
        //{
        //    CAD_PES_JURIDICA = new HashSet<CAD_PES_JURIDICA>();
        //}

        [Key]
        [Column(Order = 0)]
        public long SQ_CATEGORIA { get; set; }

        //[Key]
        //[Column(Order = 1)]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public long CD_SERVICO { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CD_PES_JURIDICA { get; set; }


        [Required]
        [StringLength(255)]
        public string NM_NOME { get; set; }

        public bool SHOW { get; set; }

        [Required]
        [StringLength(255)]
        public string DS_DESCRICAO { get; set; }

        public virtual CAD_PES_JURIDICA CAD_PES_JURIDICA { get; set; }

        //public virtual ICollection<CAD_PES_JURIDICA> CAD_PES_JURIDICA { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<CAD_SUB_CATEGORIA> CAD_SUB_CATEGORIA { get; set; }
    }
}

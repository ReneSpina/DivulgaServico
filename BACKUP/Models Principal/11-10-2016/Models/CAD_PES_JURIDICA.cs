namespace DIVULGA_SERVICOS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CAD_PES_JURIDICA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CAD_PES_JURIDICA()
        {
            CAD_AVALIACAO = new HashSet<CAD_AVALIACAO>();
            CAD_CLIENTE = new HashSet<CAD_CLIENTE>();
            CAD_DICA = new HashSet<CAD_DICA>();
            CAD_ATIVIDADE = new HashSet<CAD_ATIVIDADE>();
            CAD_SLIDESHOW = new HashSet<CAD_SLIDESHOW>();
            VEN_ORCAMENTO = new HashSet<VEN_ORCAMENTO>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CD_PESSOA { get; set; }

        //[StringLength(1000)]
        public long CD_CODIGO_INDICACAO { get; set; }

        [Required(ErrorMessage = "O CPF/CNPJ � obrigat�rio!")]
        [RegularExpression("^(\\d{14})|(\\d{11})$", ErrorMessage = "Insira um CPF ou CNPJ v�lido (digite somente n�meros)!")]
        [StringLength(30)]
        public string CD_CNPJ { get; set; }

        [StringLength(500)]
        public string DS_LINK_SITE { get; set; }

        [Column(TypeName = "text")]
        [DataType(DataType.MultilineText)]
        public string DS_SOBRE { get; set; }

        [Column(TypeName = "text")]
        [DataType(DataType.MultilineText)]
        public string DS_QUEM_SOMOS { get; set; }

        public int? ID_PLANO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAD_AVALIACAO> CAD_AVALIACAO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAD_CLIENTE> CAD_CLIENTE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAD_DICA> CAD_DICA { get; set; }

        public virtual CAD_PESSOA CAD_PESSOA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAD_ATIVIDADE> CAD_ATIVIDADE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAD_SLIDESHOW> CAD_SLIDESHOW { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VEN_ORCAMENTO> VEN_ORCAMENTO { get; set; }
    }
}

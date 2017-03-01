namespace DIVULGA_SERVICOS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CAD_PES_USUARIO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CAD_PES_USUARIO()
        {
            CAD_AVALIACAO = new HashSet<CAD_AVALIACAO>();
            //VEN_ORCAMENTO = new HashSet<VEN_ORCAMENTO>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CD_PESSOA { get; set; }

        public int CD_INDICACAO { get; set; }

        public bool ATIVO { get; set; }

        //[Required (ErrorMessage = "Por favor, digite sua data de nascimento.")]
        //[DisplayName("2016/12/26")]
        //[Column(TypeName = "date")]
        ////[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime DT_DATA_NASCIMENTO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAD_AVALIACAO> CAD_AVALIACAO { get; set; }

        public virtual CAD_PESSOA CAD_PESSOA { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<VEN_ORCAMENTO> VEN_ORCAMENTO { get; set; }
    }
}

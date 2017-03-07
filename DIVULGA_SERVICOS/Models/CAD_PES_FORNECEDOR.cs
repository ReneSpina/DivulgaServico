namespace DIVULGA_SERVICOS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CAD_PES_FORNECEDOR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CAD_PES_FORNECEDOR()
        {
            CAD_CIDADES_DIVULGA_FORNECEDOR = new HashSet<CAD_CIDADES_DIVULGA_FORNECEDOR>();
            CAD_PRODUTO_FORNECEDOR = new HashSet<CAD_PRODUTO_FORNECEDOR>();
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CD_PESSOA { get; set; }

        [Required(ErrorMessage = "O CPF/CNPJ é obrigatório!")]
        [RegularExpression("^(\\d{14})|(\\d{11})$", ErrorMessage = "Insira um CPF ou CNPJ válido (digite somente números)!")]
        [StringLength(30)]
        public string CD_CNPJ { get; set; }

        public int CD_INDICACAO { get; set; }

        public bool ATIVO { get; set; }

        public int CD_STATUS_PAGT { get; set; }

        [Required]
        public bool ACEITE_CONTRATO { get; set; }

        public virtual CAD_PESSOA CAD_PESSOA { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAD_CIDADES_DIVULGA_FORNECEDOR> CAD_CIDADES_DIVULGA_FORNECEDOR { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAD_PRODUTO_FORNECEDOR> CAD_PRODUTO_FORNECEDOR { get; set; }
    }
}

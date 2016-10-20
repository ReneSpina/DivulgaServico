namespace DIVULGA_SERVICOS.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;
    public partial class CAD_PESSOA : IdentityUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CAD_PESSOA()
        {
            CAD_PES_ENDERECO = new HashSet<CAD_PES_ENDERECO>();
            CAD_PES_FONE = new HashSet<CAD_PES_FONE>();
        }

        [Key]
        public override string Id { get; set; }
        [Required (ErrorMessage = "O NOME � OBRIGAT�RIO!")]
        [RegularExpression("^[a-zA-Z''-'\\s]{1,255}$", ErrorMessage = "INSIRA UM NOME V�LIDO (SOMENTE LETRAS MAI�SCULAS E/OU MIN�SCULAS)!")]
        [StringLength(255)]
        public string NM_NOME_PESSOA { get; set; }

        [Required(ErrorMessage = "INSIRA UM NOME PARA O SEU SITE! (EX.: MEUSITE)")]
        [RegularExpression("^[a-zA-Z]{1,255}$", ErrorMessage = "INSIRA UM NOME V�LIDO PARA O SEU SITE(SOMENTE LETRAS, SEM ESPA�O)")]
        //[Remote("Valida_NM_Site", "CADASTRO_PRESTADOR", ErrorMessage = "Esse nome j� est� cadastrado!")]
        [StringLength(100)]
        public string DS_APELIDO_SITE { get; set; }

        [Required (ErrorMessage = "O EMAIL � OBRIGAT�RIO!")]
        [StringLength(255)]
        //[Remote("Valida_Email", "CADASTRO_PRESTADOR", ErrorMessage = "Esse email j� est� cadastrado!")]
        [DataType(DataType.EmailAddress/*, ErrorMessage = "Por favor, digite um endere�o de email v�lido"*/)]
        public string DS_EMAIL { get; set; }

        [Required(ErrorMessage = "O TELEFONE FIXO � OBRIAT�RIO!")]
        [DisplayName("Ex.: (##) ####-#### ou (##) #####-####!")]
        [StringLength(15)]
        [RegularExpression("^\\([1-9]{2}\\) [2-9][0-9]{3,4}\\-[0-9]{4}$", ErrorMessage = "INSIRA UM TELEFONE V�LIDO (Ex.: (##) ####-#### ou (##) #####-####!)")]
        public string TF_TEL_FIXO { get; set; }

        [Required(ErrorMessage = "O TELEFONE CELULAR � OBRIAT�RIO!")]
        [DisplayName("Ex.: (99) 99999-9999!")]
        [StringLength(15)]
        [RegularExpression("^\\([1-9]{2}\\) [2-9][0-9]{3,4}\\-[0-9]{4}$", ErrorMessage = "INSIRA UM TELEFONE V�LIDO (Ex.: (99) 99999-9999)!")]
        public string TF_TEL_CEL { get; set; }

        [Column(TypeName = "date")]
        public DateTime DT_DATA_CADASTRO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAD_PES_ENDERECO> CAD_PES_ENDERECO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAD_PES_FONE> CAD_PES_FONE { get; set; }

        public virtual CAD_PES_JURIDICA CAD_PES_JURIDICA { get; set; }

        public virtual CAD_PES_USUARIO CAD_PES_USUARIO { get; set; }
    }
}

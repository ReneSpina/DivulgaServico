using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Web.Mvc;

namespace DIVULGA_SERVICOS.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Relembrar?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterUsuarioViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O EMAIL DEVE CONTER NO MÍNIMO 6 CARACTERES!", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "As senhas não são iguais.")]
        public string ConfirmPassword { get; set; }
    }
    
    public class RegisterPrestadorViewModel
    {
        [Required(ErrorMessage = "O NOME É OBRIGATÓRIO!")]
        [Display(Name = "NOME*")]
        public string NM_NOME_PESSOA { get; set; }

        [Required(ErrorMessage = "O TELEFONE FIXO É OBRIGATÓRIO!")]
        [StringLength(15)]
        //[RegularExpression("^\\([1-9]{2}\\) [2-9][0-9]{3,3}\\-[0-9]{4}$", ErrorMessage = "INSIRA UM TELEONE VÁLIDO (Ex.: (##) ####-#### ou (##) #####-####)")]
        [Display(Name = "TELEFONE FIXO*")]
        //[DisplayFormat(DataFormatString = "{0:(##) ####-####}", ApplyFormatInEditMode =true)]
        public string TF_TEL_FIXO { get; set; }

        [Required(ErrorMessage = "O TELEFONE CELULAR É OBRIGATÓRIO")]
        [StringLength(15)]
        //[RegularExpression("^\\([1-9]{2}\\) [2-9][0-9]{3,3}\\-[0-9]{4}$", ErrorMessage = "INSIRA UM TELEONE VÁLIDO (Ex.: (##) ####-#### ou (##) #####-####!")]
        [Display(Name = "CELULAR*")]
        //[DisplayFormat(DataFormatString = "{0:(##) #####-####}", ApplyFormatInEditMode = true)]
        public string TF_TEL_CEL { get; set; }

        [Required(ErrorMessage = "O CPF/CNPJ É OBRIGATÓRIO!")]
        //[RegularExpression("^(\\d{14})|(\\d{11})$", ErrorMessage = "INSIRA UM CPF OU UM CNPJ VÁLIDO (DIGITE SOMENTE NÚMEROS)!")]
        [StringLength(30)]
        [Display(Name = "CPF OU CNPJ*")]
        public string CD_CNPJ { get; set; }

        [Required(ErrorMessage = "O LOGRADOURO É OBRIGATÓRIO!")]
        [Display(Name = "LOGRADOURO*")]
        [StringLength(1000)]
        public string NM_LOGRADOURO { get; set; }

        [Required(ErrorMessage = "O NÚMERO É OBRIGATÓRIO!")]
        [Display(Name = "NÚMERO*")]
        public int NUMERO { get; set; }

        //[Required(ErrorMessage = "O CEP É OBRIGATÓRIO!")]
        [Display(Name = "CEP")]
        [StringLength(20)]
        public string CD_CEP { get; set; }

        [Required(ErrorMessage = "O NOME DA CIDADE É OBRIGATÓRIO!")]
        [Display(Name = "CIDADE*")]
        [StringLength(255)]
        public string NM_CIDADE { get; set; }

        //[Required(ErrorMessage = "O BAIRRO É OBRIGATÓRIO!")]
        //[Display(Name = "BAIRRO")]
        [StringLength(100)]
        public string NM_BAIRRO { get; set; }

        [Required(ErrorMessage = "O ESTADO É OBRIGATÓRIO!")]
        [Display(Name = "ESTADO*")]
        [StringLength(50)]
        public string NM_ESTADO { get; set; }

        [Required(ErrorMessage = "O EMAIL É OBRIGATÓRIO!")]
        [Display(Name = "EMAIL*")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "A CONFIRMAÇÃO DO EMAIL É OBRIGATÓRIA!")]
        [Display(Name = "CONFIRME SEU EMAIL*")]
        [System.ComponentModel.DataAnnotations.Compare("UserName", ErrorMessage = "Os Emails não são iguais!")]
        public string ConfirmUserName { get; set; }

        [Required(ErrorMessage = "A SENHA É OBRIGATÓRIA!")]
        //[StringLength(100, ErrorMessage = "A SENHA DEVE CONTER NO MÍNIMO 6 CARACTERES!", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "SENHA*")]
        public string Password { get; set; }

        [Required(ErrorMessage = "A CONFIRMAÇÃO DA SENHA É OBRIGATÓRIA!")]
        [DataType(DataType.Password)]
        [Display(Name = "CONFIRME SUA SENHA*")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "AS SENHAS NÃO SÃO IGUAIS!")]
        public string ConfirmPassword { get; set; }

        //[Required(ErrorMessage = "O NOME DO SITE É OBRIGATÓRIO!")]
        //[RegularExpression("^[a-zA-Z]{1,255}$", ErrorMessage = "INSIRA UM NOME VÁLIDO PARA O SEU SITE(SOMENTE LETRAS, SEM ESPAÇO)!")]
        ////[Remote("Valida_NM_Site", "Account", ErrorMessage = "ESSE NOME JÁ ESTÁ CADASTRADO!")]
        //[StringLength(100)]
        //[Display(Name = "NOME PARA O SEU SITE")]
        //public string DS_APELIDO_SITE { get; set; }
        
        [Column(TypeName = "date")]
        public DateTime DT_DATA_CADASTRO { get; set; }

        //[StringLength(500)]
        //public string DS_LINK_SITE { get; set; }

        //[Display(Name = "CÓDIGO DE INDICAÇÃO")]
        //[DisplayName("CÓDIGO DE INDICAÇÃO")]
        //public long CD_CODIGO_INDICACAO { get; set; }

        //[Required(ErrorMessage = "O TIPO DE LOGRADOURO É OBRIGATÓRIO!")]
        //[Display(Name = "TIPO DE LOGRADOURO")]
        //[StringLength(30)]
        //public string TP_TIPO_LOGRADOURO { get; set; }        

        //[StringLength(20)]
        //public string TP_TIPO_LOGRADOURO { get; set; }

        [StringLength(100)]
        public string CD_LAT { get; set; }

        [StringLength(100)]
        public string CD_LONG { get; set; }

        [Required]
        public int SEGUNDA_HORA_INICIO { get; set; }

        [Required]
        public int SEGUNDA_HORA_FIM { get; set; }

        [Required]
        public int TERCA_HORA_INICIO { get; set; }

        [Required]
        public int TERCA_HORA_FIM { get; set; }

        [Required]
        public int QUARTA_HORA_INICIO { get; set; }

        [Required]
        public int QUARTA_HORA_FIM { get; set; }

        [Required]
        public int QUINTA_HORA_INICIO { get; set; }

        [Required]
        public int QUINTA_HORA_FIM { get; set; }

        [Required]
        public int SEXTA_HORA_INICIO { get; set; }

        [Required]
        public int SEXTA_HORA_FIM { get; set; }

        [Required]
        public int SABADO_HORA_INICIO { get; set; }

        [Required]
        public int SABADO_HORA_FIM { get; set; }

        [Required]
        public int DOMINGO_HORA_INICIO { get; set; }

        [Required]
        public int DOMINGO_HORA_FIM { get; set; }

        [Required]
        public bool TODO_DIA { get; set; }

        [Required]
        public bool DINHEIRO { get; set; }

        [Required]
        public bool CHEQUE { get; set; }

        [Required]
        public bool DEBITO { get; set; }

        [Required]
        public bool CREDITO { get; set; }

        [Required]
        public bool OUTROS { get; set; }

        [StringLength(6)]
        public string NM_OPERADORA { get; set; }
        
        public bool WHATSAPP { get; set; }

        [Required]
        [StringLength(255)]
        public string NM_NOME_ATIVIDADE { get; set; }

        [Required]
        [StringLength(255)]
        public string DS_DESCRICAO_ATIVIDADE { get; set; }
    }

    public class RegisterFornecedorViewModel
    {
        [Required(ErrorMessage = "O NOME É OBRIGATÓRIO!")]
        [Display(Name = "NOME*")]
        public string NM_NOME_PESSOA { get; set; }

        [Required(ErrorMessage = "O TELEFONE FIXO É OBRIGATÓRIO!")]
        [StringLength(15)]
        //[RegularExpression("^\\([1-9]{2}\\) [2-9][0-9]{3,3}\\-[0-9]{4}$", ErrorMessage = "INSIRA UM TELEONE VÁLIDO (Ex.: (##) ####-#### ou (##) #####-####)")]
        [Display(Name = "TELEFONE FIXO*")]
        //[DisplayFormat(DataFormatString = "{0:(##) ####-####}", ApplyFormatInEditMode =true)]
        public string TF_TEL_FIXO { get; set; }

        [Required(ErrorMessage = "O TELEFONE CELULAR É OBRIGATÓRIO")]
        [StringLength(15)]
        //[RegularExpression("^\\([1-9]{2}\\) [2-9][0-9]{3,3}\\-[0-9]{4}$", ErrorMessage = "INSIRA UM TELEONE VÁLIDO (Ex.: (##) ####-#### ou (##) #####-####!")]
        [Display(Name = "CELULAR*")]
        //[DisplayFormat(DataFormatString = "{0:(##) #####-####}", ApplyFormatInEditMode = true)]
        public string TF_TEL_CEL { get; set; }

        [Required(ErrorMessage = "O CPF/CNPJ É OBRIGATÓRIO!")]
        //[RegularExpression("^(\\d{14})|(\\d{11})$", ErrorMessage = "INSIRA UM CPF OU UM CNPJ VÁLIDO (DIGITE SOMENTE NÚMEROS)!")]
        [StringLength(30)]
        [Display(Name = "CPF OU CNPJ*")]
        public string CD_CNPJ { get; set; }

        [Required(ErrorMessage = "O LOGRADOURO É OBRIGATÓRIO!")]
        [Display(Name = "LOGRADOURO*")]
        [StringLength(1000)]
        public string NM_LOGRADOURO { get; set; }

        [Required(ErrorMessage = "O NÚMERO É OBRIGATÓRIO!")]
        [Display(Name = "NÚMERO*")]
        public int NUMERO { get; set; }

        //[Required(ErrorMessage = "O CEP É OBRIGATÓRIO!")]
        [Display(Name = "CEP")]
        [StringLength(20)]
        public string CD_CEP { get; set; }

        [Required(ErrorMessage = "O NOME DA CIDADE É OBRIGATÓRIO!")]
        [Display(Name = "CIDADE*")]
        [StringLength(255)]
        public string NM_CIDADE { get; set; }

        [Required(ErrorMessage = "O ESTADO É OBRIGATÓRIO!")]
        [Display(Name = "ESTADO*")]
        [StringLength(50)]
        public string NM_ESTADO { get; set; }

        [Required(ErrorMessage = "O EMAIL É OBRIGATÓRIO!")]
        [Display(Name = "EMAIL*")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "A CONFIRMAÇÃO DO EMAIL É OBRIGATÓRIA!")]
        [Display(Name = "CONFIRME SEU EMAIL*")]
        [System.ComponentModel.DataAnnotations.Compare("UserName", ErrorMessage = "Os Emails não são iguais!")]
        public string ConfirmUserName { get; set; }

        [Required(ErrorMessage = "A SENHA É OBRIGATÓRIA!")]
        //[StringLength(100, ErrorMessage = "A SENHA DEVE CONTER NO MÍNIMO 6 CARACTERES!", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "SENHA*")]
        public string Password { get; set; }

        [Required(ErrorMessage = "A CONFIRMAÇÃO DA SENHA É OBRIGATÓRIA!")]
        [DataType(DataType.Password)]
        [Display(Name = "CONFIRME SUA SENHA*")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "AS SENHAS NÃO SÃO IGUAIS!")]
        public string ConfirmPassword { get; set; }

        [Column(TypeName = "date")]
        public DateTime DT_DATA_CADASTRO { get; set; }

        [StringLength(100)]
        public string CD_LAT { get; set; }

        [StringLength(100)]
        public string CD_LONG { get; set; }

        public int CD_INDICACAO { get; set; }

        [StringLength(6)]
        public string NM_OPERADORA { get; set; }

        public bool WHATSAPP { get; set; }

        //[Required]
        //[StringLength(255)]
        //public string NM_NOME_ATIVIDADE { get; set; }

        //[Required]
        //[StringLength(255)]
        //public string DS_DESCRICAO_ATIVIDADE { get; set; }
    }


    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class EditPerfilJuridico
    {

        [Required(ErrorMessage = "O EMAIL É OBRIGATÓRIO!")]
        [Display(Name = "EMAIL")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O NOME É OBRIGATÓRIO!")]
        [Display(Name = "NOME")]
        public string NM_NOME_PESSOA { get; set; }

        [Required(ErrorMessage = "O CPF OU CNPJ É OBRIGATÓRIO!")]
        //[RegularExpression("^(\\d{14})|(\\d{11})$", ErrorMessage = "INSIRA UM CPF OU UM CNPJ VÁLIDO (DIGITE SOMENTE NÚMEROS)!")]
        [StringLength(30)]
        [Display(Name = "CPF OU CNPJ")]
        public string CD_CNPJ { get; set; }

        [Column(TypeName = "text")]
        [DataType(DataType.MultilineText)]
        public string DS_SOBRE { get; set; }

        [Column(TypeName = "text")]
        [DataType(DataType.MultilineText)]
        public string DS_QUEM_SOMOS { get; set; }
        
        public bool ATIVADO { get; set; }
    }


    public class EditarPerfilFornecedor
    {
        [Required(ErrorMessage = "O EMAIL É OBRIGATÓRIO!")]
        [Display(Name = "EMAIL")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O NOME É OBRIGATÓRIO!")]
        [Display(Name = "NOME")]
        public string NM_NOME_PESSOA { get; set; }

        [Required(ErrorMessage = "O CPF OU CNPJ É OBRIGATÓRIO!")]
        [RegularExpression("^(\\d{14})|(\\d{11})$", ErrorMessage = "INSIRA UM CPF OU UM CNPJ VÁLIDO (DIGITE SOMENTE NÚMEROS)!")]
        [StringLength(30)]
        [Display(Name = "CPF OU CNPJ")]
        public string CD_CNPJ { get; set; }

        public bool ATIVO { get; set; }
    }

}

using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace DIVULGA_SERVICOS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string DS_APELIDO_SITE { get; set; }
        public string TF_TEL_FIXO { get; set; }
        public string TF_TEL_CEL { get; set; }
        public DateTime DT_DATA_CADASTRO { get; set; }
        public string NM_NOME_PESSOA { get; set; }
        public string DS_EMAIL { get; set; }
        //public string CD_CNPJ { get; set; }
        //public string DS_LINK_SITE { get; set; }
        //public int CD_CODIGO_INDICACAO { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("connectionstring", throwIfV1Schema: false)
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
namespace DIVULGA_SERVICOS.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Microsoft.AspNet.Identity.EntityFramework;
    public partial class PRINCIPAL : DbContext
    {
        public PRINCIPAL()
            : base("name=connectionstring")
        {
        }

        //public virtual DbSet<CAD_ATIVIDADE> CAD_ATIVIDADE { get; set; }
        public virtual DbSet<CAD_AVALIACAO> CAD_AVALIACAO { get; set; }
        public virtual DbSet<CAD_CATEGORIA> CAD_CATEGORIA { get; set; }
        public virtual DbSet<CAD_FORMA_PAGAMENTO> CAD_FORMA_PAGAMENTO { get; set; }
        public virtual DbSet<CAD_CLIENTE> CAD_CLIENTE { get; set; }
        public virtual DbSet<CAD_DICA> CAD_DICA { get; set; }
        public virtual DbSet<CAD_HORA_ATENDIMENTO> CAD_HORA_ATENDIMENTO { get; set; }
        public virtual DbSet<CAD_IMAGEM> CAD_IMAGEM { get; set; }
        public virtual DbSet<CAD_PES_ENDERECO> CAD_PES_ENDERECO { get; set; }
        public virtual DbSet<CAD_PES_FONE> CAD_PES_FONE { get; set; }
        public virtual DbSet<CAD_PES_JURIDICA> CAD_PES_JURIDICA { get; set; }
        public virtual DbSet<CAD_PES_USUARIO> CAD_PES_USUARIO { get; set; }
        public virtual DbSet<CAD_PESSOA> CAD_PESSOA { get; set; }
        //public virtual DbSet<CAD_SERV_JURIDICA> CAD_SERV_JURIDICA { get; set; }
        //public virtual DbSet<CAD_SLIDESHOW> CAD_SLIDESHOW { get; set; }
        //public virtual DbSet<CAD_SUB_CAT_ATIV> CAD_SUB_CAT_ATIV { get; set; }
        //public virtual DbSet<CAD_SUB_CATEGORIA> CAD_SUB_CATEGORIA { get; set; }
        public virtual DbSet<VEN_BOLETO> VEN_BOLETO { get; set; }
        public virtual DbSet<VEN_ORCAMENTO> VEN_ORCAMENTO { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //modelBuilder.Entity<CAD_ATIVIDADE>()
            //    .HasMany(e => e.CAD_SUB_CAT_ATIV)
            //    .WithRequired(e => e.CAD_ATIVIDADE)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<CAD_AVALIACAO>()
                .Property(e => e.DS_DESCRICAO)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_CATEGORIA>()
                .Property(e => e.NM_NOME)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_CATEGORIA>()
                .Property(e => e.DS_DESCRICAO)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_CATEGORIA>()
                .Property(p => p.SQ_CATEGORIA)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<CAD_CATEGORIA>()
                .HasRequired(c => c.CAD_PES_JURIDICA)
                .WithMany(p => p.CAD_CATEGORIA)
                .HasForeignKey(p => p.CD_PES_JURIDICA);

            //modelBuilder.Entity<CAD_CATEGORIA>()
            //    .HasMany(e => e.CAD_SUB_CATEGORIA)
            //    .WithRequired(e => e.CAD_CATEGORIA)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<CAD_SUB_CAT_ATIV>()
            //    .HasMany(e => e.CAD_ATIVIDADE)
            //    .WithRequired(e => e.CAD_SUB_CAT_ATIV)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<CAD_SERV_JURIDICA>()
            //   .HasMany(e => e.CAD_CATEGORIA)
            //   .WithRequired(e => e.CAD_SERV_JURIDICA)
            //   .WillCascadeOnDelete(false);

            //modelBuilder.Entity<CAD_CATEGORIA>()
            //.HasMany(x => x.CAD_PES_JURIDICA)
            //.WithMany(x => x.CAD_CATEGORIA)
            //.Map(m =>
            //{
            //    m.MapLeftKey("CD_CATEGORIA");
            //    m.MapRightKey("CD_PESSOA");
            //    m.ToTable("CAD_SERV_JURIDICA");
            //});

            modelBuilder.Entity<CAD_CLIENTE>()
                .Property(e => e.NM_NOME)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_CLIENTE>()
                .Property(p => p.SQ_CLIENTE)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<CAD_CLIENTE>()
                .HasRequired(c => c.CAD_PES_JURIDICA)
                .WithMany(p => p.CAD_CLIENTE)
                .HasForeignKey(p => p.CD_PESSOA);

            modelBuilder.Entity<CAD_DICA>()
                .Property(e => e.NM__NOME)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_DICA>()
                .Property(p => p.SQ_DICA)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<CAD_DICA>()
                .Property(e => e.DS_DESCRICAO)
                .IsUnicode(false);

            //modelBuilder.Entity<BASE_DE_DADOS>()
            //    .Property(e => e.NM_NOME_CATEGORIA)
            //    .IsUnicode(false);

            //modelBuilder.Entity<BASE_DE_DADOS>()
            //    .Property(e => e.NM_NOME_ATIVIDADE)
            //    .IsUnicode(false);

            //modelBuilder.Entity<BASE_DE_DADOS>()
            //    .Property(e => e.NM_NOME_SUBCATEGORIA)
            //    .IsUnicode(false);

            modelBuilder.Entity<CAD_IMAGEM>()
                .Property(e => e.NM_NOME)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_IMAGEM>()
                .HasMany(e => e.CAD_CLIENTE)
                .WithMany(e => e.CAD_IMAGEM)
                .Map(m => m.ToTable("CAD_IMG_CLIENTE").MapLeftKey("CD_IMAGEM").MapRightKey(new[] { "CD_PESSOA", "SQ_CLIENTE" }));

            modelBuilder.Entity<CAD_IMAGEM>()
                .HasMany(e => e.CAD_DICA)
                .WithMany(e => e.CAD_IMAGEM)
                .Map(m => m.ToTable("CAD_IMG_DICA").MapLeftKey("CD_IMAGEM").MapRightKey(new[] { "CD_PESSOA", "SQ_DICA" }));

            //modelBuilder.Entity<CAD_IMAGEM>()
            //    .HasMany(e => e.CAD_SLIDESHOW)
            //    .WithMany(e => e.CAD_IMAGEM)
            //    .Map(m => m.ToTable("CAD_IMG_SLIDE").MapLeftKey("CD_IMAGEM").MapRightKey(new[] { "CD_PESSOA", "SQ_SLIDESHOW" }));

            modelBuilder.Entity<CAD_PES_ENDERECO>()
                .Property(e => e.NM_CIDADE)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_PES_ENDERECO>()
                .Property(e => e.NM_LOGRADOURO)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_PES_ENDERECO>()
                .Property(e => e.NM_BAIRRO)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_PES_ENDERECO>()
                .Property(e => e.CD_CEP)
                .IsUnicode(false);

            //modelBuilder.Entity<CAD_PES_ENDERECO>()
            //    .Property(e => e.TP_TIPO_LOGRADOURO)
            //    .IsUnicode(false);

            modelBuilder.Entity<CAD_PES_ENDERECO>()
                .Property(p => p.SQ_ENDERECO)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<CAD_PES_ENDERECO>()
                .HasRequired(c => c.CAD_PESSOA)
                .WithMany(p => p.CAD_PES_ENDERECO)
                .HasForeignKey(p => p.CD_PESSOA);

            modelBuilder.Entity<CAD_PES_FONE>()
                .Property(e => e.CD_FIXO)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_PES_FONE>()
                .Property(e => e.CD_CELULAR)
                .IsUnicode(false);
            
            modelBuilder.Entity<CAD_PES_JURIDICA>()
                .Property(e => e.CD_CNPJ)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_PES_JURIDICA>()
                .Property(e => e.DS_LINK_SITE)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_PES_JURIDICA>()
                .Property(e => e.DS_SOBRE)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_PES_JURIDICA>()
                .Property(e => e.DS_QUEM_SOMOS)
                .IsUnicode(false);

            //modelBuilder.Entity<CAD_PES_JURIDICA>()
            //    .HasMany(e => e.CAD_AVALIACAO)
            //    .WithRequired(e => e.CAD_PES_JURIDICA)
            //    .HasForeignKey(e => e.CD_PES_JURIDICA)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<CAD_PES_JURIDICA>()
                .HasMany(e => e.CAD_CLIENTE)
                .WithRequired(e => e.CAD_PES_JURIDICA)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CAD_PES_JURIDICA>()
                .HasMany(e => e.CAD_DICA)
                .WithRequired(e => e.CAD_PES_JURIDICA)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<CAD_PES_JURIDICA>()
            //    .HasMany(e => e.CAD_SERV_JURIDICA)
            //    .WithRequired(e => e.CAD_PES_JURIDICA)
            //    .HasForeignKey(e => e.CD_PES_JURIDICA)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<CAD_PES_JURIDICA>()
            //    .HasMany(e => e.CAD_SLIDESHOW)
            //    .WithRequired(e => e.CAD_PES_JURIDICA)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<CAD_PES_JURIDICA>()
                .HasMany(e => e.VEN_ORCAMENTO)
                .WithRequired(e => e.CAD_PES_JURIDICA)
                .HasForeignKey(e => e.CD_PES_JURIDICO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CAD_PES_USUARIO>()
                .HasMany(e => e.CAD_AVALIACAO)
                .WithRequired(e => e.CAD_PES_USUARIO)
                .HasForeignKey(e => e.CD_PES_USUARIO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CAD_PES_USUARIO>()
                .HasMany(e => e.VEN_ORCAMENTO)
                .WithRequired(e => e.CAD_PES_USUARIO)
                .HasForeignKey(e => e.CD_PES_USUARIO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CAD_PESSOA>()
                .Property(e => e.NM_NOME_PESSOA)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_PESSOA>()
                .Property(e => e.DS_APELIDO_SITE)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_PESSOA>()
                .Property(e => e.DS_EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_PESSOA>()
                .Property(e => e.TF_TEL_FIXO)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_PESSOA>()
                .Property(e => e.TF_TEL_CEL)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_PESSOA>()
                .HasMany(e => e.CAD_PES_ENDERECO)
                .WithRequired(e => e.CAD_PESSOA)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CAD_PESSOA>()
                .HasMany(e => e.CAD_PES_FONE)
                .WithRequired(e => e.CAD_PESSOA)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CAD_PESSOA>()
                .HasOptional(e => e.CAD_PES_JURIDICA)
                .WithRequired(e => e.CAD_PESSOA);


            modelBuilder.Entity<CAD_PESSOA>()
                .HasOptional(e => e.CAD_PES_USUARIO)
                .WithRequired(e => e.CAD_PESSOA);

            modelBuilder.Entity<CAD_PES_JURIDICA>()
                .HasOptional(e => e.CAD_FORMA_PAGAMENTO)
                .WithRequired(e => e.CAD_PES_JURIDICA);

            //modelBuilder.Entity<CAD_SUB_CATEGORIA>()
            //    .Property(e => e.NM_NOME)
            //    .IsUnicode(false);

            //modelBuilder.Entity<CAD_SUB_CATEGORIA>()
            //    .HasMany(e => e.CAD_SUB_CAT_ATIV)
            //    .WithRequired(e => e.CAD_SUB_CATEGORIA)
            //    .HasForeignKey(e => new { e.CD_SUB_CATEGORIA, e.CD_CATEGORIA })
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<VEN_ORCAMENTO>()
                .HasOptional(e => e.VEN_BOLETO)
                .WithRequired(e => e.VEN_ORCAMENTO);
            
            //modelBuilder.Entity<VEN_ORCAMENTO>()
            //    .HasMany(e => e.CAD_SUB_CATEGORIA)
            //    .WithMany(e => e.VEN_ORCAMENTO)
            //    .Map(m => m.ToTable("VEN_ORC_SERVICO"));/*.MapLeftKey("CD_ORCAMENTO").MapRightKey(new[] { "CD_SUB_CATEGORIA", "CD_CATEGORIA" }));*/

            modelBuilder.Entity<CAD_PESSOA>()
               .ToTable("AspNetUsers")
               .Property(p => p.Id)
               .HasColumnName("Id");

            modelBuilder.Entity<IdentityUserLogin>()
               .ToTable("AspNetUserLogins")
               .HasKey(t => t.UserId);

            modelBuilder.Entity<IdentityUserRole>()
               .ToTable("AspNetUserRoles")
               .HasKey(t => t.UserId);

            modelBuilder.Entity<IdentityRole>()
               .ToTable("AspNetRoles")
               .HasKey(t => t.Id);

            modelBuilder.Entity<IdentityUserClaim>()
               .ToTable("AspNetUserClaims")
               .HasKey(t => t.Id);

        }
    }
}

namespace DIVULGA_SERVICOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Principal2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAD_AVALIACAO",
                c => new
                    {
                        CD_PES_JURIDICA = c.String(nullable: false, maxLength: 128),
                        CD_PES_USUARIO = c.String(nullable: false, maxLength: 128),
                        NT_MEDIA = c.Int(nullable: false),
                        PRECO_QUALIDADE = c.Int(nullable: false),
                        PONTUALIDADE = c.Int(nullable: false),
                        ORGANIZACAO = c.Int(nullable: false),
                        INDICACAO = c.Int(nullable: false),
                        SATISFACAO_ATENDIMENTO = c.Int(nullable: false),
                        SATISFACAO_SERVICO = c.Int(nullable: false),
                        DS_DESCRICAO = c.String(nullable: false, unicode: false, storeType: "text"),
                        CAD_PES_JURIDICA_CD_PESSOA = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.CD_PES_JURIDICA, t.CD_PES_USUARIO })
                .ForeignKey("dbo.CAD_PES_JURIDICA", t => t.CAD_PES_JURIDICA_CD_PESSOA)
                .ForeignKey("dbo.CAD_PES_USUARIO", t => t.CD_PES_USUARIO)
                .Index(t => t.CD_PES_USUARIO)
                .Index(t => t.CAD_PES_JURIDICA_CD_PESSOA);
            
            CreateTable(
                "dbo.CAD_PES_JURIDICA",
                c => new
                    {
                        CD_PESSOA = c.String(nullable: false, maxLength: 128),
                        CD_CODIGO_INDICACAO = c.Long(nullable: false),
                        CD_CNPJ = c.String(nullable: false, maxLength: 30, unicode: false),
                        DS_LINK_SITE = c.String(maxLength: 500, unicode: false),
                        DS_SOBRE = c.String(unicode: false, storeType: "text"),
                        DS_QUEM_SOMOS = c.String(unicode: false, storeType: "text"),
                        ID_PLANO = c.Int(),
                    })
                .PrimaryKey(t => t.CD_PESSOA)
                .ForeignKey("dbo.AspNetUsers", t => t.CD_PESSOA)
                .Index(t => t.CD_PESSOA);
            
            CreateTable(
                "dbo.CAD_CATEGORIA",
                c => new
                    {
                        SQ_CATEGORIA = c.Long(nullable: false, identity: true),
                        CD_PES_JURIDICA = c.String(nullable: false, maxLength: 128),
                        NM_NOME = c.String(nullable: false, maxLength: 255, unicode: false),
                        SHOW = c.Boolean(nullable: false),
                        DS_DESCRICAO = c.String(nullable: false, maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => new { t.SQ_CATEGORIA, t.CD_PES_JURIDICA })
                .ForeignKey("dbo.CAD_PES_JURIDICA", t => t.CD_PES_JURIDICA, cascadeDelete: true)
                .Index(t => t.CD_PES_JURIDICA);
            
            CreateTable(
                "dbo.CAD_CLIENTE",
                c => new
                    {
                        CD_PESSOA = c.String(nullable: false, maxLength: 128),
                        SQ_CLIENTE = c.Int(nullable: false, identity: true),
                        NM_NOME = c.String(nullable: false, maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => new { t.CD_PESSOA, t.SQ_CLIENTE })
                .ForeignKey("dbo.CAD_PES_JURIDICA", t => t.CD_PESSOA)
                .Index(t => t.CD_PESSOA);
            
            CreateTable(
                "dbo.CAD_IMAGEM",
                c => new
                    {
                        CD_IMAGEM = c.Long(nullable: false, identity: true),
                        NM_NOME = c.String(nullable: false, maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.CD_IMAGEM);
            
            CreateTable(
                "dbo.CAD_DICA",
                c => new
                    {
                        CD_PESSOA = c.String(nullable: false, maxLength: 128),
                        SQ_DICA = c.Int(nullable: false, identity: true),
                        NM__NOME = c.String(nullable: false, maxLength: 255, unicode: false),
                        DS_DESCRICAO = c.String(nullable: false, unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => new { t.CD_PESSOA, t.SQ_DICA })
                .ForeignKey("dbo.CAD_PES_JURIDICA", t => t.CD_PESSOA)
                .Index(t => t.CD_PESSOA);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        NM_NOME_PESSOA = c.String(nullable: false, maxLength: 255, unicode: false),
                        DS_APELIDO_SITE = c.String(nullable: false, maxLength: 100, unicode: false),
                        DS_EMAIL = c.String(nullable: false, maxLength: 255, unicode: false),
                        TF_TEL_FIXO = c.String(nullable: false, maxLength: 15, unicode: false),
                        TF_TEL_CEL = c.String(nullable: false, maxLength: 15, unicode: false),
                        DT_DATA_CADASTRO = c.DateTime(nullable: false, storeType: "date"),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CAD_PES_ENDERECO",
                c => new
                    {
                        CD_PESSOA = c.String(nullable: false, maxLength: 128),
                        SQ_ENDERECO = c.Int(nullable: false, identity: true),
                        NM_CIDADE = c.String(maxLength: 255, unicode: false),
                        NM_LOGRADOURO = c.String(maxLength: 255, unicode: false),
                        NM_BAIRRO = c.String(maxLength: 255, unicode: false),
                        NM_ESTADO = c.String(maxLength: 255),
                        CD_CEP = c.String(nullable: false, maxLength: 20, unicode: false),
                        NUMERO = c.Int(nullable: false),
                        localizacao = c.Geography(),
                    })
                .PrimaryKey(t => new { t.CD_PESSOA, t.SQ_ENDERECO })
                .ForeignKey("dbo.AspNetUsers", t => t.CD_PESSOA)
                .Index(t => t.CD_PESSOA);
            
            CreateTable(
                "dbo.CAD_PES_FONE",
                c => new
                    {
                        CD_PESSOA = c.String(nullable: false, maxLength: 128),
                        SQ_FONE = c.Int(nullable: false),
                        CD_FIXO = c.String(maxLength: 30, unicode: false),
                        CD_CELULAR = c.String(maxLength: 30, unicode: false),
                        CAD_PESSOA_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.CD_PESSOA, t.SQ_FONE })
                .ForeignKey("dbo.AspNetUsers", t => t.CAD_PESSOA_Id)
                .Index(t => t.CAD_PESSOA_Id);
            
            CreateTable(
                "dbo.CAD_PES_USUARIO",
                c => new
                    {
                        CD_PESSOA = c.String(nullable: false, maxLength: 128),
                        DT_DATA_NASCIMENTO = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.CD_PESSOA)
                .ForeignKey("dbo.AspNetUsers", t => t.CD_PESSOA)
                .Index(t => t.CD_PESSOA);
            
            CreateTable(
                "dbo.VEN_ORCAMENTO",
                c => new
                    {
                        CD_ORCAMENTO = c.Long(nullable: false, identity: true),
                        CD_PES_USUARIO = c.String(nullable: false, maxLength: 128),
                        CD_PES_JURIDICO = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.CD_ORCAMENTO)
                .ForeignKey("dbo.CAD_PES_USUARIO", t => t.CD_PES_USUARIO)
                .ForeignKey("dbo.CAD_PES_JURIDICA", t => t.CD_PES_JURIDICO)
                .Index(t => t.CD_PES_USUARIO)
                .Index(t => t.CD_PES_JURIDICO);
            
            CreateTable(
                "dbo.VEN_BOLETO",
                c => new
                    {
                        CD_ORCAMENTO = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.CD_ORCAMENTO)
                .ForeignKey("dbo.VEN_ORCAMENTO", t => t.CD_ORCAMENTO)
                .Index(t => t.CD_ORCAMENTO);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        CAD_PESSOA_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CAD_PESSOA_Id)
                .Index(t => t.CAD_PESSOA_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        CAD_PESSOA_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.CAD_PESSOA_Id)
                .Index(t => t.CAD_PESSOA_Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        CAD_PESSOA_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.CAD_PESSOA_Id)
                .ForeignKey("dbo.AspNetRoles", t => t.IdentityRole_Id)
                .Index(t => t.CAD_PESSOA_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CAD_IMG_CLIENTE",
                c => new
                    {
                        CD_IMAGEM = c.Long(nullable: false),
                        CD_PESSOA = c.String(nullable: false, maxLength: 128),
                        SQ_CLIENTE = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CD_IMAGEM, t.CD_PESSOA, t.SQ_CLIENTE })
                .ForeignKey("dbo.CAD_IMAGEM", t => t.CD_IMAGEM, cascadeDelete: true)
                .ForeignKey("dbo.CAD_CLIENTE", t => new { t.CD_PESSOA, t.SQ_CLIENTE }, cascadeDelete: true)
                .Index(t => t.CD_IMAGEM)
                .Index(t => new { t.CD_PESSOA, t.SQ_CLIENTE });
            
            CreateTable(
                "dbo.CAD_IMG_DICA",
                c => new
                    {
                        CD_IMAGEM = c.Long(nullable: false),
                        CD_PESSOA = c.String(nullable: false, maxLength: 128),
                        SQ_DICA = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CD_IMAGEM, t.CD_PESSOA, t.SQ_DICA })
                .ForeignKey("dbo.CAD_IMAGEM", t => t.CD_IMAGEM, cascadeDelete: true)
                .ForeignKey("dbo.CAD_DICA", t => new { t.CD_PESSOA, t.SQ_DICA }, cascadeDelete: true)
                .Index(t => t.CD_IMAGEM)
                .Index(t => new { t.CD_PESSOA, t.SQ_DICA });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "IdentityRole_Id", "dbo.AspNetRoles");
            DropForeignKey("dbo.VEN_ORCAMENTO", "CD_PES_JURIDICO", "dbo.CAD_PES_JURIDICA");
            DropForeignKey("dbo.AspNetUserRoles", "CAD_PESSOA_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "CAD_PESSOA_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "CAD_PESSOA_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CAD_PES_USUARIO", "CD_PESSOA", "dbo.AspNetUsers");
            DropForeignKey("dbo.VEN_ORCAMENTO", "CD_PES_USUARIO", "dbo.CAD_PES_USUARIO");
            DropForeignKey("dbo.VEN_BOLETO", "CD_ORCAMENTO", "dbo.VEN_ORCAMENTO");
            DropForeignKey("dbo.CAD_AVALIACAO", "CD_PES_USUARIO", "dbo.CAD_PES_USUARIO");
            DropForeignKey("dbo.CAD_PES_JURIDICA", "CD_PESSOA", "dbo.AspNetUsers");
            DropForeignKey("dbo.CAD_PES_FONE", "CAD_PESSOA_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CAD_PES_ENDERECO", "CD_PESSOA", "dbo.AspNetUsers");
            DropForeignKey("dbo.CAD_DICA", "CD_PESSOA", "dbo.CAD_PES_JURIDICA");
            DropForeignKey("dbo.CAD_CLIENTE", "CD_PESSOA", "dbo.CAD_PES_JURIDICA");
            DropForeignKey("dbo.CAD_IMG_DICA", new[] { "CD_PESSOA", "SQ_DICA" }, "dbo.CAD_DICA");
            DropForeignKey("dbo.CAD_IMG_DICA", "CD_IMAGEM", "dbo.CAD_IMAGEM");
            DropForeignKey("dbo.CAD_IMG_CLIENTE", new[] { "CD_PESSOA", "SQ_CLIENTE" }, "dbo.CAD_CLIENTE");
            DropForeignKey("dbo.CAD_IMG_CLIENTE", "CD_IMAGEM", "dbo.CAD_IMAGEM");
            DropForeignKey("dbo.CAD_CATEGORIA", "CD_PES_JURIDICA", "dbo.CAD_PES_JURIDICA");
            DropForeignKey("dbo.CAD_AVALIACAO", "CAD_PES_JURIDICA_CD_PESSOA", "dbo.CAD_PES_JURIDICA");
            DropIndex("dbo.CAD_IMG_DICA", new[] { "CD_PESSOA", "SQ_DICA" });
            DropIndex("dbo.CAD_IMG_DICA", new[] { "CD_IMAGEM" });
            DropIndex("dbo.CAD_IMG_CLIENTE", new[] { "CD_PESSOA", "SQ_CLIENTE" });
            DropIndex("dbo.CAD_IMG_CLIENTE", new[] { "CD_IMAGEM" });
            DropIndex("dbo.AspNetUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "CAD_PESSOA_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "CAD_PESSOA_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "CAD_PESSOA_Id" });
            DropIndex("dbo.VEN_BOLETO", new[] { "CD_ORCAMENTO" });
            DropIndex("dbo.VEN_ORCAMENTO", new[] { "CD_PES_JURIDICO" });
            DropIndex("dbo.VEN_ORCAMENTO", new[] { "CD_PES_USUARIO" });
            DropIndex("dbo.CAD_PES_USUARIO", new[] { "CD_PESSOA" });
            DropIndex("dbo.CAD_PES_FONE", new[] { "CAD_PESSOA_Id" });
            DropIndex("dbo.CAD_PES_ENDERECO", new[] { "CD_PESSOA" });
            DropIndex("dbo.CAD_DICA", new[] { "CD_PESSOA" });
            DropIndex("dbo.CAD_CLIENTE", new[] { "CD_PESSOA" });
            DropIndex("dbo.CAD_CATEGORIA", new[] { "CD_PES_JURIDICA" });
            DropIndex("dbo.CAD_PES_JURIDICA", new[] { "CD_PESSOA" });
            DropIndex("dbo.CAD_AVALIACAO", new[] { "CAD_PES_JURIDICA_CD_PESSOA" });
            DropIndex("dbo.CAD_AVALIACAO", new[] { "CD_PES_USUARIO" });
            DropTable("dbo.CAD_IMG_DICA");
            DropTable("dbo.CAD_IMG_CLIENTE");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.VEN_BOLETO");
            DropTable("dbo.VEN_ORCAMENTO");
            DropTable("dbo.CAD_PES_USUARIO");
            DropTable("dbo.CAD_PES_FONE");
            DropTable("dbo.CAD_PES_ENDERECO");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.CAD_DICA");
            DropTable("dbo.CAD_IMAGEM");
            DropTable("dbo.CAD_CLIENTE");
            DropTable("dbo.CAD_CATEGORIA");
            DropTable("dbo.CAD_PES_JURIDICA");
            DropTable("dbo.CAD_AVALIACAO");
        }
    }
}

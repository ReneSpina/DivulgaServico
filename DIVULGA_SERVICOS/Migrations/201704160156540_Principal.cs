namespace DIVULGA_SERVICOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Principal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAD_AVALIACAO",
                c => new
                    {
                        CD_PES_JURIDICA = c.String(nullable: false, maxLength: 128),
                        SQ_AVALIACAO = c.Long(nullable: false, identity: true),
                        CD_AVALIADOR = c.String(),
                        NM_NOME_AVALIADOR = c.String(),
                        PRECO_QUALIDADE = c.Int(nullable: false),
                        PONTUALIDADE = c.Int(nullable: false),
                        ORGANIZACAO = c.Int(nullable: false),
                        INDICACAO = c.Int(nullable: false),
                        SATISFACAO_SERVICO = c.Int(nullable: false),
                        DS_DESCRICAO = c.String(nullable: false, unicode: false, storeType: "text"),
                        NM_ASSUNTO = c.String(nullable: false, unicode: false, storeType: "text"),
                        DIA_AVALIACAO = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.CD_PES_JURIDICA, t.SQ_AVALIACAO })
                .ForeignKey("dbo.CAD_PES_JURIDICA", t => t.CD_PES_JURIDICA)
                .Index(t => t.CD_PES_JURIDICA);
            
            CreateTable(
                "dbo.CAD_PES_JURIDICA",
                c => new
                    {
                        CD_PESSOA = c.String(nullable: false, maxLength: 128),
                        NM_NOME_PRESTADOR = c.String(nullable: false, maxLength: 255),
                        CD_CNPJ = c.String(nullable: false, maxLength: 30),
                        DS_SOBRE = c.String(unicode: false, storeType: "text"),
                        DS_QUEM_SOMOS = c.String(unicode: false, storeType: "text"),
                        TODO_DIA = c.Boolean(nullable: false),
                        ACEITE_CONTRATO = c.Boolean(nullable: false),
                        DIVULGACAO = c.Boolean(nullable: false),
                        ATIVO = c.Boolean(nullable: false),
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
                        DS_DESCRICAO = c.String(nullable: false, maxLength: 1000, unicode: false),
                    })
                .PrimaryKey(t => new { t.SQ_CATEGORIA, t.CD_PES_JURIDICA })
                .ForeignKey("dbo.CAD_PES_JURIDICA", t => t.CD_PES_JURIDICA, cascadeDelete: true)
                .Index(t => t.CD_PES_JURIDICA);
            
            CreateTable(
                "dbo.CAD_FORMA_PAGAMENTO",
                c => new
                    {
                        CD_PESSOA = c.String(nullable: false, maxLength: 128),
                        DINHEIRO = c.Boolean(nullable: false),
                        CHEQUE = c.Boolean(nullable: false),
                        DEBITO = c.Boolean(nullable: false),
                        CREDITO = c.Boolean(nullable: false),
                        OUTROS = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CD_PESSOA)
                .ForeignKey("dbo.CAD_PES_JURIDICA", t => t.CD_PESSOA)
                .Index(t => t.CD_PESSOA);
            
            CreateTable(
                "dbo.CAD_HORA_ATENDIMENTO",
                c => new
                    {
                        DIA_SEMANA = c.Int(nullable: false),
                        CD_PES_JURIDICA = c.String(nullable: false, maxLength: 128),
                        HORA_INICIO = c.Int(nullable: false),
                        HORA_FIM = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DIA_SEMANA, t.CD_PES_JURIDICA })
                .ForeignKey("dbo.CAD_PES_JURIDICA", t => t.CD_PES_JURIDICA, cascadeDelete: true)
                .Index(t => t.CD_PES_JURIDICA);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        NM_NOME_PESSOA = c.String(nullable: false, maxLength: 255),
                        NEWSLETTER = c.Boolean(nullable: false),
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
                        NM_CIDADE = c.String(nullable: false, maxLength: 255, unicode: false),
                        NM_LOGRADOURO = c.String(nullable: false, maxLength: 255, unicode: false),
                        NM_BAIRRO = c.String(maxLength: 255, unicode: false),
                        NM_ESTADO = c.String(nullable: false, maxLength: 255),
                        CD_CEP = c.String(maxLength: 20, unicode: false),
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
                        SQ_FONE = c.Int(nullable: false, identity: true),
                        CD_FIXO = c.String(maxLength: 30),
                        CD_CELULAR = c.String(maxLength: 30),
                        NM_OPERADORA = c.String(maxLength: 6),
                        WHATSAPP = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.CD_PESSOA, t.SQ_FONE })
                .ForeignKey("dbo.AspNetUsers", t => t.CD_PESSOA)
                .Index(t => t.CD_PESSOA);
            
            CreateTable(
                "dbo.CAD_PES_FORNECEDOR",
                c => new
                    {
                        CD_PESSOA = c.String(nullable: false, maxLength: 128),
                        CD_CNPJ = c.String(nullable: false, maxLength: 30),
                        ATIVO = c.Boolean(nullable: false),
                        CD_STATUS_PAGT = c.Int(nullable: false),
                        ACEITE_CONTRATO = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CD_PESSOA)
                .ForeignKey("dbo.AspNetUsers", t => t.CD_PESSOA)
                .Index(t => t.CD_PESSOA);
            
            CreateTable(
                "dbo.CAD_CIDADES_DIVULGA_FORNECEDOR",
                c => new
                    {
                        CD_PESSOA = c.String(nullable: false, maxLength: 128),
                        SQ_CIDADE = c.Int(nullable: false, identity: true),
                        NM_CIDADE = c.String(maxLength: 255),
                        NM_ESTADO = c.String(maxLength: 255),
                        DILGAR_ESTADO = c.Boolean(nullable: false),
                        BRASIL = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.CD_PESSOA, t.SQ_CIDADE })
                .ForeignKey("dbo.CAD_PES_FORNECEDOR", t => t.CD_PESSOA)
                .Index(t => t.CD_PESSOA);
            
            CreateTable(
                "dbo.CAD_PRODUTO_FORNECEDOR",
                c => new
                    {
                        CD_PESSOA = c.String(nullable: false, maxLength: 128),
                        SQ_PRODUTO = c.Int(nullable: false, identity: true),
                        NM_PRODUTO = c.String(maxLength: 100),
                        DS_DESCRICAO = c.String(maxLength: 255),
                        VALOR_PRODUTO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DT_CRIACAO = c.DateTime(nullable: false),
                        ATIVO = c.Boolean(nullable: false),
                        TAGS = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => new { t.CD_PESSOA, t.SQ_PRODUTO })
                .ForeignKey("dbo.CAD_PES_FORNECEDOR", t => t.CD_PESSOA)
                .Index(t => t.CD_PESSOA);
            
            CreateTable(
                "dbo.CAD_PES_USUARIO",
                c => new
                    {
                        CD_PESSOA = c.String(nullable: false, maxLength: 128),
                        ATIVO = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CD_PESSOA)
                .ForeignKey("dbo.AspNetUsers", t => t.CD_PESSOA)
                .Index(t => t.CD_PESSOA);
            
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
                "dbo.CAD_PORTE_EMPRESA",
                c => new
                    {
                        CD_PESSOA = c.String(nullable: false, maxLength: 128),
                        PESSOA_FISICA = c.Boolean(nullable: false),
                        MICRO_EMPRESA = c.Boolean(nullable: false),
                        PEQUENAS_EMPRESAS = c.Boolean(nullable: false),
                        EMPRESA_GRANDE_PORTE = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CD_PESSOA)
                .ForeignKey("dbo.CAD_PES_JURIDICA", t => t.CD_PESSOA)
                .Index(t => t.CD_PESSOA);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "IdentityRole_Id", "dbo.AspNetRoles");
            DropForeignKey("dbo.CAD_PORTE_EMPRESA", "CD_PESSOA", "dbo.CAD_PES_JURIDICA");
            DropForeignKey("dbo.AspNetUserRoles", "CAD_PESSOA_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "CAD_PESSOA_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "CAD_PESSOA_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CAD_PES_USUARIO", "CD_PESSOA", "dbo.AspNetUsers");
            DropForeignKey("dbo.CAD_PES_JURIDICA", "CD_PESSOA", "dbo.AspNetUsers");
            DropForeignKey("dbo.CAD_PES_FORNECEDOR", "CD_PESSOA", "dbo.AspNetUsers");
            DropForeignKey("dbo.CAD_PRODUTO_FORNECEDOR", "CD_PESSOA", "dbo.CAD_PES_FORNECEDOR");
            DropForeignKey("dbo.CAD_CIDADES_DIVULGA_FORNECEDOR", "CD_PESSOA", "dbo.CAD_PES_FORNECEDOR");
            DropForeignKey("dbo.CAD_PES_FONE", "CD_PESSOA", "dbo.AspNetUsers");
            DropForeignKey("dbo.CAD_PES_ENDERECO", "CD_PESSOA", "dbo.AspNetUsers");
            DropForeignKey("dbo.CAD_HORA_ATENDIMENTO", "CD_PES_JURIDICA", "dbo.CAD_PES_JURIDICA");
            DropForeignKey("dbo.CAD_FORMA_PAGAMENTO", "CD_PESSOA", "dbo.CAD_PES_JURIDICA");
            DropForeignKey("dbo.CAD_CATEGORIA", "CD_PES_JURIDICA", "dbo.CAD_PES_JURIDICA");
            DropForeignKey("dbo.CAD_AVALIACAO", "CD_PES_JURIDICA", "dbo.CAD_PES_JURIDICA");
            DropIndex("dbo.CAD_PORTE_EMPRESA", new[] { "CD_PESSOA" });
            DropIndex("dbo.AspNetUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "CAD_PESSOA_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "CAD_PESSOA_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "CAD_PESSOA_Id" });
            DropIndex("dbo.CAD_PES_USUARIO", new[] { "CD_PESSOA" });
            DropIndex("dbo.CAD_PRODUTO_FORNECEDOR", new[] { "CD_PESSOA" });
            DropIndex("dbo.CAD_CIDADES_DIVULGA_FORNECEDOR", new[] { "CD_PESSOA" });
            DropIndex("dbo.CAD_PES_FORNECEDOR", new[] { "CD_PESSOA" });
            DropIndex("dbo.CAD_PES_FONE", new[] { "CD_PESSOA" });
            DropIndex("dbo.CAD_PES_ENDERECO", new[] { "CD_PESSOA" });
            DropIndex("dbo.CAD_HORA_ATENDIMENTO", new[] { "CD_PES_JURIDICA" });
            DropIndex("dbo.CAD_FORMA_PAGAMENTO", new[] { "CD_PESSOA" });
            DropIndex("dbo.CAD_CATEGORIA", new[] { "CD_PES_JURIDICA" });
            DropIndex("dbo.CAD_PES_JURIDICA", new[] { "CD_PESSOA" });
            DropIndex("dbo.CAD_AVALIACAO", new[] { "CD_PES_JURIDICA" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.CAD_PORTE_EMPRESA");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.CAD_PES_USUARIO");
            DropTable("dbo.CAD_PRODUTO_FORNECEDOR");
            DropTable("dbo.CAD_CIDADES_DIVULGA_FORNECEDOR");
            DropTable("dbo.CAD_PES_FORNECEDOR");
            DropTable("dbo.CAD_PES_FONE");
            DropTable("dbo.CAD_PES_ENDERECO");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.CAD_HORA_ATENDIMENTO");
            DropTable("dbo.CAD_FORMA_PAGAMENTO");
            DropTable("dbo.CAD_CATEGORIA");
            DropTable("dbo.CAD_PES_JURIDICA");
            DropTable("dbo.CAD_AVALIACAO");
        }
    }
}

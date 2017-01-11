namespace DIVULGA_SERVICOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Principal2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CAD_ATIVIDADE", "CAD_SUB_CAT_ATIV_CD_SUB_CAT_ATIV", "dbo.CAD_SUB_CAT_ATIV");
            DropForeignKey("dbo.CAD_CATEGORIA", new[] { "CAD_SERV_JURIDICA_CD_SERVICO", "CAD_SERV_JURIDICA_CD_PES_JURIDICA" }, "dbo.CAD_SERV_JURIDICA");
            DropForeignKey("dbo.VEN_ORC_SERVICO", "VEN_ORCAMENTO_CD_ORCAMENTO", "dbo.VEN_ORCAMENTO");
            DropForeignKey("dbo.VEN_ORC_SERVICO", new[] { "CAD_SUB_CATEGORIA_CD_SUB_CATEGORIA", "CAD_SUB_CATEGORIA_CD_CATEGORIA" }, "dbo.CAD_SUB_CATEGORIA");
            DropForeignKey("dbo.CAD_IMG_SLIDE", "CD_IMAGEM", "dbo.CAD_IMAGEM");
            DropForeignKey("dbo.CAD_IMG_SLIDE", new[] { "CD_PESSOA", "SQ_SLIDESHOW" }, "dbo.CAD_SLIDESHOW");
            DropForeignKey("dbo.CAD_SERV_JURIDICA", "CD_PES_JURIDICA", "dbo.CAD_PES_JURIDICA");
            DropForeignKey("dbo.CAD_SLIDESHOW", "CD_PESSOA", "dbo.CAD_PES_JURIDICA");
            DropForeignKey("dbo.CAD_SUB_CATEGORIA", new[] { "CAD_CATEGORIA_CD_CATEGORIA", "CAD_CATEGORIA_CD_SERVICO", "CAD_CATEGORIA_CD_PES_JURIDICA" }, "dbo.CAD_CATEGORIA");
            DropForeignKey("dbo.CAD_SUB_CAT_ATIV", new[] { "CD_SUB_CATEGORIA", "CD_CATEGORIA" }, "dbo.CAD_SUB_CATEGORIA");
            DropIndex("dbo.CAD_ATIVIDADE", new[] { "CAD_SUB_CAT_ATIV_CD_SUB_CAT_ATIV" });
            DropIndex("dbo.CAD_SUB_CAT_ATIV", new[] { "CD_SUB_CATEGORIA", "CD_CATEGORIA" });
            DropIndex("dbo.CAD_SUB_CATEGORIA", new[] { "CAD_CATEGORIA_CD_CATEGORIA", "CAD_CATEGORIA_CD_SERVICO", "CAD_CATEGORIA_CD_PES_JURIDICA" });
            DropIndex("dbo.CAD_CATEGORIA", new[] { "CAD_SERV_JURIDICA_CD_SERVICO", "CAD_SERV_JURIDICA_CD_PES_JURIDICA" });
            DropIndex("dbo.CAD_SERV_JURIDICA", new[] { "CD_PES_JURIDICA" });
            DropIndex("dbo.CAD_SLIDESHOW", new[] { "CD_PESSOA" });
            DropIndex("dbo.VEN_ORC_SERVICO", new[] { "VEN_ORCAMENTO_CD_ORCAMENTO" });
            DropIndex("dbo.VEN_ORC_SERVICO", new[] { "CAD_SUB_CATEGORIA_CD_SUB_CATEGORIA", "CAD_SUB_CATEGORIA_CD_CATEGORIA" });
            DropIndex("dbo.CAD_IMG_SLIDE", new[] { "CD_IMAGEM" });
            DropIndex("dbo.CAD_IMG_SLIDE", new[] { "CD_PESSOA", "SQ_SLIDESHOW" });
            DropPrimaryKey("dbo.CAD_CATEGORIA");
            AddColumn("dbo.CAD_CATEGORIA", "SQ_CATEGORIA", c => c.Long(nullable: false, identity: true));
            AddColumn("dbo.CAD_CATEGORIA", "SHOW", c => c.Boolean(nullable: false));
            AddColumn("dbo.CAD_CATEGORIA", "DS_DESCRICAO", c => c.String(nullable: false, maxLength: 255, unicode: false));
            AlterColumn("dbo.CAD_CATEGORIA", "CD_PES_JURIDICA", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.CAD_CATEGORIA", new[] { "SQ_CATEGORIA", "CD_PES_JURIDICA" });
            CreateIndex("dbo.CAD_CATEGORIA", "CD_PES_JURIDICA");
            AddForeignKey("dbo.CAD_CATEGORIA", "CD_PES_JURIDICA", "dbo.CAD_PES_JURIDICA", "CD_PESSOA", cascadeDelete: true);
            DropColumn("dbo.CAD_CATEGORIA", "CD_CATEGORIA");
            DropColumn("dbo.CAD_CATEGORIA", "CD_SERVICO");
            DropColumn("dbo.CAD_CATEGORIA", "CAD_SERV_JURIDICA_CD_SERVICO");
            DropColumn("dbo.CAD_CATEGORIA", "CAD_SERV_JURIDICA_CD_PES_JURIDICA");
            DropTable("dbo.CAD_ATIVIDADE");
            DropTable("dbo.CAD_SUB_CAT_ATIV");
            DropTable("dbo.CAD_SUB_CATEGORIA");
            DropTable("dbo.CAD_SERV_JURIDICA");
            DropTable("dbo.CAD_SLIDESHOW");
            DropTable("dbo.BASE_DE_DADOS");
            DropTable("dbo.VEN_ORC_SERVICO");
            DropTable("dbo.CAD_IMG_SLIDE");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CAD_IMG_SLIDE",
                c => new
                    {
                        CD_IMAGEM = c.Long(nullable: false),
                        CD_PESSOA = c.String(nullable: false, maxLength: 128),
                        SQ_SLIDESHOW = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.CD_IMAGEM, t.CD_PESSOA, t.SQ_SLIDESHOW });
            
            CreateTable(
                "dbo.VEN_ORC_SERVICO",
                c => new
                    {
                        VEN_ORCAMENTO_CD_ORCAMENTO = c.Long(nullable: false),
                        CAD_SUB_CATEGORIA_CD_SUB_CATEGORIA = c.Long(nullable: false),
                        CAD_SUB_CATEGORIA_CD_CATEGORIA = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.VEN_ORCAMENTO_CD_ORCAMENTO, t.CAD_SUB_CATEGORIA_CD_SUB_CATEGORIA, t.CAD_SUB_CATEGORIA_CD_CATEGORIA });
            
            CreateTable(
                "dbo.BASE_DE_DADOS",
                c => new
                    {
                        CD_BASE_DE_DADOS = c.Long(nullable: false, identity: true),
                        NM_NOME_CATEGORIA = c.String(nullable: false, maxLength: 255, unicode: false),
                        NM_NOME_SUBCATEGORIA = c.String(nullable: false, maxLength: 255, unicode: false),
                        NM_NOME_ATIVIDADE = c.String(nullable: false, maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.CD_BASE_DE_DADOS);
            
            CreateTable(
                "dbo.CAD_SLIDESHOW",
                c => new
                    {
                        CD_PESSOA = c.String(nullable: false, maxLength: 128),
                        SQ_SLIDESHOW = c.Long(nullable: false, identity: true),
                        IMAGEM = c.Binary(),
                    })
                .PrimaryKey(t => new { t.CD_PESSOA, t.SQ_SLIDESHOW });
            
            CreateTable(
                "dbo.CAD_SERV_JURIDICA",
                c => new
                    {
                        CD_SERVICO = c.Long(nullable: false, identity: true),
                        CD_PES_JURIDICA = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.CD_SERVICO, t.CD_PES_JURIDICA });
            
            CreateTable(
                "dbo.CAD_SUB_CATEGORIA",
                c => new
                    {
                        CD_SUB_CATEGORIA = c.Long(nullable: false),
                        CD_CATEGORIA = c.Long(nullable: false),
                        NM_NOME = c.String(nullable: false, unicode: false, storeType: "text"),
                        CAD_CATEGORIA_CD_CATEGORIA = c.Long(nullable: false),
                        CAD_CATEGORIA_CD_SERVICO = c.Long(nullable: false),
                        CAD_CATEGORIA_CD_PES_JURIDICA = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.CD_SUB_CATEGORIA, t.CD_CATEGORIA });
            
            CreateTable(
                "dbo.CAD_SUB_CAT_ATIV",
                c => new
                    {
                        CD_SUB_CAT_ATIV = c.Long(nullable: false),
                        CD_SUB_CATEGORIA = c.Long(nullable: false),
                        CD_CATEGORIA = c.Long(nullable: false),
                        CD_ATIVIDADE = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.CD_SUB_CAT_ATIV);
            
            CreateTable(
                "dbo.CAD_ATIVIDADE",
                c => new
                    {
                        CD_ATIVIDADE = c.Long(nullable: false, identity: true),
                        NM_NOME = c.String(nullable: false, unicode: false, storeType: "text"),
                        CAD_SUB_CAT_ATIV_CD_SUB_CAT_ATIV = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.CD_ATIVIDADE);
            
            AddColumn("dbo.CAD_CATEGORIA", "CAD_SERV_JURIDICA_CD_PES_JURIDICA", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.CAD_CATEGORIA", "CAD_SERV_JURIDICA_CD_SERVICO", c => c.Long(nullable: false));
            AddColumn("dbo.CAD_CATEGORIA", "CD_SERVICO", c => c.Long(nullable: false));
            AddColumn("dbo.CAD_CATEGORIA", "CD_CATEGORIA", c => c.Long(nullable: false));
            DropForeignKey("dbo.CAD_CATEGORIA", "CD_PES_JURIDICA", "dbo.CAD_PES_JURIDICA");
            DropIndex("dbo.CAD_CATEGORIA", new[] { "CD_PES_JURIDICA" });
            DropPrimaryKey("dbo.CAD_CATEGORIA");
            AlterColumn("dbo.CAD_CATEGORIA", "CD_PES_JURIDICA", c => c.Long(nullable: false));
            DropColumn("dbo.CAD_CATEGORIA", "DS_DESCRICAO");
            DropColumn("dbo.CAD_CATEGORIA", "SHOW");
            DropColumn("dbo.CAD_CATEGORIA", "SQ_CATEGORIA");
            AddPrimaryKey("dbo.CAD_CATEGORIA", new[] { "CD_CATEGORIA", "CD_SERVICO", "CD_PES_JURIDICA" });
            CreateIndex("dbo.CAD_IMG_SLIDE", new[] { "CD_PESSOA", "SQ_SLIDESHOW" });
            CreateIndex("dbo.CAD_IMG_SLIDE", "CD_IMAGEM");
            CreateIndex("dbo.VEN_ORC_SERVICO", new[] { "CAD_SUB_CATEGORIA_CD_SUB_CATEGORIA", "CAD_SUB_CATEGORIA_CD_CATEGORIA" });
            CreateIndex("dbo.VEN_ORC_SERVICO", "VEN_ORCAMENTO_CD_ORCAMENTO");
            CreateIndex("dbo.CAD_SLIDESHOW", "CD_PESSOA");
            CreateIndex("dbo.CAD_SERV_JURIDICA", "CD_PES_JURIDICA");
            CreateIndex("dbo.CAD_CATEGORIA", new[] { "CAD_SERV_JURIDICA_CD_SERVICO", "CAD_SERV_JURIDICA_CD_PES_JURIDICA" });
            CreateIndex("dbo.CAD_SUB_CATEGORIA", new[] { "CAD_CATEGORIA_CD_CATEGORIA", "CAD_CATEGORIA_CD_SERVICO", "CAD_CATEGORIA_CD_PES_JURIDICA" });
            CreateIndex("dbo.CAD_SUB_CAT_ATIV", new[] { "CD_SUB_CATEGORIA", "CD_CATEGORIA" });
            CreateIndex("dbo.CAD_ATIVIDADE", "CAD_SUB_CAT_ATIV_CD_SUB_CAT_ATIV");
            AddForeignKey("dbo.CAD_SUB_CAT_ATIV", new[] { "CD_SUB_CATEGORIA", "CD_CATEGORIA" }, "dbo.CAD_SUB_CATEGORIA", new[] { "CD_SUB_CATEGORIA", "CD_CATEGORIA" });
            AddForeignKey("dbo.CAD_SUB_CATEGORIA", new[] { "CAD_CATEGORIA_CD_CATEGORIA", "CAD_CATEGORIA_CD_SERVICO", "CAD_CATEGORIA_CD_PES_JURIDICA" }, "dbo.CAD_CATEGORIA", new[] { "CD_CATEGORIA", "CD_SERVICO", "CD_PES_JURIDICA" });
            AddForeignKey("dbo.CAD_SLIDESHOW", "CD_PESSOA", "dbo.CAD_PES_JURIDICA", "CD_PESSOA");
            AddForeignKey("dbo.CAD_SERV_JURIDICA", "CD_PES_JURIDICA", "dbo.CAD_PES_JURIDICA", "CD_PESSOA");
            AddForeignKey("dbo.CAD_IMG_SLIDE", new[] { "CD_PESSOA", "SQ_SLIDESHOW" }, "dbo.CAD_SLIDESHOW", new[] { "CD_PESSOA", "SQ_SLIDESHOW" }, cascadeDelete: true);
            AddForeignKey("dbo.CAD_IMG_SLIDE", "CD_IMAGEM", "dbo.CAD_IMAGEM", "CD_IMAGEM", cascadeDelete: true);
            AddForeignKey("dbo.VEN_ORC_SERVICO", new[] { "CAD_SUB_CATEGORIA_CD_SUB_CATEGORIA", "CAD_SUB_CATEGORIA_CD_CATEGORIA" }, "dbo.CAD_SUB_CATEGORIA", new[] { "CD_SUB_CATEGORIA", "CD_CATEGORIA" }, cascadeDelete: true);
            AddForeignKey("dbo.VEN_ORC_SERVICO", "VEN_ORCAMENTO_CD_ORCAMENTO", "dbo.VEN_ORCAMENTO", "CD_ORCAMENTO", cascadeDelete: true);
            AddForeignKey("dbo.CAD_CATEGORIA", new[] { "CAD_SERV_JURIDICA_CD_SERVICO", "CAD_SERV_JURIDICA_CD_PES_JURIDICA" }, "dbo.CAD_SERV_JURIDICA", new[] { "CD_SERVICO", "CD_PES_JURIDICA" });
            AddForeignKey("dbo.CAD_ATIVIDADE", "CAD_SUB_CAT_ATIV_CD_SUB_CAT_ATIV", "dbo.CAD_SUB_CAT_ATIV", "CD_SUB_CAT_ATIV");
        }
    }
}

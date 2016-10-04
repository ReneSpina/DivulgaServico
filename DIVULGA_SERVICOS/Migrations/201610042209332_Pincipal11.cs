namespace DIVULGA_SERVICOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pincipal11 : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BASE_DE_DADOS");
        }
    }
}

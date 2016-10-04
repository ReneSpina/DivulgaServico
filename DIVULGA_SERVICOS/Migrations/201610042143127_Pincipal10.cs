namespace DIVULGA_SERVICOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pincipal10 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CAD_ATIVIDADE", "DS_DESCRICAO");
            DropColumn("dbo.CAD_SERV_JURIDICA", "NM_NOME");
            DropColumn("dbo.CAD_SERV_JURIDICA", "DS_DESCRICAO");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CAD_SERV_JURIDICA", "DS_DESCRICAO", c => c.String(maxLength: 500));
            AddColumn("dbo.CAD_SERV_JURIDICA", "NM_NOME", c => c.String(maxLength: 255));
            AddColumn("dbo.CAD_ATIVIDADE", "DS_DESCRICAO", c => c.String(nullable: false, unicode: false, storeType: "text"));
        }
    }
}

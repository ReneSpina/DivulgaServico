namespace DIVULGA_SERVICOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Principal2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAD_PES_JURIDICA", "ATIVO", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "NEWSLETTER", c => c.Boolean(nullable: false));
            DropColumn("dbo.AspNetUsers", "ATIVADO");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ATIVADO", c => c.Boolean(nullable: false));
            DropColumn("dbo.AspNetUsers", "NEWSLETTER");
            DropColumn("dbo.CAD_PES_JURIDICA", "ATIVO");
        }
    }
}

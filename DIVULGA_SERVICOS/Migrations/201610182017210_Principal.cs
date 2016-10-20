namespace DIVULGA_SERVICOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Principal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAD_CATEGORIA", "SHOW", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CAD_CATEGORIA", "SHOW");
        }
    }
}

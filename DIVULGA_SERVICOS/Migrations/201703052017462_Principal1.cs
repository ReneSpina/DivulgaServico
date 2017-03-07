namespace DIVULGA_SERVICOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Principal1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAD_PES_JURIDICA", "ACEITE_CONTRATO", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CAD_PES_JURIDICA", "ACEITE_CONTRATO");
        }
    }
}

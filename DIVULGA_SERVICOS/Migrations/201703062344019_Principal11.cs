namespace DIVULGA_SERVICOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Principal11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAD_PES_FORNECEDOR", "ACEITE_CONTRATO", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CAD_PES_FORNECEDOR", "ACEITE_CONTRATO");
        }
    }
}

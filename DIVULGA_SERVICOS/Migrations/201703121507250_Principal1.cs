namespace DIVULGA_SERVICOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Principal1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CAD_PES_FORNECEDOR", "CD_INDICACAO");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CAD_PES_FORNECEDOR", "CD_INDICACAO", c => c.Int(nullable: false));
        }
    }
}

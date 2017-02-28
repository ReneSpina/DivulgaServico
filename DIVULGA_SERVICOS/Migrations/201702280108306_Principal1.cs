namespace DIVULGA_SERVICOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Principal1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CAD_CIDADES_DIVULGA_FORNECEDOR", "NM_CIDADE", c => c.String(maxLength: 255));
            AlterColumn("dbo.CAD_CIDADES_DIVULGA_FORNECEDOR", "NM_ESTADO", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CAD_CIDADES_DIVULGA_FORNECEDOR", "NM_ESTADO", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.CAD_CIDADES_DIVULGA_FORNECEDOR", "NM_CIDADE", c => c.String(nullable: false, maxLength: 255));
        }
    }
}

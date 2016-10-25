namespace DIVULGA_SERVICOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Principal1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAD_PES_ENDERECO", "NM_ESTADO", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CAD_PES_ENDERECO", "NM_ESTADO");
        }
    }
}

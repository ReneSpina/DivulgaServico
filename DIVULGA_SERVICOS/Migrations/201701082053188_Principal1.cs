namespace DIVULGA_SERVICOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Spatial;
    
    public partial class Principal1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAD_PES_ENDERECO", "localizacao", c => c.Geography());
            DropColumn("dbo.CAD_PES_ENDERECO", "CD_LAT");
            DropColumn("dbo.CAD_PES_ENDERECO", "CD_LONG");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CAD_PES_ENDERECO", "CD_LONG", c => c.String(maxLength: 100));
            AddColumn("dbo.CAD_PES_ENDERECO", "CD_LAT", c => c.String(maxLength: 100));
            DropColumn("dbo.CAD_PES_ENDERECO", "localizacao");
        }
    }
}

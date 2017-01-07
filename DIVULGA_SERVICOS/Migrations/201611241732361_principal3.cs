namespace DIVULGA_SERVICOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class principal3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CAD_PES_ENDERECO", "CD_LAT", c => c.String(maxLength: 100));
            AlterColumn("dbo.CAD_PES_ENDERECO", "CD_LONG", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CAD_PES_ENDERECO", "CD_LONG", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.CAD_PES_ENDERECO", "CD_LAT", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}

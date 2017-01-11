namespace DIVULGA_SERVICOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Principal2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAD_PES_ENDERECO", "NM_ESTADO", c => c.String(maxLength: 255));
            AddColumn("dbo.CAD_PES_ENDERECO", "NUMERO", c => c.Int(nullable: false));
            AddColumn("dbo.CAD_PES_ENDERECO", "CD_LAT", c => c.String(maxLength: 100));
            AddColumn("dbo.CAD_PES_ENDERECO", "CD_LONG", c => c.String(maxLength: 100));
            DropColumn("dbo.CAD_PES_ENDERECO", "TP_TIPO_LOGRADOURO");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CAD_PES_ENDERECO", "TP_TIPO_LOGRADOURO", c => c.String(maxLength: 20, unicode: false));
            DropColumn("dbo.CAD_PES_ENDERECO", "CD_LONG");
            DropColumn("dbo.CAD_PES_ENDERECO", "CD_LAT");
            DropColumn("dbo.CAD_PES_ENDERECO", "NUMERO");
            DropColumn("dbo.CAD_PES_ENDERECO", "NM_ESTADO");
        }
    }
}

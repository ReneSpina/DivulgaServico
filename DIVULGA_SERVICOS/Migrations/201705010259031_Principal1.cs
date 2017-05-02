namespace DIVULGA_SERVICOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Principal1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAD_PES_JURIDICA", "DS_O_QUE_FAZEMOS", c => c.String(unicode: false, storeType: "text"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CAD_PES_JURIDICA", "DS_O_QUE_FAZEMOS");
        }
    }
}

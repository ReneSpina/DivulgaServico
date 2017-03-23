namespace DIVULGA_SERVICOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Principal1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAD_AVALIACAO", "NM_NOME_AVALIADOR", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CAD_AVALIACAO", "NM_NOME_AVALIADOR");
        }
    }
}

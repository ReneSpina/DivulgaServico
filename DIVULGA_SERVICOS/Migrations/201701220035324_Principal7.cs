namespace DIVULGA_SERVICOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Principal7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAD_PES_JURIDICA", "TODO_DIA", c => c.Boolean(nullable: false));
            DropColumn("dbo.CAD_HORA_ATENDIMENTO", "TODO_DIA");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CAD_HORA_ATENDIMENTO", "TODO_DIA", c => c.Boolean(nullable: false));
            DropColumn("dbo.CAD_PES_JURIDICA", "TODO_DIA");
        }
    }
}

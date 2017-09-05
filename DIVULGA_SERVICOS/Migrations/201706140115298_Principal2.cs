namespace DIVULGA_SERVICOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Principal2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CAD_CATEGORIA", "DS_DESCRICAO", c => c.String(maxLength: 2000, unicode: false));
            AlterColumn("dbo.CAD_HORA_ATENDIMENTO", "HORA_INICIO", c => c.Int(nullable: true));
            AlterColumn("dbo.CAD_HORA_ATENDIMENTO", "HORA_FIM", c => c.Int(nullable: true));
        }

        public override void Down()
        {
            AlterColumn("dbo.CAD_CATEGORIA", "DS_DESCRICAO", c => c.String(nullable: false, maxLength: 2000, unicode: false));
        }
    }
}

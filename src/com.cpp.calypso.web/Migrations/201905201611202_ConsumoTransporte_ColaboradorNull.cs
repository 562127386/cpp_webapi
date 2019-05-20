namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConsumoTransporte_ColaboradorNull : DbMigration
    {
        public override void Up()
        {
            DropIndex("SCH_TRANSPORTES.consumos_transporte", new[] { "colaborador_id" });
            AlterColumn("SCH_TRANSPORTES.consumos_transporte", "colaborador_id", c => c.Int());
            CreateIndex("SCH_TRANSPORTES.consumos_transporte", "colaborador_id");
        }
        
        public override void Down()
        {
            DropIndex("SCH_TRANSPORTES.consumos_transporte", new[] { "colaborador_id" });
            AlterColumn("SCH_TRANSPORTES.consumos_transporte", "colaborador_id", c => c.Int(nullable: false));
            CreateIndex("SCH_TRANSPORTES.consumos_transporte", "colaborador_id");
        }
    }
}

namespace com.cpp.calypso.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConsumosSinReserva_ColaboradorNull : DbMigration
    {
        public override void Up()
        {
            DropIndex("SCH_SERVICIOS.consumos_sin_reserva_hospedaje", new[] { "colaborador_id" });
            AlterColumn("SCH_SERVICIOS.consumos_sin_reserva_hospedaje", "colaborador_id", c => c.Int());
            AlterColumn("SCH_SERVICIOS.consumos_sin_reserva_hospedaje", "autorizacion", c => c.Int());
            CreateIndex("SCH_SERVICIOS.consumos_sin_reserva_hospedaje", "colaborador_id");
        }
        
        public override void Down()
        {
            DropIndex("SCH_SERVICIOS.consumos_sin_reserva_hospedaje", new[] { "colaborador_id" });
            AlterColumn("SCH_SERVICIOS.consumos_sin_reserva_hospedaje", "autorizacion", c => c.Int(nullable: false));
            AlterColumn("SCH_SERVICIOS.consumos_sin_reserva_hospedaje", "colaborador_id", c => c.Int(nullable: false));
            CreateIndex("SCH_SERVICIOS.consumos_sin_reserva_hospedaje", "colaborador_id");
        }
    }
}

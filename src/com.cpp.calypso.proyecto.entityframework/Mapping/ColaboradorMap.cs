using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class ColaboradorMap : EntityTypeConfiguration<Colaborador>
    {
        public ColaboradorMap()
        {
            ToTable("colaboradores", "SCH_RRHH");

            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.Id).HasColumnName("id");
            Property(d => d.CandidatoIdSap).HasColumnName("candidato_id_sap");
            Property(d => d.CatalogoAptoMedicoId).HasColumnName("catalogo_apto_medico_id");
            Property(d => d.CatalogoAreaId).HasColumnName("catalogo_area_id");
            Property(d => d.CatalogoAsociacionId).HasColumnName("catalogo_asociacion_id");
            Property(d => d.CatalogoBancoId).HasColumnName("catalogo_banco_id");
            Property(d => d.CatalogoCargoId).HasColumnName("catalogo_cargo_id");
            Property(d => d.CatalogoClaseContratoId).HasColumnName("catalogo_clase_contrato_id");
            Property(d => d.CatalogoClaseId).HasColumnName("catalogo_clase_id");
            Property(d => d.CatalogoCoberturaDependienteId).HasColumnName("catalogo_cobertura_dependiente_id");
            Property(d => d.CatalogoCodigoIncapacidadId).HasColumnName("catalogo_codigo_incapacidad_id");
            Property(d => d.CatalogoCodigoSiniestroId).HasColumnName("catalogo_codigo_siniestro_id");
            Property(d => d.CatalogoDestinoEstanciaId).HasColumnName("catalogo_destino_estancia_id");
            Property(d => d.CatalogoDivisionPersonalId).HasColumnName("catalogo_division_personal_id");
            Property(d => d.CatalogoEncargadoPersonalId).HasColumnName("catalogo_encargado_personal_id");
            Property(d => d.CatalogoEncuadreId).HasColumnName("catalogo_encuadre_id");
            Property(d => d.CatalogoEstadoCivilId).HasColumnName("catalogo_estado_civil_id");
            Property(d => d.CatalogoEtniaId).HasColumnName("catalogo_etnia_id");
            Property(d => d.CatalogoFormacionEducativaId).HasColumnName("catalogo_formacion_educativa_id");
            Property(d => d.CatalogoFormaPagoId).HasColumnName("catalogo_forma_pago_id");
            Property(d => d.CatalogoFuncionId).HasColumnName("catalogo_funcion_id");
            Property(d => d.CatalogoGeneroId).HasColumnName("catalogo_genero_id");
            Property(d => d.CatalogoGrupoId).HasColumnName("catalogo_grupo_id");
            Property(d => d.CatalogoPeriodoNominaId).HasColumnName("catalogo_periodo_nomina_id");
            Property(d => d.CatalogoPlanBeneficiosId).HasColumnName("catalogo_plan_beneficios_id");
            Property(d => d.CatalogoPlanesBeneficiosId).HasColumnName("catalogo_planes_beneficios_id");
            Property(d => d.CatalogoPlanSaludId).HasColumnName("catalogo_plan_salud_id");
            Property(d => d.CatalogoSectorId).HasColumnName("catalogo_sector_id");
            Property(d => d.CatalogoSitioTrabajoId).HasColumnName("catalogo_sitio_trabajo_id");
            Property(d => d.CatalogoSubdivisionPersonalId).HasColumnName("catalogo_subdivision_personal_id");
            Property(d => d.CatalogoSubgrupoId).HasColumnName("catalogo_subgrupo_id");
            Property(d => d.CatalogoTipoContratoId).HasColumnName("catalogo_tipo_contrato_id");
            Property(d => d.CatalogoTipoCuentaId).HasColumnName("catalogo_tipo_cuenta_id");
            Property(d => d.CatalogoTipoIdentificacionId).HasColumnName("catalogo_tipo_identificacion_id");
            Property(d => d.CatalogoTipoNominaId).HasColumnName("catalogo_tipo_nomina_id");
            Property(d => d.CatalogoViaPagoId).HasColumnName("catalogo_via_pago_id");
            Property(d => d.CatalogoVinculoLaboralId).HasColumnName("catalogo_vinculo_laboral_id");
            Property(d => d.CatalogoGrupoPersonalId).HasColumnName("catalogo_grupo_personal_id");
            Property(d => d.ContratoId).HasColumnName("contrato_id");
            Property(d => d.EjecutorObra).HasColumnName("ejecutor_obra");
            Property(d => d.EmpleadoIdSap).HasColumnName("empleado_id_sap");
            Property(d => d.EmpresaId).HasColumnName("empresa_id");
            Property(d => d.Estado).HasColumnName("estado");
            Property(d => d.FechaAlta).HasColumnName("fecha_alta");
            Property(d => d.FechaCaducidadContrato).HasColumnName("fecha_caducidad_contrato");
            Property(d => d.FechaIngreso).HasColumnName("fecha_ingreso");
            Property(d => d.FechaMatrimonio).HasColumnName("fecha_matrimonio");
            Property(d => d.FechaNacimiento).HasColumnName("fecha_nacimiento");
            Property(d => d.HorarioDesde).HasColumnName("horario_desde");
            Property(d => d.HorarioHasta).HasColumnName("horario_hasta");
            Property(d => d.LocacionTrabajoId).HasColumnName("locacion_trabajo_id");
            Property(d => d.Meta4).HasColumnName("meta4");
            Property(d => d.Nombres).HasColumnName("nombres");
            Property(d => d.NombresApellidos).HasColumnName("nombres_apellidos");
            Property(d => d.NumeroCuenta).HasColumnName("numero_cuenta");
            Property(d => d.NumeroHijos).HasColumnName("numero_hijos");
            Property(d => d.NumeroIdentificacion).HasColumnName("numero_identificacion");
            Property(d => d.NumeroLegajoDefinitivo).HasColumnName("numero_legajo_definitivo");
            Property(d => d.NumeroLegajoTemporal).HasColumnName("numero_legajo_temporal");
            //Renombrar 
            Property(d => d.PaisNacimientoId).HasColumnName("pais_pais_nacimiento_id");

            Property(d => d.Posicion).HasColumnName("posicion");
            Property(d => d.PrimerApellido).HasColumnName("primer_apellido");
            Property(d => d.RegistroMasivo).HasColumnName("registro_masivo");
            Property(d => d.RemuneracionMensual).HasColumnName("remuneracion_mensual");
            Property(d => d.SegundoApellido).HasColumnName("segundo_apellido");
            Property(d => d.TurnoId).HasColumnName("turno_id");
            Property(d => d.UsuarioId).HasColumnName("usuario_id");
            Property(d => d.ValidacionCedula).HasColumnName("validacion_cedula");
            Property(d => d.HuellaDigital).HasColumnName("huella_digital");
            Property(d => d.TieneAusentismo).HasColumnName("tiene_ausentismo");

            Property(d => d.TieneDerechoAlimentacion).HasColumnName("tiene_derecho_alimentacion");
            Property(d => d.TieneDerechoHospedaje).HasColumnName("tiene_derecho_hospedaje");
            Property(d => d.TieneDerechoTransporte).HasColumnName("tiene_derecho_transporte");
            Property(d => d.TieneAcceso).HasColumnName("tiene_acceso");
            Property(d => d.TipoComidaPermitidos).HasColumnName("tipos_comida_permitidos");
            Property(d => d.ObservacionesAcceso).HasColumnName("observaciones_acceso");
            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");


            this.HasRequired(t => t.CatalogoTipoIdentificacion)
                .WithMany()
                .HasForeignKey(d => d.CatalogoTipoIdentificacionId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoGenero)
                .WithMany()
                .HasForeignKey(d => d.CatalogoGeneroId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoEtnia)
                .WithMany()
                .HasForeignKey(d => d.CatalogoEtniaId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoEstadoCivil)
                .WithMany()
                .HasForeignKey(d => d.CatalogoEstadoCivilId)
                .WillCascadeOnDelete(false);

            this.HasOptional(t => t.CatalogoCodigoSiniestro)
                .WithMany()
                .HasForeignKey(d => d.CatalogoCodigoSiniestroId)
                .WillCascadeOnDelete(false);

            this.HasOptional(t => t.CatalogoFormacionEducativa)
                .WithMany()
                .HasForeignKey(d => d.CatalogoFormacionEducativaId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoGrupoPersonal)
                .WithMany()
                .HasForeignKey(d => d.CatalogoGrupoPersonalId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoDestinoEstancia)
                .WithMany()
                .HasForeignKey(d => d.CatalogoDestinoEstanciaId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoSitioTrabajo)
                .WithMany()
                .HasForeignKey(d => d.CatalogoSitioTrabajoId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoArea)
                .WithMany()
                .HasForeignKey(d => d.CatalogoAreaId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoCargo)
                .WithMany()
                .HasForeignKey(d => d.CatalogoCargoId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoVinculoLaboral)
                .WithMany()
                .HasForeignKey(d => d.CatalogoVinculoLaboralId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoClase)
                .WithMany()
                .HasForeignKey(d => d.CatalogoClaseId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoPlanBeneficios)
                .WithMany()
                .HasForeignKey(d => d.CatalogoPlanBeneficiosId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoPlanSalud)
                .WithMany()
                .HasForeignKey(d => d.CatalogoPlanSaludId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoCoberturaDependiente)
                .WithMany()
                .HasForeignKey(d => d.CatalogoCoberturaDependienteId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoPlanesBeneficios)
               .WithMany()
               .HasForeignKey(d => d.CatalogoPlanesBeneficiosId)
               .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoAsociacion)
               .WithMany()
               .HasForeignKey(d => d.CatalogoAsociacionId)
               .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoAptoMedico)
               .WithMany()
               .HasForeignKey(d => d.CatalogoAptoMedicoId)
               .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoDivisionPersonal)
               .WithMany()
               .HasForeignKey(d => d.CatalogoDivisionPersonalId)
               .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoSubdivisionPersonal)
               .WithMany()
               .HasForeignKey(d => d.CatalogoSubdivisionPersonalId)
               .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoTipoContrato)
               .WithMany()
               .HasForeignKey(d => d.CatalogoTipoContratoId)
               .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoClaseContrato)
               .WithMany()
               .HasForeignKey(d => d.CatalogoClaseContratoId)
               .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoFuncion)
               .WithMany()
               .HasForeignKey(d => d.CatalogoFuncionId)
               .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoTipoNomina)
               .WithMany()
               .HasForeignKey(d => d.CatalogoTipoNominaId)
               .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoPeriodoNomina)
               .WithMany()
               .HasForeignKey(d => d.CatalogoPeriodoNominaId)
               .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoFormaPago)
               .WithMany()
               .HasForeignKey(d => d.CatalogoFormaPagoId)
               .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoGrupo)
               .WithMany()
               .HasForeignKey(d => d.CatalogoGrupoId)
               .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoSubgrupo)
               .WithMany()
               .HasForeignKey(d => d.CatalogoSubgrupoId)
               .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoBanco)
               .WithMany()
               .HasForeignKey(d => d.CatalogoBancoId)
               .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoTipoCuenta)
               .WithMany()
               .HasForeignKey(d => d.CatalogoTipoCuentaId)
               .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoTipoCuenta)
               .WithMany()
               .HasForeignKey(d => d.CatalogoTipoCuentaId)
               .WillCascadeOnDelete(false);

            this.HasRequired(t => t.Empresa)
               .WithMany()
               .HasForeignKey(d => d.EmpresaId)
               .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoEncuadre)
               .WithMany()
               .HasForeignKey(d => d.CatalogoEncuadreId)
               .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoEncargadoPersonal)
               .WithMany()
               .HasForeignKey(d => d.CatalogoEncargadoPersonalId)
               .WillCascadeOnDelete(false);

            this.HasRequired(t => t.CatalogoCodigoIncapacidad)
               .WithMany()
               .HasForeignKey(d => d.CatalogoCodigoIncapacidadId)
               .WillCascadeOnDelete(false);

            this.HasOptional(t => t.CatalogoSector)
               .WithMany()
               .HasForeignKey(d => d.CatalogoSectorId)
               .WillCascadeOnDelete(false);

            this.HasOptional(t => t.CatalogoViaPago)
               .WithMany()
               .HasForeignKey(d => d.CatalogoViaPagoId)
               .WillCascadeOnDelete(false);

        }
    }
}

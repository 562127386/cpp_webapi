using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class Colaborador : Entity, ISoftDelete
    {
        public int CatalogoTipoIdentificacionId { get; set; }
        public Catalogo CatalogoTipoIdentificacion { get; set; }

        public int? CatalogoGeneroId { get; set; }

        public Catalogo CatalogoGenero { get; set; }

        public int? CatalogoEtniaId { get; set; }

        public Catalogo CatalogoEtnia { get; set; }

        public int? CatalogoEstadoCivilId { get; set; }

        public Catalogo CatalogoEstadoCivil { get; set; }

        public int? CatalogoCodigoSiniestroId { get; set; }

        public Catalogo CatalogoCodigoSiniestro { get; set; }

        public int? CatalogoFormacionEducativaId { get; set; }

        public Catalogo CatalogoFormacionEducativa { get; set; }

        public int? CatalogoGrupoPersonalId { get; set; }

        public Catalogo CatalogoGrupoPersonal { get; set; }

        public int? CatalogoDestinoEstanciaId { get; set; }

        public Catalogo CatalogoDestinoEstancia { get; set; }

        public int? CatalogoSitioTrabajoId { get; set; }

        public Catalogo CatalogoSitioTrabajo { get; set; }

        public int? CatalogoAreaId { get; set; }

        public Catalogo CatalogoArea { get; set; }

        public int? CatalogoCargoId { get; set; }

        public Catalogo CatalogoCargo { get; set; }

        public int? CatalogoVinculoLaboralId { get; set; }

        public Catalogo CatalogoVinculoLaboral { get; set; }

        public int? CatalogoClaseId { get; set; }

        public Catalogo CatalogoClase { get; set; }

        public int? CatalogoPlanBeneficiosId { get; set; }

        public Catalogo CatalogoPlanBeneficios { get; set; }

        public int? CatalogoPlanSaludId { get; set; }

        public Catalogo CatalogoPlanSalud { get; set; }

        public int? CatalogoCoberturaDependienteId { get; set; }

        public Catalogo CatalogoCoberturaDependiente { get; set; }

        public int? CatalogoPlanesBeneficiosId { get; set; }

        public Catalogo CatalogoPlanesBeneficios { get; set; }

        public int? CatalogoAsociacionId { get; set; }

        public Catalogo CatalogoAsociacion { get; set; }

        public int? CatalogoAptoMedicoId { get; set; }

        public Catalogo CatalogoAptoMedico { get; set; }

        public int? CatalogoDivisionPersonalId { get; set; }

        public Catalogo CatalogoDivisionPersonal { get; set; }

        public int? CatalogoSubdivisionPersonalId { get; set; }

        public Catalogo CatalogoSubdivisionPersonal { get; set; }

        public int? CatalogoTipoContratoId { get; set; }

        public Catalogo CatalogoTipoContrato { get; set; }

        public int? CatalogoClaseContratoId { get; set; }

        public Catalogo CatalogoClaseContrato { get; set; }

        public int? CatalogoFuncionId { get; set; }

        public Catalogo CatalogoFuncion { get; set; }

        public int? CatalogoTipoNominaId { get; set; }

        public Catalogo CatalogoTipoNomina { get; set; }

        public int? CatalogoPeriodoNominaId { get; set; }

        public Catalogo CatalogoPeriodoNomina { get; set; }

        public int? CatalogoFormaPagoId { get; set; }

        public Catalogo CatalogoFormaPago { get; set; }

        public int? CatalogoGrupoId { get; set; }

        public Catalogo CatalogoGrupo { get; set; }

        public int? CatalogoSubgrupoId { get; set; }

        public Catalogo CatalogoSubgrupo { get; set; }

        public int? CatalogoBancoId { get; set; }

        public Catalogo CatalogoBanco { get; set; }

        public int? CatalogoTipoCuentaId { get; set; }

        public Catalogo CatalogoTipoCuenta { get; set; }

        public int EmpresaId { get; set; }

        public Empresa Empresa { get; set; }

        public int? PaisNacimientoId { get; set; }

        public int? LocacionTrabajoId { get; set; }

        public Locacion LocacionTrabajo { get; set; }

        public int? UsuarioId { get; set; }

        public int? ContratoId { get; set; }

        public int? TurnoId { get; set; }

        public TimeSpan? HorarioDesde { get; set; }

        public TimeSpan? HorarioHasta { get; set; }

        public int? EmpleadoIdSap { get; set; }

        public int? CandidatoIdSap { get; set; }

        public string NumeroIdentificacion { get; set; }

        public string Nombres { get; set; }

        public DateTime? FechaIngreso { get; set; }

        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public DateTime? FechaMatrimonio { get; set; }

        public int? Meta4 { get; set; }

        public string Posicion { get; set; }

        public DateTime? FechaCaducidadContrato { get; set; }

        public bool? EjecutorObra { get; set; }

        public decimal? RemuneracionMensual { get; set; }

        public string NumeroCuenta { get; set; }

        public string NumeroLegajoTemporal { get; set; }

        public string NumeroLegajoDefinitivo { get; set; }

        public int? NumeroHijos { get; set; }

        public string Estado { get; set; }

        public bool TieneAusentismo { get; set; }

        public bool? RegistroMasivo { get; set; } = false;

        public int? CatalogoEncuadreId { get; set; }

        public Catalogo CatalogoEncuadre { get; set; }

        public int? CatalogoEncargadoPersonalId { get; set; }

        public Catalogo CatalogoEncargadoPersonal { get; set; }

        public bool ValidacionCedula { get; set; } = false;

        public string NombresApellidos { get; set; }

        public int? CatalogoCodigoIncapacidadId { get; set; }

        public Catalogo CatalogoCodigoIncapacidad { get; set; }

        public int? CatalogoSectorId { get; set; }

        public Catalogo CatalogoSector { get; set; }

        public int? CatalogoViaPagoId { get; set; }

        public Catalogo CatalogoViaPago { get; set; }

        public DateTime? FechaAlta { get; set; }

        public string HuellaDigital { get; set; }

        public bool TieneDerechoAlimentacion { get; set; }

        public bool TieneDerechoHospedaje { get; set; }

        public bool TieneDerechoTransporte { get; set; }

        public bool TieneAcceso { get; set; }

        public string ObservacionesAcceso { get; set; }

        public string TipoComidaPermitidos { get; set; }

        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }

        public string uid { get; set; }
    }
}

using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.proyecto.dominio.Entidades;
using com.cpp.calypso.comun.aplicacion;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(Colaborador))]
    [Serializable]
    public class ColaboradorDto : EntityDto
    {
        public int CatalogoTipoIdentificacionId { get; set; }
        public CatalogoDto CatalogoTipoIdentificacion { get; set; }

        public int CatalogoGeneroId { get; set; }

        public CatalogoDto CatalogoGenero { get; set; }

        public int CatalogoEtniaId { get; set; }

        public CatalogoDto CatalogoEtnia { get; set; }

        public int CatalogoEstadoCivilId { get; set; }

        public CatalogoDto CatalogoEstadoCivil { get; set; }

        public int? CatalogoCodigoSiniestroId { get; set; }

        public CatalogoDto CatalogoCodigoSiniestro { get; set; }

        public int? CatalogoFormacionEducativaId { get; set; }

        public CatalogoDto CatalogoFormacionEducativa { get; set; }

        public int CatalogoGrupoPersonalId { get; set; }

        public CatalogoDto CatalogoGrupoPersonal { get; set; }

        public int CatalogoDestinoEstanciaId { get; set; }

        public CatalogoDto CatalogoDestinoEstancia { get; set; }

        public int CatalogoSitioTrabajoId { get; set; }

        public CatalogoDto CatalogoSitioTrabajo { get; set; }

        public int CatalogoAreaId { get; set; }

        public CatalogoDto CatalogoArea { get; set; }

        public int CatalogoCargoId { get; set; }

        public CatalogoDto CatalogoCargo { get; set; }

        public int CatalogoVinculoLaboralId { get; set; }

        public CatalogoDto CatalogoVinculoLaboral { get; set; }

        public int CatalogoClaseId { get; set; }

        public CatalogoDto CatalogoClase { get; set; }

        public int CatalogoPlanBeneficiosId { get; set; }

        public CatalogoDto CatalogoPlanBeneficios { get; set; }

        public int CatalogoPlanSaludId { get; set; }

        public CatalogoDto CatalogoPlanSalud { get; set; }

        public int CatalogoCoberturaDependienteId { get; set; }

        public CatalogoDto CatalogoCoberturaDependiente { get; set; }

        public int CatalogoPlanesBeneficiosId { get; set; }

        public CatalogoDto CatalogoPlanesBeneficios { get; set; }

        public int CatalogoAsociacionId { get; set; }

        public CatalogoDto CatalogoAsociacion { get; set; }

        public int CatalogoAptoMedicoId { get; set; }

        public CatalogoDto CatalogoAptoMedico { get; set; }

        public int CatalogoDivisionPersonalId { get; set; }

        public CatalogoDto CatalogoDivisionPersonal { get; set; }

        public int CatalogoSubdivisionPersonalId { get; set; }

        public CatalogoDto CatalogoSubdivisionPersonal { get; set; }

        public int CatalogoTipoContratoId { get; set; }

        public CatalogoDto CatalogoTipoContrato { get; set; }

        public int CatalogoClaseContratoId { get; set; }

        public CatalogoDto CatalogoClaseContrato { get; set; }

        public int CatalogoFuncionId { get; set; }

        public CatalogoDto CatalogoFuncion { get; set; }

        public int CatalogoTipoNominaId { get; set; }

        public CatalogoDto CatalogoTipoNomina { get; set; }

        public int CatalogoPeriodoNominaId { get; set; }

        public CatalogoDto CatalogoPeriodoNomina { get; set; }

        public int CatalogoFormaPagoId { get; set; }

        public CatalogoDto CatalogoFormaPago { get; set; }

        public int CatalogoGrupoId { get; set; }

        public CatalogoDto CatalogoGrupo { get; set; }

        public int CatalogoSubgrupoId { get; set; }

        public CatalogoDto CatalogoSubgrupo { get; set; }

        public int CatalogoBancoId { get; set; }

        public CatalogoDto CatalogoBanco { get; set; }

        public int CatalogoTipoCuentaId { get; set; }

        public CatalogoDto CatalogoTipoCuenta { get; set; }

        public int EmpresaId { get; set; }

        public EmpresaDto Empresa { get; set; }

        public int PaisNacimientoId { get; set; }

        public int LocacionTrabajoId { get; set; }

        public LocacionDto LocacionTrabajo { get; set; }

        public int UsuarioId { get; set; }

        public int? ContratoId { get; set; }

        public int TurnoId { get; set; }

        public TimeSpan HorarioDesde { get; set; }

        public TimeSpan HorarioHasta { get; set; }

        public int EmpleadoIdSap { get; set; }

        public int CandidatoIdSap { get; set; }

        public string NumeroIdentificacion { get; set; }

        public string Nombres { get; set; }

        public DateTime? FechaIngreso { get; set; }

        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public DateTime? FechaMatrimonio { get; set; }

        public int Meta4 { get; set; }

        public string Posicion { get; set; }

        public DateTime? FechaCaducidadContrato { get; set; }

        public bool? EjecutorObra { get; set; }

        public decimal RemuneracionMensual { get; set; }

        public string NumeroCuenta { get; set; }

        public string NumeroLegajoTemporal { get; set; }

        public string NumeroLegajoDefinitivo { get; set; }

        public int NumeroHijos { get; set; }

        public string Estado { get; set; }

        public bool TieneAusentismo { get; set; }

        public bool RegistroMasivo { get; set; } = false;

        public int CatalogoEncuadreId { get; set; }

        public CatalogoDto CatalogoEncuadre { get; set; }

        public int CatalogoEncargadoPersonalId { get; set; }

        public CatalogoDto CatalogoEncargadoPersonal { get; set; }

        public bool ValidacionCedula { get; set; } = false;

        public string NombresApellidos { get; set; }

        public int? CatalogoCodigoIncapacidadId { get; set; }

        public CatalogoDto CatalogoCodigoIncapacidad { get; set; }

        public int? CatalogoSectorId { get; set; }

        public CatalogoDto CatalogoSector { get; set; }

        public int? CatalogoViaPagoId { get; set; }

        public CatalogoDto CatalogoViaPago { get; set; }

        public DateTime? FechaAlta { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }

    }
}

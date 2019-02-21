using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using com.cpp.calypso.comun.dominio;
using JetBrains.Annotations;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class Colaborador : Entity, ISoftDelete
    {
        [CanBeNull]
        [DisplayName("Tipo de identificacion")]
        public int catalogo_tipo_identificacion_id { get; set; }


        [DisplayName("Género")]
        public int catalogo_genero_id { get; set; }

        [DisplayName("Etnia")]
        public int catalogo_etnia_id { get; set; }

        [DisplayName("Estado Civil")]
        public int catalogo_estado_civil_id { get; set; }

        [CanBeNull]
        [DisplayName("Código Siniestro")]
        public int? catalogo_codigo_siniestro_id { get; set; }

        [CanBeNull]
        [DisplayName("Formación Educativa")]
        public int? catalogo_formacion_educativa_id { get; set; }

        [DisplayName("Grupo Personal")]
        public int catalogo_grupo_personal_id { get; set; }

        [DisplayName("Destino (Estancia):")]
        public int catalogo_destino_estancia_id { get; set; }

        [DisplayName("Sitio de Trabajo")]
        public string catalogo_sitio_trabajo_id { get; set; }

        [DisplayName("Área")]
        public int catalogo_area_id { get; set; }

        [DisplayName("Cargo")]
        public int catalogo_cargo_id { get; set; }

        [DisplayName("Vinculo Laboral")]
        public int catalogo_vinculo_laboral_id { get; set; }

        [DisplayName("Clase")]
        public int catalogo_clase_id { get; set; }

        [DisplayName("Plan de Beneficios")]
        public int catalogo_plan_beneficios_id { get; set; }

        [DisplayName("Opción Plan de Salud")]
        public int catalogo_plan_salud_id { get; set; }

        [DisplayName("Cobertura Dependiente")]
        public int catalogo_cobertura_dependiente_id { get; set; }

        [DisplayName("Planes de Beneficios")]
        public int catalogo_planes_beneficios_id { get; set; }

        [DisplayName("Asociación")]
        public int catalogo_asociacion_id { get; set; }

        [DisplayName("Tipo Apto Médico")]
        public int catalogo_apto_medico_id { get; set; }

        [DisplayName("División Personal")]
        public int catalogo_division_personal_id { get; set; }

        [DisplayName("Subdivisión Personal")]
        public int catalogo_subdivision_personal_id { get; set; }

        [DisplayName("Tipo de Contrato")]
        public int catalogo_tipo_contrato_id { get; set; }

        [DisplayName("Clase de Contrato")]
        public int catalogo_clase_contrato_id { get; set; }

        [DisplayName("Función")]
        public int catalogo_funcion_id { get; set; }

        [DisplayName("Tipo de Nómina")]
        public int catalogo_tipo_nomina_id { get; set; }

        [DisplayName("Período de Nómina")]
        public int catalogo_periodo_nomina_id { get; set; }

        [DisplayName("Forma de Pago")]
        public int catalogo_forma_pago_id { get; set; }

        [DisplayName("Grupo (Categoría O PC)")]
        public int catalogo_grupo_id { get; set; }

        [DisplayName("Sub Grupo (Cuartil)")]
        public int catalogo_subgrupo_id { get; set; }

        [DisplayName("Banco")]
        public int catalogo_banco_id { get; set; }

        [DisplayName("Tipo de Cuenta")]
        public int catalogo_tipo_cuenta_id { get; set; }

        [DisplayName("Empresa")]
        public int empresa_id { get; set; }


        [DisplayName("País de Nacimiento")]
        public int pais_pais_nacimiento_id { get; set; }


        [DisplayName("Locación de Trabajo")]
        public int locacion_trabajo_id { get; set; }

        [DisplayName("ID Usuario:")]
        public int usuario_id { get; set; }

        [DisplayName("Proyecto")]
        public int? ContratoId { get; set; }

        [DisplayName("Turno")]
        public int turno_id { get; set; }

        [DisplayName("Horario Desde")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan horario_desde { get; set; }

        [DisplayName("Horario Hasta")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan horario_hasta { get; set; }

        [DisplayName("ID Empleado:")]
        public int empleado_id_sap { get; set; }

        [DisplayName("ID Candidato")]
        public int candidato_id_sap { get; set; }

        [CanBeNull]
        [MaxLength(25)]
        [DisplayName("Nro Identificación:")]
        public string numero_identificacion { get; set; }

        [CanBeNull]
        [DisplayName("Nombres")]
        public string nombres { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Fecha Ingreso")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? fecha_ingreso { get; set; }

        [CanBeNull]
        [DisplayName("Prmer Apellido:")]
        public string primer_apellido { get; set; }

        [CanBeNull]
        [DisplayName("Segundo Apellido:")]
        public string segundo_apellido { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Fecha Nacimiento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? fecha_nacimiento { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Fecha Matrimonio")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? fecha_matrimonio { get; set; }

        [DisplayName("Meta 4")]
        public int meta4 { get; set; }

        [DisplayName("Posición")]
        public string posicion { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Caducidad Contrato")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? fecha_caducidad_contrato { get; set; }

        [DisplayName("Es Ejecutor de Obra?")]
        public bool? ejecutor_obra { get; set; }

        [DisplayName("Remuneración Mensual")]
        public decimal remuneracion_mensual { get; set; }

        [DisplayName("Número de Cuenta")]
        public string numero_cuenta { get; set; }

        [DisplayName("Número de Legajo")]
        public string numero_legajo_temporal { get; set; }

        [DisplayName("Número de Legajo")]
        public string numero_legajo_definitivo { get; set; }

        [DisplayName("Numero Hijos:")]
        public int numero_hijos { get; set; }

        [CanBeNull]
        [DisplayName("Estado")]
        public string estado { get; set; }

        [Obligado]
        [DisplayName("Estado")]
        public bool registro_masivo { get; set; } = false;

        [DisplayName("Encuadre")]
        public int catalogo_encuadre_id { get; set; }

        [DisplayName("Encargado de personal:")]
        public int catalogo_encargado_personal_id { get; set; }

        [DisplayName("Validación Cédula")]
        public bool validacion_cedula { get; set; } = false;

        [CanBeNull]
        [DisplayName("Nombres Apellidos")]
        public string nombres_apellidos { get; set; }

        [DisplayName("Codigo Incapacidad")]
        public int? catalogo_codigo_incapacidad_id { get; set; }

        [DisplayName("Sector")]
        public int? catalogo_sector_id { get; set; }

        [DisplayName("Vía de Pago")]
        public int? catalogo_via_pago_id { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Fecha Alta")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime? fecha_alta { get; set; }

        public bool IsDeleted { get; set; }
    }
}

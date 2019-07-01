using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class ReporteMovil : Entity
    {
        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public string Parametros { get; set; }

        public int CatalogoServicioId { get; set; }

        public Catalogo CatalogoServicio { get; set; }

        public string Rol { get; set; }

        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public string uid { get; set; }
    }
}

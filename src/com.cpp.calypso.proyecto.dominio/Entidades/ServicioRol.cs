using Abp.Domain.Entities;
using System;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class ServicioRol : Entity, ISoftDelete
    {
        public int ServicioId { get; set; }

        public Catalogo Servicio { get; set; }

        public string Rol { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }
}

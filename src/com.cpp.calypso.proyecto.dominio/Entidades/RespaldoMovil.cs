using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Newtonsoft.Json.Linq;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class RespaldoMovil : Entity
    {
        public int UserId { get; set; }

        public string Json { get; set; }

        public DateTime FechaRegistro { get; set; }

        public string Estado { get; set; }
    }
}

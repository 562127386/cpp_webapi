using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace com.cpp.calypso.webapi.Api.Controllers.Models
{
    public class SincronizacionLogModel
    {
        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public string Log { get; set; }
    }
}
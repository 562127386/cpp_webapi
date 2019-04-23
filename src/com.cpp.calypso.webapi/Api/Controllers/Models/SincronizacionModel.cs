using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace com.cpp.calypso.webapi.Api.Controllers.Models
{


    public class SincronizacionModel
    {
        [Required]
        public string Versiones { get; set; }

        [Required]
        public string Usuarios { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace com.cpp.calypso.webapi.Api.Controllers.Models
{
    public class AuthPinModel
    {
        [Required]
        public string UsernameOrEmailAddress { get; set; }

        [Required]
        public string Pin { get; set; }
        
    }
}
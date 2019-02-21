using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Abp.Domain.Entities;

namespace com.cpp.calypso.comun.dominio
{

    /// <summary>
    /// View
    /// </summary>
    [Serializable]
    public class View : Entity
    {

 
        [Required]
        [StringLength(255)]
        public string Name { set; get; }

        /// <summary>
        /// the model linked to the view. (String)
        /// </summary>
        [Required]
        [StringLength(510)]
        public string Model { set; get; }


        /// <summary>
        /// type the model linked to the view
        /// </summary>
        [XmlIgnore]
        [NotMapped]
        public Type ModelType { set; get; }


        /// <summary>
        /// the description of the view's layout
        /// </summary>
        public string Arch { set; get; }

        /// <summary>
        /// Object Layout generate from Arch - XML
        /// </summary>
        [XmlIgnore]
        [NotMapped]
        public ILayout Layout { set; get; }

       

    }

}
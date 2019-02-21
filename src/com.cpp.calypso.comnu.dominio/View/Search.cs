using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Xml.Serialization;

namespace com.cpp.calypso.comun.dominio
{

    
 
    [Serializable]
    public class Search : ILayout
    {
        [XmlIgnore]
        [NotMapped]
        public IGenerateWidget GenerateWidget { set; get; }

        /// <summary>
        /// Fields 
        /// </summary>
        public List<FieldSearch> Fields { set; get; }

        /// <summary>
        /// Filters
        /// </summary>
        public List<Filter> Filters { set; get; }


        public Search()
        {
            Fields = new List<FieldSearch>();
            Filters = new List<Filter>();

            //Generioc
            GenerateWidget = new GenericoGenerateWidget();

        }

        public void ProcessView(Type modelType)
        {
 
            //1. Procesar vista. Verificar si los campos existen en modelo
            //Si no se ha especificado alguna descripcion para las etiquetas, obtenerlas desde el model con DataAnnotations
        
            //Model
            PropertyInfo[]
                propertiesModel = modelType.GetProperties();

            foreach (var item in Fields)
            {
                bool exits = false;
                //item.Name
                foreach (var pro in propertiesModel)
                {
                    if (item.Name.ToUpper() == pro.Name.ToUpper())
                    {
                        exits = true;

                        //Si no existe, utilizar DataAnnotations del modelo
                        if (string.IsNullOrEmpty(item.String))
                        {
                            item.String = pro.GetDescription();
                        }

                        item.FieldType = pro.PropertyType;
                    }
                }

                //TODO: MEJORAS: Recuperar las opciones. del widget. establecidos... (json)
                //Como determinar el tipo de objeto para deserializar.... por el nombre del widget...
                //encontrar el objeto ... correspondiente... 

                if (!exits)
                {
                    var msg = string.Format("El Campo {0} definido en la vista, no existe como propiedad en el objeto {1}", item.Name, modelType);
                    throw new GenericException(msg, msg);

                }
            }

            //return viewUpdate;
        }
    }


    /// <summary>
    /// fields define domains or contexts with user-provided values. 
    /// When search domains are generated, field domains are composed with one another
    /// and with filters using AND.
    /// </summary>
    [Serializable]
    public class FieldSearch
    {
        /// <summary>
        ///  the name of the field to filter on
        /// </summary>
        [Required]
        [StringLength(80)]
        [XmlAttribute]
        public string Name { set; get; }


        /// <summary>
        /// Type of field in model
        /// </summary>
        [XmlIgnore]
        [NotMapped]
        public Type FieldType { set; get; }

        /// <summary>
        /// the field's label
        /// </summary>
        [XmlAttribute]
        public string String { set; get; }

        /// <summary>
        /// Default
        /// </summary>
        private string _Operator = "==";


        /// <summary>
        /// by default, fields generate domains of the form [(name, operator, provided_value)] 
        /// where name is the field's name and provided_value is the value provided by the user, 
        /// possibly filtered or transformed (e.g. a user is expected to provide the label of a selection field's value, not the value itself).
        /// The operator attribute allows overriding the default operator, 
        /// which depends on the field's type (e.g. = for float fields but ilike for char fields)
        /// </summary>
        [XmlAttribute]
        public string Operator
        {
            set
            {
                _Operator = value;
            }
            get
            {
                return _Operator;
            }
        }


        /// <summary>
        /// use specific search widget for the field (the only use case in standard Odoo 8.0 is a selection widget for Many2one fields)
        /// </summary>
        [XmlAttribute]
        public string Widget { set; get; }

        /// <summary>
        /// JSON object specifying configuration option for the field's widget (including default widgets)
        /// </summary>
        [XmlAttribute]
        public string Options { set; get; }


        ///// <summary>
        ///// domain
        /////if the field can provide an auto-completion(e.g.Many2one), filters the possible completion results.
        ///// </summary>
        //[XmlAttribute]
        //public string Domain { set; get; }



    }

    /// <summary>
    /// a filter is a predefined toggle in the search view, it can only be enabled or disabled. 
    /// Its main purposes are to add data to the search context 
    /// (the context passed to the data view for searching/filtering),
    /// or to append new sections to the search filter.
    /// </summary>
    [Serializable]
    public class Filter
    {

        /// <summary>
        ///  logical name for the filter, can be used to enable it by default, can also be used as inheritance hook
        /// </summary>
        [Required]
        [StringLength(80)]
        [XmlAttribute]
        public string Name { set; get; }


        /// <summary>
        /// A domain is a list of criteria, each criterion being a triple (either a list or a tuple)
        /// of (field_name, operator, value)
        /// 
        /// TODO: Trabajar JSON para simplicar parse
        /// [{'Field':'Name','Operator':'==','Value':'foo'},{'Field':'Id','Operator':'<','Value':5},{'Field':'Active','Operator':'!=','Value':true},{'Field':'Date','Operator':'<','Value':'2016-05-18'}]
        /// </summary>
        [XmlAttribute]
        public string Domain { set; get; }


        /// <summary>
        /// the label of the filter
        /// </summary>
        [XmlAttribute]
        public string String { set; get; }

    }
}
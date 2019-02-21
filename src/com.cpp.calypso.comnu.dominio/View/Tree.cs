using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Xml.Serialization;

namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Tree : ILayout
    {
        [XmlIgnore]
        [NotMapped]
        public IGenerateWidget GenerateWidget { set; get; }

        private bool _Create = true;

        /// <summary>
        /// Allows disabling the corresponding action create
        /// </summary>
        [XmlAttribute]
        public bool Create
        {
            set
            {
                _Create = value;
            }
            get
            {
                return _Create;
            }
        }

        private bool _Edit = true;

        /// <summary>
        /// Allows disabling the corresponding action Edit
        /// </summary>
        [XmlAttribute]
        public bool Edit
        {
            set
            {
                _Edit = value;
            }
            get
            {
                return _Edit;
            }
        }

        private bool _Delete = true;

        /// <summary>
        /// Allows disabling the corresponding action Delete
        /// </summary>
        [XmlAttribute]
        public bool Delete
        {
            set
            {
                _Delete = value;
            }
            get
            {
                return _Delete;
            }
        }


        private bool _Details = true;

        /// <summary>
        /// Allows disabling the corresponding action Details
        /// </summary>
        [XmlAttribute]
        public bool Details
        {
            set
            {
                _Details = value;
            }
            get
            {
                return _Details;
            }
        }


        private string _DefaultOrder = string.Empty;


        /// <summary>
        /// overrides the ordering of the view, replacing the model's default order. 
        /// The value is a comma-separated list of fields, postfixed by desc to sort in reverse order:
        /// 
        /// <tree default_order="sequence,name desc">
        /// 
        /// </summary>
        [XmlAttribute(AttributeName = "default_order") ]
        public string DefaultOrder
        {
            set
            {
                _DefaultOrder = value;
            }
            get
            {
                return _DefaultOrder;
            }
        }


        

        public List<Field> Fields { set; get; }

        /// <summary>
        /// displays a button in a list cell. additional  Crued (Create,Edit,Delete)
        /// </summary>
        public List<Button> Buttons { set; get; }
        

        public Tree()
        {
            //Generioc
            GenerateWidget = new GenericoGenerateWidget();

            Fields = new List<Field>();
            Buttons = new List<Button>();
        }

        public Field GetField(string name) {

            foreach (var item in Fields)
            {
                if (item.Name == name)
                    return item;
            }

            return null;
        }

         

        public void ProcessView(Type modelType)
        {
            //TODO: Add Cache

            //Procesar vista. Verificar si los campos existen en modelo
            //Si no se ha especificado alguna descripcion para las etiquetas, obtenerlas desde el model con DataAnnotations
            var viewUpdate = this;

            //Model
            PropertyInfo[]
                propertiesModel = modelType.GetProperties();

            foreach (var item in viewUpdate.Fields)
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
    /// 
    /// </summary>
    [Serializable]
    public class Field
    {
        /// <summary>
        ///  the name of the field to display in the current model. 
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
        /// the title of the field's column (by default, uses the string of the model's field)
        /// </summary>
        [XmlAttribute]
        public string String { set; get; }


        private bool _Invisible = false;

        /// <summary>
        /// invisible fetches and stores the field, but doesn't display the column in the table. Necessary for fields which shouldn't be displayed but are used by e.g.@colors
        /// </summary>
        [XmlAttribute]
        public bool Invisible
        {
            set
            {
                _Invisible = value;
            }
            get
            {
                return _Invisible;
            }
        }
 
        /// <summary>
        /// alternate representations for a field's display
        /// </summary>
        [XmlAttribute]
        public string Widget { set; get; }
    }
}
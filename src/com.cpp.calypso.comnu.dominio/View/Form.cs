using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Xml.Serialization;

namespace com.cpp.calypso.comun.dominio
{
    public class GenericoGenerateWidget : IGenerateWidget
    {
        public string AutoGenerate(PropertyInfo property)
        {
            //Generico
            return "TextBox";
        }
    }
      
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Form : ILayout
    {
        [XmlIgnore]
        [NotMapped]
        public IGenerateWidget GenerateWidget { set; get; }

        public List<FieldForm> Fields { set; get; }

        public List<Group> Group { set; get; }

        public Form()
        {
            //Generioc
            GenerateWidget = new GenericoGenerateWidget();
        }


        public Form(IGenerateWidget generateWidget)
        {
            Fields = new List<FieldForm>();
            Group = new List<Group>();
            GenerateWidget = generateWidget;
        }

        /// <summary>
        /// Procesar visa. Verificar si los campos existen en el modelo.
        /// 
        /// Si no se ha especificado un widget, autogenerar uno basado en el tipo de la propiedad del modelo
        /// Pendiente:  Si no se ha especificado alguna descripcion para las etiquetas, obtenerlas desde el model con DataAnnotations
        /// 
        /// </summary>
        /// <param name="modelType"></param>
        public void ProcessView(Type modelType)
        {
            //TODO: Add Cache
 
            //Model
            PropertyInfo[]
                propertiesModel = modelType.GetProperties();

            foreach (var item in Fields)
            {
                ProcessField(item, propertiesModel, modelType);
            }

            foreach (var group in Group)
            {
                foreach (var item in group.Fields)
                {

                    ProcessField(item, propertiesModel, modelType);
                }
            }
        }

        /// <summary>
        /// Analizar si FieldForm, se encuentra en las propiedades de la Entidad (propertiesModel)
        /// </summary>
        /// <param name="fieldForm"></param>
        /// <param name="propertiesModel"></param>
        protected void ProcessField(FieldForm fieldForm, PropertyInfo[] propertiesModel, Type modelType) {

            bool exits = false;
            //item.Name
            foreach (var pro in propertiesModel)
            {
                if (fieldForm.Name.ToUpper() == pro.Name.ToUpper())
                {
                    exits = true;
                    fieldForm.FieldType = pro.PropertyType;

                    if (string.IsNullOrWhiteSpace(fieldForm.Widget)) {

                        //Detectar el widget segun el tipo de dato...
                        fieldForm.Widget = GenerateWidget.AutoGenerate(pro);
                    }
                }
            }


            if (!exits)
            {
                var msg = string.Format("El Campo {0} definido en la vista, no existe como propiedad en el objeto {1}", fieldForm.Name, modelType);
                throw new GenericException(msg, msg);
            }

        }


    }

    [Serializable]
    public class Group {

        /// <summary>
        /// the title of the group
        /// </summary>
        [XmlAttribute]
        public string String { set; get; }

        private int _Col = 1;

        /// <summary>
        /// The number of columns in a group 
        /// </summary>
        [XmlAttribute]
        public int Col {
            set
            {
                _Col = value;
            }
            get
            {
                return _Col;
            }
        }

        /// <summary>
        /// Fields 
        /// </summary>
        public List<FieldForm> Fields { set; get; }

        public List<Group> Groups { set; get; }

        public Group()
        {
            Fields = new List<FieldForm>();
            Groups = new List<Group>();
        }
    }

    [Serializable]
    public class FieldForm {

        /// <summary>
        ///  the name of the field to render
        /// </summary>
        [Required]
        [StringLength(80)]
        [XmlAttribute]
        public string Name { set; get; }

        /// <summary>
        /// the label of the field's  (by default, uses the string of the model's field)
        /// </summary>
        [XmlAttribute]
        public string String { set; get; }


        /// <summary>
        /// Type of field in model
        /// </summary>
        [XmlIgnore]
        [NotMapped]
        public Type FieldType { set; get; }


        /// <summary>
        /// fields have a default rendering based on their type 
        /// (e.g. String, Boolean, DataTime). 
        /// The widget attributes allows using a different rendering method and context.
        /// </summary>
        [XmlAttribute]
        public string Widget { set; get; }

        /// <summary>
        /// JSON object specifying configuration option for the field's widget (including default widgets)
        /// </summary>
        [XmlAttribute]
        public string Options { set; get; }

        

        private bool _Invisible = false;

        /// <summary>
        /// invisible the field,  doesn't display the field. Necessary for fields which shouldn't be displayed but are used
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



        private bool _Readonly = false;
        /// <summary>
        /// display the field in both readonly and edition mode, but never make it editable
        /// </summary>
        [XmlAttribute]
        public bool Readonly
        {
                set
                {
                _Readonly = value;
                }
                get
                {
                    return _Readonly;
                }
         }


        /// <summary>
        /// HTML class to set on the generated element
        /// </summary>
        [XmlAttribute]
        public string Class { set; get; }

        //mode
        //for One2many, display mode(view type) to use for the field's linked records. One of tree, form, kanban or graph. The default is tree (a list display)

        //only displays the field for specific users
        //groups


        /// <summary>
        /// A domain is a list of criteria, each criterion being a triple (either a list or a tuple)
        /// of (field_name, operator, value)
        /// 
        /// TODO: Trabajar JSON para simplicar parse
        /// [{'Field':'Name','Operator':'==','Value':'foo'},{'Field':'Id','Operator':'<','Value':5},{'Field':'Active','Operator':'!=','Value':true},{'Field':'Date','Operator':'<','Value':'2016-05-18'}]
        /// </summary>
        [XmlAttribute]
        public string Domain { set; get; }


    }
}
using System;
using System.Xml.Serialization;

namespace com.cpp.calypso.comun.dominio
{
    [Serializable]
    public class Button
    {
        /// <summary>
        /// icon to use to display the button. (Css)
        /// </summary>
        [XmlAttribute]
        public string Icon { set; get; }

        /// <summary>
        /// if there is no icon, the button's text
        /// if there is an icon, alt text for the icon
        /// </summary>
        [XmlAttribute]
        public string String { set; get; }

        /// <summary>
        /// type of button.
        /// 
        /// Controler:  The button's name is the Controler/Action or Action to use. 
        /// </summary>
        [XmlAttribute]
        public string Type { set; get; }

        /// <summary>
        ///   see type
        /// </summary>
        [XmlAttribute]
        public string Name { set; get; }

    }

      



}
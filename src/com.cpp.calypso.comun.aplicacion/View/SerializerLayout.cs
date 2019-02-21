
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;
using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace com.cpp.calypso.comun.aplicacion
{
    /// <summary>
    /// 
    /// </summary>
    public class SerializerLayout: ISerializerLayout
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arch"></param>
        /// <returns></returns>
        public ILayout GetLayoutFormStringArch(string arch) {

            Guard.AgainstNullOrEmptyString(arch, "arch");

            //1. Get XML
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(arch);
          
            //2. Detectar el primer node del XML para determinar el tipo de Layout
            Type typeLayout = GetTypeLayout(xmlDoc.DocumentElement);

            //3. Deserialize Layout
            var obj = Deserialize(xmlDoc, typeLayout);

            var layout = obj as ILayout;

            return layout;
        }

        protected object Deserialize(XmlDocument xml, Type type)
        {
            XmlSerializer s = new XmlSerializer(type);
            string xmlString = xml.OuterXml.ToString();

            byte[] buffer = GetBytes(xml, xmlString);

            MemoryStream ms = new MemoryStream(buffer);

            XmlReader reader = new XmlTextReader(ms);
            Exception caught = null;

            try
            {
                object o = s.Deserialize(reader);
                return o;
            }

            catch (Exception e)
            {
                caught = e;
            }
            finally
            {
                reader.Close();

                if (caught != null)
                    throw caught;
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        protected byte[] GetBytes(XmlDocument xml, string xmlString) {

           
            var xmlDeclaration = GetXmlDeclaration(xml);
            if (xmlDeclaration != null)
            {

                if (xmlDeclaration.Encoding == Encoding.Unicode.BodyName)
                    return Encoding.Unicode.GetBytes(xmlString);

                if (xmlDeclaration.Encoding == Encoding.UTF8.BodyName)
                    return Encoding.UTF8.GetBytes(xmlString);

                if (xmlDeclaration.Encoding == Encoding.UTF32.BodyName)
                    return Encoding.UTF32.GetBytes(xmlString);

                if (xmlDeclaration.Encoding == Encoding.UTF7.BodyName)
                    return Encoding.UTF7.GetBytes(xmlString);

                throw new NotSupportedException(string.Format("Encoding not SupportedSupported. Encoding {0}", xmlDeclaration.Encoding));
            }

            return Encoding.Unicode.GetBytes(xmlString);
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <returns></returns>
        protected XmlDeclaration GetXmlDeclaration(XmlDocument xmlDocument)
        {
            XmlDeclaration xmlDeclaration = null;
            if (xmlDocument.HasChildNodes)
                xmlDeclaration = xmlDocument.FirstChild as XmlDeclaration;

            if (xmlDeclaration != null)
                return xmlDeclaration;

            return null;

            ////Create an XML declaration. 
            //xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", null, null);

            ////Add the new node to the document.
            //XmlElement root = xmlDocument.DocumentElement;
            //xmlDocument.InsertBefore(xmlDeclaration, root);
            //return xmlDeclaration;
        }
    

        protected Type GetTypeLayout(XmlElement root)
        {
            if (root.Name == typeof(Tree).Name)
                return typeof(Tree);

            if (root.Name == typeof(Form).Name)
                return typeof(Form);

            if (root.Name == typeof(Search).Name)
                return typeof(Search);

            throw new NotSupportedException(string.Format("Type Layout not SupportedSupported. XmlElement.Name {0}", root.Name));
        }

    }
}
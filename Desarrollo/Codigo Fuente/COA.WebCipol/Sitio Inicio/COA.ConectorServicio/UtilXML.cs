using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace COA.ConectorServicio
{
    public static class UtilXML
    {
        public static string GetTagValue(string xml, string tagName, bool replace)
        {
            return GetTagValue(xml, tagName, 0, replace);
        }

        public static string GetTagValue(string xml, string tagName, int index, bool replace)
        {
            if (String.IsNullOrEmpty(tagName)) throw new Exception("tagName no especificado");
            if (String.IsNullOrEmpty(xml)) throw new Exception("xml no especificado");
            if (index < 0) throw new Exception("index no puede ser menor que 0");

            try
            {
                // parsea el XML 
                XDocument xmlDoc = null;

                try
                {
                    xmlDoc = XDocument.Parse(xml);
                }
                catch (XmlException)
                {
                    // NOTA: Por la forma en la que se va a utilizar, puede ser que el xml venga sin nodo root, 
                    //       Esto no es valido, entonces hay que agregarle un nodo dummy y reintentar.
                    xml = "<root>" + xml + "</root>";
                    try
                    {
                        xmlDoc = XDocument.Parse(xml);
                    }
                    catch (Exception e)
                    {
                        // hay otro problema y no se puede continuar
                        throw e;
                    }
                }

                if (xmlDoc != null)
                {
                    // quita los namespaces
                    foreach (XElement e in xmlDoc.Descendants())
                    {
                        if (e.Name.Namespace != XNamespace.None)
                        {
                            e.Name = XNamespace.None.GetName(e.Name.LocalName);
                        }
                        if (e.Attributes().Where(a => a.IsNamespaceDeclaration || a.Name.Namespace != XNamespace.None).Any())
                        {
                            e.ReplaceAttributes(e.Attributes().Select(a => a.IsNamespaceDeclaration ? null : a.Name.Namespace != XNamespace.None ? new XAttribute(XNamespace.None.GetName(a.Name.LocalName), a.Value) : a));
                        }
                    }

                    // busca la lista de los que se pidieron
                    var nodes = (from child in xmlDoc.Descendants(tagName)
                                 select child).ToList();

                    // si existe el item[index] lo devuelve
                    if ((index >= 0) && (index < nodes.Count()))
                    {
                        // obtiene el nodo buscado
                        // por definicion se pidio el string con el contenido del nodo, sin incluirlo
                        var reader = nodes[index].CreateReader();
                        reader.MoveToContent();
                        string result = reader.ReadInnerXml();

                        if (replace)
                        {
                            // NOTA: El xml de un webservice viene mal formado, tiene adentro otro xml con declaracion y todo.
                            //       Eso no es valido, entonces, para que no les de error,
                            //       lo que hicieron fue reemplazar los < y > de ese xml para que no se parsee,
                            //       entonces ese xml va como campo de texto plano. Para poder procesarlo, 
                            //       hay que quitar la declaracion y reemplazar nuevamente los < y > 
                            Regex rgx;

                            string lt = "&lt;"; // <
                            string gt = "&gt;"; // >
                            string xmlpattern = lt + "\\?xml.*" + gt; // declaracion de xml

                            // corrige el xml quitando la declaracion duplicada ?xml
                            rgx = new Regex(xmlpattern);
                            result = rgx.Replace(result, "");

                            // corrige los <
                            rgx = new Regex(lt);
                            result = rgx.Replace(result, "<");

                            // corrige los >
                            rgx = new Regex(gt);
                            result = rgx.Replace(result, ">");
                        }
                        return result;
                    }
                }
                return "";
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string TransformXML(string inputXml, string xsltString)
        {
            try
            {
                // crea el objeto de transformacion
                XslCompiledTransform transform = new XslCompiledTransform();

                // le asigna el XSLT
                using (XmlReader reader = XmlReader.Create(new StringReader(xsltString)))
                {
                    transform.Load(reader);
                }

                // asigna el XML con los datos, y transforma
                StringWriter results = new StringWriter();
                using (XmlReader reader = XmlReader.Create(new StringReader(inputXml)))
                {
                    transform.Transform(reader, null, results);
                }

                // resultado
                return results.ToString();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}

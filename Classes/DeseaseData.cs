using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Task3.Classes.АppointmentData
{
    /// <summary>
    /// Class to represent a Desease data.
    /// </summary>
    [Serializable]
    public class DeseaseData
    {
        /// <summary>
        /// A code of Desease.
        /// </summary>
        [XmlAttribute]
        public uint Code { get; set; }

        /// <summary>
        /// Recipe of Desease.
        /// </summary>
        [XmlAttribute]
        public string Recipe { get; set; }

        /// <summary>
        /// Default constructor is used for xml serialization/deserialization. 
        /// </summary>
        public DeseaseData()
        {
        }

        /// <summary>
        /// Constructor to set data from multiple parameters.
        /// </summary>
        /// <param name="code">A code of Desease.</param>
        /// <param name="Recipe">Recipe of Desease.</param>
        public DeseaseData(uint code, string recipe)
        {
            Code = code;
            Recipe = recipe;
        }

        /// <summary>
        /// Constructor creates its object using XmlAttributeCollection data.
        /// </summary>
        public DeseaseData(XmlAttributeCollection source)
        {
            if (source == null)
            {
                throw new NullReferenceException("can't parse DeseaseData");
            }

            if (!uint.TryParse(source["Code"].Value, out var code))
            {
                throw new InvalidDataException("DeseaseData.Code must be of type 'uint'");
            }

            Code = code;


            Recipe = source["Recipe"].Value;
        }

        /// <summary>
        /// Transfers the DeseaseData object to XElement.
        /// </summary>
        public XElement ToXml()
        {
            return new XElement(
                "DeseaseData",
                new XAttribute("Code", Code),
                new XAttribute("Recipe", Recipe));
        }
    }
}
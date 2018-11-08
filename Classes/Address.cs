using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Task3.Classes.АppointmentData
{
    /// <summary>
    /// Class to represent an address.
    /// </summary>
    [Serializable]
    public class Address
    {
        /// <summary>
        /// A name of the city.
        /// </summary>
        [XmlAttribute]
        public string City { get; set; }

        /// <summary>
        /// A name of the street.
        /// </summary>
        [XmlAttribute]
        public string Street { get; set; }

        /// <summary>
        /// A number of building.
        /// </summary>
        [XmlAttribute]
        public uint BuildingNumber { get; set; }

        /// <summary>
        /// Default constructor is used for xml serialization/deserialization. 
        /// </summary>
        public Address()
        {
        }

        /// <summary>
        /// Constructor to set data from multiple parameters.
        /// </summary>
        /// <param name="city">A name of the city.</param>
        /// <param name="street">A name of the street.</param>
        /// <param name="buildingNumber">A number of building.</param>
        public Address(string city, string street, uint buildingNumber)
        {
            City = city.Trim();
            Street = street.Trim();
            BuildingNumber = buildingNumber;
        }

        /// <summary>
        /// Constructor creates its object using XmlAttributeCollection data.
        /// </summary>
        public Address(XmlAttributeCollection source)
        {
            if (source == null)
            {
                throw new NullReferenceException("can't parse Address");
            }

            City = source["City"].Value;
            Street = source["Street"].Value;
            if (!uint.TryParse(source["BuildingNumber"].Value, out var buidingNumber))
            {
                throw new InvalidDataException("Address.BuildingNumber must be of type 'uint'");
            }

            BuildingNumber = buidingNumber;
        }

        /// <summary>
        /// Transfers the Address object to XElement.
        /// </summary>
        public XElement ToXml()
        {
            return new XElement(
                "Address",
                new XAttribute("City", City),
                new XAttribute("Street", Street),
                new XAttribute("BuildingNumber", BuildingNumber));
        }
    }
}
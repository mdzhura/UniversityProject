using System;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Task3.Classes.АppointmentData;

namespace Task3.Classes.AppointmentData
{
    /// <summary>
    /// Class to represent patient data.
    /// </summary>
    [Serializable]
    public class PatientData
    {
        /// <summary>
        /// The first name of a patient.
        /// </summary>
        [XmlAttribute]
        public string FirstName { get; set; }

        /// <summary>
        /// The second name of a patient.
        /// </summary>
        [XmlAttribute]
        public string LastName { get; set; }

        /// <summary>
        /// An email of the patient.
        /// </summary>
        [XmlAttribute]
        public string Email { get; set; }

        /// <summary>
        /// patient's phone number.
        /// </summary>
        [XmlAttribute]
        public string PhoneNumber { get; set; }

        
        /// <summary>
        /// <summary>
        /// patient's diagnosis.
        /// </summary>
        [XmlAttribute]
        public string Diagnosis { get; set; }

        /// <summary>
        /// patient's address.
        /// </summary>
        
        public Address Address { get; set; }

        /// <summary>
        /// Default constructor is used for xml serialization/deserialization. 
        /// </summary>
        public PatientData()
        {
            Address = new Address();
        }

        /// <summary>
        /// Constructor to set data from multiple parameters.
        /// </summary>
        /// <param name="firstName">The first name of a patient.</param>
        /// <param name="lastName">The second name of a patient.</param>
        /// <param name="email">An email of the patient.</param>
        /// <param name="phoneNumber">patient's phone number.</param>
        /// <param name="patientAddress">An address of a patient.</param>
        public PatientData(
            string firstName,
            string lastName,
            string email,
            string phoneNumber,
            Address patientAddress , 
            string diagnosis
            )
        {
            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            Email = email.Trim();
            PhoneNumber = phoneNumber.Trim();
            Address = patientAddress;
            Diagnosis = diagnosis.Trim();
           
        }

        /// <summary>
        /// Constructor creates its object using XmlNode data.
        /// </summary>
        public PatientData(XmlNode source)
        {
            if (source == null)
            {
                throw new NullReferenceException("can't parse patientData");
            }

            if (source.Attributes == null)
            {
                throw new NullReferenceException("can't parse patientData attributes");
            }

            FirstName = source.Attributes["FirstName"].Value;
            LastName = source.Attributes["LastName"].Value;
            Email = source.Attributes["Email"].Value;
            PhoneNumber = source.Attributes["PhoneNumber"].Value;
            Diagnosis = source.Attributes["Diagnosis"].Value;
            
            var addressNode = source.SelectSingleNode("Address");
            if (addressNode == null)
            {
                throw new NullReferenceException("can't parse patientData.Address");
            }

            Address = new Address(addressNode.Attributes);
        }

        /// <summary>
        /// Transfers the patientData object to XElement.
        /// </summary>
        public XElement ToXml()
        {
            return new XElement(
                "patientData",
                new XAttribute("FirstName", FirstName),
                new XAttribute("LastName", LastName),
                new XAttribute("Email", Email),
                new XAttribute("PhoneNumber", PhoneNumber),
                new XAttribute("Diagnosis", Diagnosis),
                
                Address.ToXml());
        }
    }
}
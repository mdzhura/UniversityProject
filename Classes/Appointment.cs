using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Globalization;
using System.Xml.Serialization;

using Task3.Classes.АppointmentData;

namespace Task3.Classes.AppointmentData
{
    /// <summary>
    /// Represents an Appointment.
    /// </summary>
    [Serializable]
    public class Appointment
    {
        /// <summary>
        /// Holds an id of the Appointment.
        /// </summary>
        [XmlAttribute]
        public long Id { get; set; }

        /// <summary>
        /// Contains Patient personal information.
        /// </summary>
        public PatientData PatientData { get; set; }

       
        /// <summary>
        /// Holds an information about Appointmented Desease.
        /// </summary>
        public DeseaseData DeseaseData { get; set; }

        /// <summary>
        /// Constructs Appointment object from xml node source.
        /// </summary>
        /// <param name="source">Xml node object which contains Appointment data.</param>
        /// <exception cref="InvalidDataException">
        /// Throws if xml node attricutes is null or if id value is not long type.
        /// </exception>
        public Appointment(XmlNode source)
        {
            if (source.Attributes == null)
            {
                throw new InvalidDataException("invalid xml source content");
            }

            if (!long.TryParse(source.Attributes["Id"].Value, out var id))
            {
                throw new InvalidDataException("Appointment.Id must be of type 'uint'");
            }

            Id = id;
            PatientData = new PatientData(source.SelectSingleNode("PatientData"));
          
            var deseaseData = source.SelectSingleNode("DeseaseData");
            if (deseaseData == null)
            {
                return;
            }

            DeseaseData = new DeseaseData(deseaseData.Attributes);
        }

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="id">An id of the Appointment.</param>
        /// <param name="PatientData">Patient data object.</param>
        /// <param name="DeseaseData">Desease data object.</param>
        public Appointment(long id, PatientData patientData, DeseaseData deseaseData)
        {
            Id = id;
            PatientData = patientData;
           
            DeseaseData = deseaseData;
        }

        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public Appointment()
        {
            PatientData = new PatientData();
            DeseaseData = new DeseaseData();
        }

        /// <summary>
        /// Appointment to Xml object converter.
        /// </summary>
        /// <returns>Appointment representation as xml element.</returns>
        public XElement ToXml()
        {
            return new XElement(
                "Appointment",
                new XAttribute("Id", Id),
                PatientData.ToXml(),
                DeseaseData.ToXml());
        }

        /// <summary>
        /// Updates xml node by reference.
        /// </summary>
        /// <param name="node">Current editing xml node.</param>
        /// <param name="new">New Appointment to be set.</param>
        /// <exception cref="NullReferenceException">Throws if node is null or contains invalid data.</exception>
        public static void EditXmlNode(ref XmlNode node, Appointment @new)
        {
            if (node == null)
            {
                throw new NullReferenceException("can't edit node");
            }

            if (node.Attributes != null)
            {
                node.Attributes["Id"].Value = @new.Id.ToString();
            }

            var PatientData = node.SelectSingleNode("PatientData");
            if (PatientData == null)
            {
                throw new NullReferenceException("can't edit PatientData");
            }

            if (PatientData.Attributes == null)
            {
                throw new NullReferenceException("can't edit PatientData.Attributes");
            }

            PatientData.Attributes["FirstName"].Value = @new.PatientData.FirstName;
            PatientData.Attributes["LastName"].Value = @new.PatientData.LastName;
            PatientData.Attributes["Email"].Value = @new.PatientData.Email;
            PatientData.Attributes["PhoneNumber"].Value = @new.PatientData.PhoneNumber;
            PatientData.Attributes["Diagnosis"].Value = @new.PatientData.Diagnosis;
            var PatientAddress = PatientData.SelectSingleNode("Address");
            if (PatientAddress == null)
            {
                throw new NullReferenceException("can't edit PatientData.Address");
            }

            if (PatientAddress.Attributes == null)
            {
                throw new NullReferenceException("can't edit PatientData.Address.Attributes");
            }

            PatientAddress.Attributes["City"].Value = @new.PatientData.Address.City;
            PatientAddress.Attributes["Street"].Value = @new.PatientData.Address.Street;
            PatientAddress.Attributes["BuildingNumber"].Value = @new.PatientData.Address.BuildingNumber.ToString();
            

            var DeseaseData = node.SelectSingleNode("DeseaseData");
            if (DeseaseData == null)
            {
                throw new NullReferenceException("can't edit Appointment.DeseaseData");
            }

            if (DeseaseData.Attributes == null)
            {
                throw new NullReferenceException("can't edit Appointment.DeseaseData.Attributes");
            }

            DeseaseData.Attributes["Code"].Value = @new.DeseaseData.Code.ToString();
            DeseaseData.Attributes["Name"].Value = @new.DeseaseData.Name;
            DeseaseData.Attributes["Recipe"].Value = @new.DeseaseData.Recipe;
        }
    }
}
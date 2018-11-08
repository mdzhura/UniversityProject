using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;
using Task3.Classes.AppointmentData;

namespace Task3.Classes
{
    /// <summary>
    /// Represents an Appointment storage object.
    /// </summary>
    public class AppointmentsStorage
    {
        /// <summary>
        /// Holds storage path.
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// Constructs an object which can connect with storage file.
        /// </summary>
        /// <param name="path"></param>
        public AppointmentsStorage(string path)
        {
            _path = path;
        }

        /// <summary>
        /// Creates new storage file if it does not already exist.
        /// </summary>
        public void CreateIfNotExists()
        {

            if (StorageExists())
            {
               // Console.WriteLine("here");
                return;
            }
            Console.WriteLine("here");
            Stream stream = new FileStream(_path, FileMode.Create);
            new XmlSerializer(typeof(List<Appointment>)).Serialize(stream, new List<Appointment>());
            stream.Close();
        }

        /// <summary>
        /// Retrieves oredr object by its id.
        /// </summary>
        /// <param name="id">Appointment id.</param>
        /// <returns>Appointment object.</returns>
        /// <exception cref="Exception">Throws if object with given id does not exist.</exception>
        public Appointment Retrieve(long id)
        {
            var doc = new XmlDocument();
            var node = FindNode(id, ref doc);
            if (node == null)
            {
                throw new Exception("data not found");
            }

            return new Appointment(node);
        }

        /// <summary>
        /// Retrieve Appointments basic data from the storage.
        /// </summary>
        /// <returns>
        /// Dictionary where key is an id and value is the concatenation of the first and last name of Appointment's author.
        /// </returns>
        /// <exception cref="NullReferenceException">Throws if storage file is broken.</exception>
        public Dictionary<long, string> RetrieveAllIds()
        {
            var dict = new Dictionary<long, string>();
            var doc = new XmlDocument();

            doc.Load(_path);
            var iterator = doc.SelectNodes("/ArrayOfAppointment/Appointment")?.GetEnumerator();
            while (iterator != null && iterator.MoveNext())
            {
                if (iterator.Current is XmlNode current)
                {
                    if (current.Attributes == null)
                    {
                        continue;
                    }

                    var owner = current.SelectSingleNode("PatientData");
                    if (owner?.Attributes == null)
                    {
                        continue;
                    }

                    if (current.Attributes == null)
                    {
                        continue;
                    }

                    dict[long.Parse(current.Attributes["Id"].Value)] =
                        owner.Attributes["FirstName"].Value + " " + owner.Attributes["LastName"].Value;
                }
                else
                {
                    throw new NullReferenceException("storage data is corrupted");
                }
            }

            return dict;
        }

        /// <summary>
        /// Appends new Appointment to the storage.
        /// </summary>
        /// <param name="Appointment">An Appointment to be appended.</param>
        /// <exception cref="InvalidDataException">Throws if the Appointment with given id already exists.</exception>
        public void Add(Appointment Appointment)
        {
            var xmlDoc = new XmlDocument();
            if (FindNode(Appointment.Id, ref xmlDoc) != null)
            {
                throw new InvalidDataException("Appointment already exists");
            }

            var doc = XDocument.Load(_path);
            var Appointments = doc.Element("ArrayOfAppointment");
            Appointments?.Add(Appointment.ToXml());
            doc.Save(_path);
        }

        /// <summary>
        /// Updates existent Appointment by its id.
        /// </summary>
        /// <param name="oldAppointmentId">An id of editable Appointment.</param>
        /// <param name="newAppointment">New Appointment data.</param>
        public void Update(long oldAppointmentId, Appointment newAppointment)
        {
            var doc = new XmlDocument();
            var node = FindNode(oldAppointmentId, ref doc);
            if (node.Attributes == null)
            {
                return;
            }

            Appointment.EditXmlNode(ref node, newAppointment);
            doc.Save(_path);
        }

        /// <summary>
        /// Deletes Appointment from the storage by its id.
        /// </summary>
        /// <param name="id">Appointment id.</param>
        /// <exception cref="NullReferenceException">Throws if storage file is broken.</exception>
        public void Remove(long id)
        {
            var doc = new XmlDocument();
            doc.Load(_path);
            var iterator = doc.SelectNodes("/ArrayOfAppointment/Appointment")?.GetEnumerator();
            while (iterator != null && iterator.MoveNext())
            {
                if (iterator.Current is XmlNode current)
                {
                    if (current.Attributes != null && current.Attributes["Id"].Value.Equals(id.ToString()))
                    {
                        var parentNode = current.ParentNode;
                        if (parentNode == null)
                        {
                            throw new NullReferenceException("storage data is corrupted");
                        }

                        parentNode.RemoveChild(current);
                        doc.Save(_path);
                        return;
                    }
                }
                else
                {
                    throw new NullReferenceException("storage data is corrupted");
                }
            }
        }

        /// <summary>
        /// Searches for xml Appointment node by its id.
        /// </summary>
        /// <param name="id">An id of the Appointment.</param>
        /// <param name="document">Storage document.</param>
        /// <returns>Appointment in XmlNode representation.</returns>
        /// <exception cref="NullReferenceException">Throws if storage file is broken.</exception>
        public XmlNode FindNode(long id, ref XmlDocument document)
        {
            document.Load(_path);
            var iterator = document.SelectNodes("/ArrayOfAppointment/Appointment")?.GetEnumerator();
            while (iterator != null && iterator.MoveNext())
            {
                if (iterator.Current is XmlNode current)
                {
                    if (current.Attributes != null && current.Attributes["Id"].Value.Equals(id.ToString()))
                    {
                        return current;
                    }
                }
                else
                {
                    throw new NullReferenceException("storage data is corrupted");
                }
            }

            return null;
        }

        /// <summary>
        /// Checks if storage exists.
        /// </summary>
        /// <returns>True if storage exists, otherwise returns false.</returns>
        public bool StorageExists()
        {
            return File.Exists(_path);
        }

        /// <summary>
        /// Deletes storage file if it exists.
        /// </summary>
        public void DeleteIfExists()
        {
            if (StorageExists())
            {
                File.Delete(_path);
            }
        }

        /// <summary>
        /// Checks if an Appointment exists by its id. 
        /// </summary>
        /// <param name="id">Appointment id.</param>
        /// <returns>True if Appointment with given id exists, otherwise returns false.</returns>
        public bool AppointmentExists(long id)
        {
            var doc = new XmlDocument();
            doc.Load(_path);
            return FindNode(id, ref doc) != null;
        }
    }
}
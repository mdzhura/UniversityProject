using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace UniversityProject
{
	class HTTPError : IComparable
	{

		/// <summary>
		/// Empty constructor
		/// </summary>
		public HTTPError()
		{
		}
		/// <summary>
		/// Not empty constructor
		/// </summary>
		public HTTPError(string s)
		{
			//Console.WriteLine (s);
			string[] words = s.Split(' ');
			//Console.words.Count
			Console.WriteLine(words[0]);
			code = int.Parse (words [0]);
			description = words [1];
			dateTime = DateTime.Parse(words [2] + " " + words [3] + " " + words [4]);
			//dateTime = DateTime.Parse (words [2], System.Globalization.CultureInfo.InvariantCulture);

		}
		/// <summary>
		/// prints error
		/// </summary>
		public void print()
		{
			Console.WriteLine (code.ToString () + "-" + description + " " + dateTime.ToString ());
		}
		/// <summary>
		/// prints error to file
		/// </summary>
		public void printToFile(StreamWriter sw)
		{
			sw.WriteLine (code.ToString () + "-" + description + " " + dateTime.ToString ());
		}
		/// <summary>
		/// to string
		/// </summary>
		public override string ToString()
		{
			return description + " " + dateTime.ToString ();
		}
		/// <summary>
		/// property code
		/// </summary>
		public int code { get; set; }
		/// <summary>
		/// property description
		/// </summary>
		public String description { get; set; }
		/// <summary>
		/// property dateTime
		/// </summary>
		public DateTime dateTime { get; set; }
		/// <summary>
		/// method compareto
		/// </summary>
		public int CompareTo(object obj)
		{
			var item = obj as HTTPError;
			return code.CompareTo(item.code);
		}
		/// <summary>
		/// method equals
		/// </summary>
		public override bool Equals(object obj)
		{
			var item = obj as HTTPError;

			if (item == null)
			{
				return false;
			}

			return this.code.Equals(item.code);
		}
		/// <summary>
		/// method getHashCode
		/// </summary>
		public override int GetHashCode()
		{
			return this.code.GetHashCode();
		}
	}
}

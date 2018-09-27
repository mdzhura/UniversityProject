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
		/// 
		/// </summary>
		public HTTPError()
		{
		}
		/// <summary>
		/// 
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
		///
		/// </summary>
		public void print()
		{
			Console.WriteLine (code.ToString () + "-" + description + " " + dateTime.ToString ());
		}
		/// <summary>
		/// zhuk debil
		/// </summary>
		public void printToFile(StreamWriter sw)
		{
			sw.WriteLine (code.ToString () + "-" + description + " " + dateTime.ToString ());
		}
		/// <summary>
		/// zhuk chmo
		/// </summary>
		public string ToString()
		{
			return description + " " + dateTime.ToString ();
		}
		/// <summary>
		/// zhuk olen
		/// </summary>
		public int code { get; set; }
		/// <summary>
		/// zhuk tylen
		/// </summary>
		public String description { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime dateTime { get; set; }
		/// <summary>
		/// or
		/// </summary>
		public int CompareTo(object obj)
		{
			var item = obj as HTTPError;
			return code.CompareTo(item.code);
		}
		/// <summary>
		/// qwerty
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
		/// 
		/// </summary>
		public override int GetHashCode()
		{
			return this.code.GetHashCode();
		}
	}
}

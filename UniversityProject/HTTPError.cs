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
		public HTTPError()
		{
		}
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
		public void print()
		{
			Console.WriteLine (code.ToString () + "-" + description + " " + dateTime.ToString ());
		}
		public void printToFile(StreamWriter sw)
		{
			sw.WriteLine (code.ToString () + "-" + description + " " + dateTime.ToString ());
		}
		public int code { get; set; }
		public String description { get; set; }
		public DateTime dateTime { get; set; }
		public int CompareTo(object obj)
		{
			var item = obj as HTTPError;
			return code.CompareTo(item.code);
		}
		public override bool Equals(object obj)
		{
			var item = obj as HTTPError;
			if (item == null)
			{
				return false;
			}
			return this.code.Equals(item.code);
		}
		public override int GetHashCode()
		{
			return this.code.GetHashCode();
		}
	}
}
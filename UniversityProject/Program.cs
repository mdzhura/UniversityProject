using System;
using System.IO;
using System.Collections;

namespace UniversityProject
{
	class MainClass
	{
		public static void Main (string[] args)
		{	
			
			StreamReader sr = new StreamReader("errors.txt");

			ArrayList errors = new ArrayList();
			while (true) 
			{
				string s = sr.ReadLine ();
				if (s.Length == 0)
					break;
				Console.WriteLine (s);
				HTTPError er = new HTTPError (s);
				errors.Add (er);
			}
			StreamWriter sw = new StreamWriter ("log.txt");
			foreach(HTTPError trouble in errors)
			{
				trouble.printToFile(sw);
			}
			sw.Close ();

		}
	}
}

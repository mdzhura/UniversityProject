using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace UniversityProject
{
	class MainClass
	{
		public static void Main (string[] args)
		{	
			try
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
				sr.Close ();

				StreamReader sr2 = new StreamReader ("errorsLg.txt");

			//	Console.WriteLine ("Can't open file errorsLog.txt");

				List<string> log = new List<string>();
				while (true) 
				{
					string s = sr2.ReadLine ();
					log.Add (s);
					Console.WriteLine (s);
					if (s.Length == 0)
						break;
					Console.WriteLine (s);
				}
				for(int i = 0; i < log.Count; i++)
				{
					foreach (HTTPError he in errors) 
					{
						int x;
						if (int.TryParse (log[i] , out x)) {
							if (he.code == int.Parse (log[i])) {
								log[i] = he.ToString();
							}
						}
					}
				}
				foreach (string er in log) 
				{
					Console.WriteLine (er);
				}
				StreamWriter sw = new StreamWriter ("codesByDates.txt");
				Tuple<int , DateTime>[] ers = new Tuple<int, DateTime>[errors.Count];
				int idx = 0;
				foreach (HTTPError er in errors) 
				{
					ers [idx++] = new Tuple<int , DateTime> (er.code, er.dateTime);
				}
				for (int i = 0; i < idx; i++) 
				{
					sw.WriteLine(ers[i].Item1.ToString() + " " + ers[i].Item2.ToString());
				}
				sw.Close ();
				//StreamWriter sw = new StreamWriter ("log.txt");
				//foreach(HTTPError trouble in errors)
				//{
				//	trouble.printToFile(sw);
				//}
				//sw.Close ();
			}
			catch (FileNotFoundException excep)
			{
				Console.WriteLine (excep.Message);
			}
			catch
			{
				Console.WriteLine("Unkonown exception");
			}
			//catch 
			//{
			//	Console.WriteLine ("Unknown exception");
				//Console.WriteLine ("Pizdec");
			//}
		}
	}
}

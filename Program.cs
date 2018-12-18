using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            Console.WriteLine("Show all info about the employee with ID 8");
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Employees WHERE EmployeeID=8;";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    result.Append($"{reader.GetName(i)}: {reader.GetValue(i).ToString()}  ");
                }

                Console.WriteLine(result.ToString());
            }
            Console.WriteLine();
            reader.Close();

            Console.WriteLine("Show the list of first and last names of the employees from London");
            command = connection.CreateCommand();
            command.CommandText = "SELECT FirstName, LastName FROM Employees WHERE City='London';";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("{0}\t{1}", reader["FirstName"], reader["LastName"]);
            }
            reader.Close();

            Console.WriteLine("\nCalculate the count of employees from London");
            command.CommandText = "SELECT COUNT(*) AS EmployeeQuantity FROM Employees WHERE City='London';";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["EmployeeQuantity"]);
            }
            reader.Close();

            Console.WriteLine("Show the list of customers’ names who used to order the ‘Tofu’ product, along with the total amount of the product they have ordered and with the total sum for ordered product calculated");
            command = connection.CreateCommand();
            command.CommandText = "SELECT DISTINCT C.ContactName AS Name, SUM(OD.Quantity) AS TotalAmount, SUM(OD.Quantity * OD.UnitPrice) AS TotalSum FROM Customers AS C " +
                "JOIN Orders AS O ON C.CustomerID = O.CustomerID " +
                "JOIN [Order Details] AS OD ON O.OrderID = OD.OrderID " +
                "JOIN  Products AS P ON OD.ProductID = P.ProductID " +
                "WHERE P.ProductName = 'Tofu' " +
                "GROUP BY C.ContactName;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"Name: {reader["Name"]}, TotalAmount: {reader["TotalAmount"]}, TotalSum: {reader["TotalSum"]}");
            }
            reader.Close();

            Console.WriteLine("\nShow the list of french customers’ names who used to order non-french products (use left join)");
            command = connection.CreateCommand();
            command.CommandText = "SELECT DISTINCT C.ContactName AS Name FROM Customers AS C " +
                "LEFT JOIN Orders AS O ON C.CustomerID = O.CustomerID " +
                "WHERE O.ShipCity != ' France' AND C.Country = 'France';";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"Name: {reader["Name"]}");
            }
            reader.Close();


 
            Console.WriteLine("\nShow first and last names of the employees as well as the count of orders each of them have received during the year 1997 (use left join)");
            command.CommandText = "SELECT e.FirstName, e.LastName, COUNT(o.EmployeeID) AS OrdersQuantity FROM Employees AS e LEFT JOIN Orders AS o ON o.EmployeeID=e.EmployeeID WHERE o.OrderDate>='1997-01-01' AND o.OrderDate<='1997-12-31' GROUP BY e.FirstName, e.LastName;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("{0,-10}{1,-10}{2}", reader["FirstName"], reader["LastName"], reader["OrdersQuantity"]);
            }
            reader.Close();

            Console.WriteLine("\nShow all Customers");
            command.CommandText = "SELECT * FROM Customers;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["ContactName"]);
            }
            reader.Close();

            Console.WriteLine("\nShow the list of french customers’ names who have made more than one order (use grouping)");
            command.CommandText = "SELECT c.ContactName FROM Customers AS c, Orders AS o WHERE c.Country='France' GROUP BY c.ContactName HAVING COUNT(o.CustomerID)>1;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["ContactName"]);
            }
            reader.Close();

            Console.WriteLine("\nShow all Employees");
            command.CommandText = "SELECT * FROM Employees WHERE EmployeeID=8;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    result.Append($"{reader.GetName(i)}: {reader.GetValue(i).ToString()}  ");
                }

                Console.WriteLine(result.ToString());
            }
            Console.WriteLine();
            reader.Close();


            Console.WriteLine("\nShow the list of french customers’ names who used to order french products");
            command.CommandText = "SELECT c.ContactName FROM Customers AS c, Orders AS o WHERE c.CustomerID=o.CustomerID AND c.Country='France' AND o.ShipCountry='France';";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["ContactName"]);
            }
            reader.Close();

            Console.WriteLine("\nShow the total ordering sum calculated for each country of customer");
            command.CommandText = "SELECT c.Country, SUM(od.UnitPrice) AS TotalPrice FROM Customers AS c, Orders AS o, [Order Details] AS od WHERE o.CustomerID=c.CustomerID AND o.OrderID=od.OrderID GROUP BY c.Country;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("{0,-10}{1}", reader["Country"], reader["TotalPrice"]);
            }
            reader.Close();

            //Insert 5 new records into Employees table. Fill in the following  fields: LastName, FirstName, BirthDate, HireDate, Address, City, Country, Notes. The Notes field should contain your own name.
            int insertQuantity = 0;
            command.CommandText = "INSERT INTO Employees(LastName, FirstName, BirthDate, HireDate, Address, City, Country, Notes) VALUES ('Bohdan', 'Romaniuk', '1997-07-23', '2017-11-14', 'Shevchenka st. 78', 'Lviv', 'Ukraine', 'Bohdan');";
            insertQuantity += command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO Employees(LastName, FirstName, BirthDate, HireDate, Address, City, Country, Notes) VALUES ('Modest', 'Radomskyi', '1997-11-14', '2017-11-14', 'Bandery st. 8', 'Lviv', 'Ukraine', 'Bohdan');";
            insertQuantity += command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO Employees(LastName, FirstName, BirthDate, HireDate, Address, City, Country, Notes) VALUES ('Vlad', 'Buchella', '1998-08-08', '2017-11-14', 'Lubin st. 73', 'Lviv', 'Ukraine', 'Bohdan');";
            insertQuantity += command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO Employees(LastName, FirstName, BirthDate, HireDate, Address, City, Country, Notes) VALUES ('Mykola', 'Baranov', '1998-01-25', '2017-11-14', 'Stryiska st. 18', 'Lviv', 'Ukraine', 'Bohdan');";
            insertQuantity += command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO Employees(LastName, FirstName, BirthDate, HireDate, Address, City, Country, Notes) VALUES ('Roman', 'Mysh', '1997-09-14', '2017-11-14', 'Naukova st. 16', 'Lviv', 'Ukraine', 'Bohdan');";
            insertQuantity += command.ExecuteNonQuery();
            Console.WriteLine("\nInserted {0} rows", insertQuantity);

            //Delete one of your records
            int deletedQuantity = 0;
            command.CommandText = "DELETE FROM Employees WHERE LastName='Vlad' AND FirstName='Buchella';";
            deletedQuantity += command.ExecuteNonQuery();
            Console.WriteLine("Deleted {0} rows", deletedQuantity);
            connection.Close();
            Console.ReadKey();
        }
    }
}

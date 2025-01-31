using System;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.IO;

namespace VulnerableApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter User ID:");
            string userId = Console.ReadLine();
            GetUser(userId);

            Console.WriteLine("Enter JSON to deserialize:");
            string json = Console.ReadLine();
            DeserializeJson(json);
        }

        // SQL Injection vulnerability
        static void GetUser(string userId)
        {
            string connectionString = "Server=myServer;Database=myDB;User Id=myUser;Password=myPassword;"; // Hardcoded credentials
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Users WHERE Id = '" + userId + "'"; // Vulnerable to SQL Injection
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("User: " + reader["Name"]);
                        }
                    }
                }
            }
        }

        // Insecure Deserialization vulnerability
        static void DeserializeJson(string json)
        {
            try
            {
                var obj = JsonConvert.DeserializeObject(json); // Insecure deserialization
                Console.WriteLine("Deserialized object: " + obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}

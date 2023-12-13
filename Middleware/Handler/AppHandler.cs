using Middleware.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using static System.Net.Mime.MediaTypeNames;
using Application = Middleware.Models.Application;

namespace Middleware.Handler
{
    public class AppHandler
    {
        static string connStr = Properties.Settings.Default.connStr;

        public static List<Application> GetAllAppications()
        {
            List<Application> listOfApps = new List<Application>();
            string queryString = "SELECT * FROM Application";

            try
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    SqlCommand command = new SqlCommand(
                        queryString, connection);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Application app = new Application
                            {
                                Id = (int)reader["id"],
                                Name = (string)reader["name"],
                                Creation_dt = (DateTime)reader["creation_dt"],
                                Res_type = "application"
                            };
                            listOfApps.Add(app);
                        }
                    }
                }
                return listOfApps;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static Application GetApplicationFromDatabase(string name)
        {
            string queryString = "SELECT * FROM Application WHERE name = @name";
            try
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@name", name);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Application app = new Application
                            {

                                Id = (int)reader["id"],
                                Name = (string)reader["name"],
                                Creation_dt = (DateTime)reader["creation_dt"],
                                Res_type = "application"
                            };
                            return app;
                        }
                        else
                        {
                            return null;
                        }

                    }

                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }


        }

        public static Application PostToDatabase(Application application)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                try
                {
                    // Check if the application already exists
                    if (GetApplicationFromDatabase(application.Name) != null)
                    {
                        throw new Exception("There is already an existing application named " + application.Name + " in the database.");
                    }

                    // Insert the new application into the database
                    string queryString = "INSERT INTO Application VALUES (@name, @creation_dt)";
                    string newApplicationName = application.Name.Replace(" ", "_");

                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.Parameters.AddWithValue("@name", newApplicationName);
                        command.Parameters.AddWithValue("@creation_dt", DateTime.Now);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new Exception("No rows were affected by the insert operation.");
                        }
                    }

                    // Retrieve the inserted application
                    Application newApp = GetApplicationFromDatabase(newApplicationName);
                    newApp.Res_type = "application";
                    return newApp;
                }
                catch (Exception ex)
                {
                    // Log the error
                    throw ex;
                }
            }
        }


    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Middleware.Models;

namespace Middleware.Handler
{
    public class AppHandler
    {
        static string connStr = Properties.Settings.Default.connStr;

        public static List<Application> GetAllApplications()
        {
            List<Application> listOfApps = new List<Application>();
            string queryString = "SELECT * FROM Application";

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
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
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }

            return listOfApps;
        }

        public static Application GetApplicationFromDatabase(string name)
        {
            string queryString = "SELECT * FROM Application WHERE name = @name";

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                try
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
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
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
                  
                    throw ex;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
        }

        public static Application UpdateToDatabase(string currentName, Application newApplication)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                try
                {
                    // Check if the application already exists
                    if (GetApplicationFromDatabase(currentName) == null)
                    {
                        throw new Exception("Application with the current name does not exist.");
                    }

                    // Update the application in the database
                    string queryString = "UPDATE Application SET Name = @newName WHERE Name = @currentName";
                    SqlCommand command = new SqlCommand(queryString, connection);
                    string newApplicationName = newApplication.Name.Replace(" ", "_");

                    command.Parameters.AddWithValue("@newName", newApplicationName);
                    command.Parameters.AddWithValue("@currentName", currentName);

                    connection.Open();
                    command.ExecuteNonQuery();

                    // Retrieve the updated application
                    Application updatedApp = GetApplicationFromDatabase(newApplicationName);
                    updatedApp.Res_type = "application";
                    return updatedApp;
                }
                catch (Exception ex)
                {
                 
                    throw ex;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
        }
    }
}

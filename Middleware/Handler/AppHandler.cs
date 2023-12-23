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
            using (SqlCommand command = new SqlCommand(queryString, connection))
            {
                try
                {
                    connection.Open();
                    //Instacing reader
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
                catch (SqlException ex)
                {
                    throw new ApplicationException("Error retrieving application from the database.", ex);
                }
            }
            //Returns the list with the application read in the DB
            return listOfApps;
        }

        public static Application GetApplicationFromDatabase(string name)
        {
            string queryString = "SELECT * FROM Application WHERE name = @name";
            //Creating the connection to SQL DB
            using (SqlConnection connection = new SqlConnection(connStr))
            using (SqlCommand command = new SqlCommand(queryString, connection))
            {
                command.Parameters.AddWithValue("@name", name);

                try
                {
                    connection.Open();
                    //Instances reader
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
                    throw new ApplicationException("Error retrieving application from the database.", ex);
                }
            }
        }

        public static Application PostToDatabase(Application application)
        {
            //Replace empty spaces
            string newApplicationName = application.Name.Replace(" ", "_");
            //Checks if the application already exists
            if (GetApplicationFromDatabase(newApplicationName) != null)
            {
                throw new Exception("There is already an existing application named " + newApplicationName + " in the database.");
            }
            //Create SQL connection to DB and creates a SQL querry string
            using (SqlConnection connection = new SqlConnection(connStr))
            using (SqlCommand command = new SqlCommand("INSERT INTO Application VALUES (@name, @creation_dt)", connection))
            {
                command.Parameters.AddWithValue("@name", newApplicationName);
                command.Parameters.AddWithValue("@creation_dt", DateTime.Now);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    //If not then the application didnt post
                    if (rowsAffected == 0)
                    {
                        throw new Exception("No rows were affected by the insert operation.");
                    }
                    //Searches the app in DB and returns it to newApp and adds the res_type
                    Application newApp = GetApplicationFromDatabase(newApplicationName);
                    newApp.Res_type = "application";
                    return newApp;
                }
                catch (SqlException ex)
                {
                    throw new ApplicationException("Error inserting application into the database.", ex);
                }
            }
        }

        public static Application UpdateToDatabase(string currentName, Application newApplication)
        {
            if (GetApplicationFromDatabase(currentName) == null)
            {
                throw new Exception("Application with the current name does not exist.");
            }
            //Create SQL connection to DB and creates a SQL querry string
            using (SqlConnection connection = new SqlConnection(connStr))
            using (SqlCommand command = new SqlCommand("UPDATE Application SET Name = @newName WHERE Name = @currentName", connection))
            {
                string newApplicationName = newApplication.Name.Replace(" ", "_");

                command.Parameters.AddWithValue("@newName", newApplicationName);
                command.Parameters.AddWithValue("@currentName", currentName);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    //Gets the app from the DB and changes the res_type
                    Application updatedApp = GetApplicationFromDatabase(newApplicationName);
                    updatedApp.Res_type = "application";
                    return updatedApp;
                }
                catch (SqlException ex)
                {
                   
                    throw new ApplicationException("Error updating application in the database.", ex);
                }
            }
        }

        public static void DeleteFromDatabase(string name)
        {
            Application app = GetApplicationFromDatabase(name);
            if (app == null)
            {
                throw new Exception("Application does not exist.");
            }
            //Checks if it has containers and deletes them if so
            List<Container> containers = (List<Container>)ContainerHandler.GetAllContainersByParentIDInDatabase(app.Id);
            if (containers != null)
            {
                foreach (Container container in containers)
                {
                    ContainerHandler.DeleteFromDatabase(name, container.Name);
                }
            }
            //Opens connections and creates the sql command
            using (SqlConnection connection = new SqlConnection(connStr))
            using (SqlCommand command = new SqlCommand("DELETE FROM Application WHERE Name = @Name", connection))
            {
                command.Parameters.AddWithValue("@Name", app.Name);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    // Log the exception or handle it appropriately
                    throw new ApplicationException("Error deleting application from the database.", ex);
                }
            }
        }

    }
}
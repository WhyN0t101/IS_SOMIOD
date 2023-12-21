using Middleware.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml;

namespace Middleware.Handler
{
    public class DataHandler
    {
        static string connStr = Properties.Settings.Default.connStr;
      
        public static int SaveToDatabaseData(Data data, string application_name, string container_name)
        {
   
            int idInserted = -1;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data.Content);

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                string insertCmd = "INSERT INTO Data VALUES (@content, @date, @parent)";
                SqlCommand cmd = new SqlCommand(insertCmd, connection);
                cmd.Parameters.AddWithValue("@content", doc.SelectSingleNode("//content").InnerText);
                cmd.Parameters.AddWithValue("@date", DateTime.Now);

                Container container = ContainerHandler.GetContainerInDatabase(application_name, container_name);

                if (container == null)
                {
                    throw new Exception("No container named " + container_name + " in application " + application_name);
                }

                cmd.Parameters.AddWithValue("@parent", container.Id);

            
                try
                {
                    // Open the database connection and execute the insert command
                    connection.Open();
                    int rowsInserted = cmd.ExecuteNonQuery();
                    if (rowsInserted != 1)
                    {
                        throw new Exception("Error inserting object into database");
                    }

                    Data newData = FindLastObjectInsertedInDatabaseByModuleId(container.Id);
                    if (newData == null)
                    {
                        throw new Exception("Can't find newly created data record in the database");
                    }
                    idInserted = newData.Id;

                }
                catch (SqlException ex)
                {
                    // Handle any errors that may have occurred
                    Console.WriteLine("Error inserting object into database: " + ex.Message);
                    throw new Exception(ex.Message);

                }
            }
            return idInserted;
        }
        public static Data FindLastObjectInsertedInDatabaseByModuleId(int container_id)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                string searchNewlyInsertedData = "SELECT * FROM Data WHERE Parent = @parent ORDER BY Id DESC";
                SqlCommand cmdSelect = new SqlCommand(searchNewlyInsertedData, connection);
                cmdSelect.Parameters.AddWithValue("@parent", container_id);

                try
                {
                    // Open the database connection and execute the search command
                    connection.Open();
                    SqlDataReader reader = cmdSelect.ExecuteReader();

                    // Check if the object was found
                    if (reader.Read())
                    {
                        // Create a new object using the data from the database
                        Data data = new Data
                        {
                            Id = (int)reader["id"],
                            Content = (string)reader["content"],
                            Creation_dt = (DateTime)reader["creation_dt"],
                            Parent = (int)reader["parent"]
                        };
                        return data;
                    }
                    else
                    {
                        // Return null if the object was not found
                        return null;
                    }
                }
                catch (SqlException ex)
                {
                    // Handle any errors that may have occurred
                    throw ex;
                }
            }
        }
          public static Data GetDataFromDatabase(string application_name, string container_name, int data_id)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                // Find Application
                Application application = AppHandler.GetApplicationFromDatabase(application_name);
                if (application == null)
                {
                    return null;
                }

                // Find Module 
                Container container = ContainerHandler.GetContainerInDatabase(application.Name, container_name);

                if (container == null)
                {
                    return null;
                }

                // Set up the command to search for the object by name
                string searchCommand = "SELECT * FROM Data WHERE Id = @Id and Parent = @Parent";
                SqlCommand command = new SqlCommand(searchCommand, connection);
                command.Parameters.AddWithValue("@Id", data_id);
                command.Parameters.AddWithValue("@Parent", container.Id);

                try
                {
                    // Open the database connection and execute the search command
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    // Check if the object was found
                    if (reader.Read())
                    {
                        // Create a new object using the data from the database
                        Data data = new Data
                        {
                            Id = (int)reader["id"],
                            Content = (string)reader["content"],
                            Creation_dt = (DateTime)reader["creation_dt"],
                            Parent = (int)reader["parent"]
                        };
                        return data;
                    }
                    else
                    {
                        // Return null if the object was not found
                        return null;
                    }
                }
                catch (SqlException ex)
                {
                    // Handle any errors that may have occurred
                    throw ex;
                }
            }
        }
    }
}
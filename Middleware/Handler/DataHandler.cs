using Middleware.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Text;
using System.Xml;
using uPLibrary.Networking.M2Mqtt;

namespace Middleware.Handler
{
    public class DataHandler
    {
        static string connStr = Properties.Settings.Default.connStr;

        public static int PostToDatabase(Data data, string application_name, string container_name,string name)
        {
            int idInserted = -1;

            // Parse XML content
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data.Content);

            // Establish database connection
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                // Define the SQL insert command
                string insertCmd = "INSERT INTO Data (name, content, creation_dt, parent) VALUES (@name, @content, @creation_dt, @parent)";

                // Create SqlCommand with parameters
                using (SqlCommand command = new SqlCommand(insertCmd, connection))
                {
                    // Extract content from XML
                    string content = doc.SelectSingleNode("//content").InnerText;

                    // Set parameters for the SQL command
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@content", content);
                    command.Parameters.AddWithValue("@creation_dt", DateTime.Now);

                    // Retrieve container from the database
                    Models.Container container = ContainerHandler.GetContainerInDatabase(application_name, container_name);

                    if (container == null)
                    {
                        throw new Exception($"No container named {container_name} in application {application_name}");
                    }

                    // Add container ID as a parameter
                    command.Parameters.AddWithValue("@parent", container.Id);

                    try
                    {
                        // Open the database connection
                        connection.Open();

                        // Execute the SQL insert command
                        int rowsInserted = command.ExecuteNonQuery();

                        // Check if the insertion was successful
                        if (rowsInserted != 1)
                        {
                            throw new Exception("Error inserting object into the database");
                        }

                        // Retrieve the last inserted data record
                        Data newData = GetLastInsertedInDatabaseByContainer(container.Id);

                        // Check if the newly created data record was found
                        if (newData == null)
                        {
                            throw new Exception("Can't find newly created data record in the database");
                        }

                        // Set the ID of the inserted record
                        idInserted = newData.Id;
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions
                        Console.WriteLine($"Error inserting data into the database: {ex.Message}");
                        throw new Exception("Error inserting data into the database.", ex);
                    }
                }
            }

            // Return the ID of the inserted record
            return idInserted;
        }

        public static Data GetLastInsertedInDatabaseByContainer(int container_id)
        {
            // Establish database connection
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                // Define the SQL select command to retrieve the last inserted data record by container ID
                string searchNewlyInsertedData = "SELECT TOP 1 * FROM Data WHERE Parent = @parent ORDER BY Id DESC";

                // Create SqlCommand with parameters
                using (SqlCommand cmdSelect = new SqlCommand(searchNewlyInsertedData, connection))
                {
                    // Add container ID as a parameter
                    cmdSelect.Parameters.AddWithValue("@parent", container_id);

                    try
                    {
                        connection.Open();

                        // Execute the SQL select command
                        using (SqlDataReader reader = cmdSelect.ExecuteReader())
                        {
                            // Check if the object was found
                            if (reader.Read())
                            {
                                // Create a new Data object using the data from the database
                                Data data = new Data
                                {
                                    Id = (int)reader["Id"],
                                    Content = (string)reader["content"],
                                    Creation_dt = (DateTime)reader["creation_dt"],
                                    Parent = (int)reader["parent"]
                                };
                                return data;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions
                        Console.WriteLine($"Error retrieving data from the database: {ex.Message}");
                        throw new Exception("Error retrieving data from the database.", ex);
                    }
                }
            }
        }



        public static Data GetDataFromDatabase(string application_name, string container_name, int data_id)
        {
            // Establish database connection
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                // Find Application
                Application application = AppHandler.GetApplicationFromDatabase(application_name);

                if (application == null)
                {
                    return null;
                }

                // Find Container 
                Models.Container container = ContainerHandler.GetContainerInDatabase(application.Name, container_name);

                if (container == null)
                {
                    return null;
                }

                // Set up the command to search for the object by name
                string searchCommand = "SELECT * FROM Data WHERE Id = @Id and Parent = @Parent";
                using (SqlCommand command = new SqlCommand(searchCommand, connection))
                {
                    // Add parameters for data ID and container ID
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
                                Name = (string)reader["name"],
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
                        // Handle SQL exceptions
                        Console.WriteLine($"Error retrieving Data from the database: {ex.Message}");
                        throw new Exception("Error retrieving Data from the database.", ex);
                    }
                }
            }
        }
        public static void DeleteFromDatabase(string application_name, string container_name, string data_name)
        {
            // Find the data
            Data obj = GetDataByName(application_name, container_name, data_name);
            if (obj == null)
            {
                throw new Exception("Data Not Found");
            }


            using (SqlConnection connection = new SqlConnection(connStr))
            {
                // Set up the command to delete object from the database
                string deleteCommand = "DELETE FROM Data WHERE name = @Name";
                using (SqlCommand command = new SqlCommand(deleteCommand, connection))
                {
                    // Add parameter for data name
                    command.Parameters.AddWithValue("@Name", data_name);

                    try
                    {
                        // Open the database connection
                        connection.Open();

                        // Execute the SQL delete command
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions
                        Console.WriteLine($"Error deleting Data from the database: {ex.Message}");
                        throw new Exception("Error deleting Data from the database.", ex);
                    }
                }
            }
        }

        public static List<Data> GetAllDataFromContainer(string application_name, string container_name)
        {
            // Creates a list for all data entries
            List<Data> dataList = new List<Data>();
            
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                //Checks if container exists
                Models.Container container = ContainerHandler.GetContainerInDatabase(application_name, container_name);

                if (container == null)
                {
                    throw new Exception($"No container named {container_name} in application {application_name}");
                }
                //Gets all data from container
                string searchCommand = "SELECT * FROM Data WHERE Parent = @Parent";
                using (SqlCommand command = new SqlCommand(searchCommand, connection))
                {
                    command.Parameters.AddWithValue("@Parent", container.Id);

                    try
                    {
                        //Opens connection and executes the querry
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        //Reads each data entrie
                        while (reader.Read())
                        {
                            Data data = new Data
                            {
                                Id = (int)reader["id"],
                                Name = (string)reader["name"],
                                Content = (string)reader["content"],
                                Creation_dt = (DateTime)reader["creation_dt"],
                                Parent = (int)reader["parent"]
                            };
                            //Add the entrie to the list
                            dataList.Add(data);
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Error retrieving Data from the database: {ex.Message}");
                        throw new Exception("Error retrieving Data from the database.", ex);
                    }
                }
            }

            return dataList;
        }

        public static Data GetDataByName(string application_name, string container_name, string data_name)
        {
            // Establish database connection
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                // Find Application
                Application application = AppHandler.GetApplicationFromDatabase(application_name);

                if (application == null)
                {
                    return null;
                }

                // Find Container
                Models.Container container = ContainerHandler.GetContainerInDatabase(application.Name, container_name);

                if (container == null)
                {
                    return null;
                }

                // Define the SQL select command to retrieve the data record by name and container ID
                string searchCommand = "SELECT * FROM Data WHERE name = @name and Parent = @Parent";

                // Create SqlCommand with parameters
                using (SqlCommand command = new SqlCommand(searchCommand, connection))
                {
                    command.Parameters.AddWithValue("@name", data_name);
                    command.Parameters.AddWithValue("@Parent", container.Id);

                    try
                    {
                        connection.Open();
                        // Execute the SQL select command
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                // Create a new Data object using the data from the database
                                Data data = new Data
                                {
                                    Id = (int)reader["id"],
                                    Name = (string)reader["name"],
                                    Content = (string)reader["content"],
                                    Creation_dt = (DateTime)reader["creation_dt"],
                                    Parent = (int)reader["parent"]
                                };
                                return data;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions
                        Console.WriteLine($"Error retrieving object from the database: {ex.Message}");
                        throw new Exception("Error retrieving object from the database.", ex);
                    }
                }
            }
        }

        public static void PublishDataToMosquitto(string application_name, string container_name, Data data, string eventMqt)
        {
            // Create an instance of MqttClient
            MqttClient mcClient = new MqttClient("127.0.0.1");

            // Connect to the MQTT broker
            mcClient.Connect(Guid.NewGuid().ToString());

            if (!mcClient.IsConnected)
            {
                Console.WriteLine("Error connecting to message broker");
                return;
            }

            // Define the MQTT topic based on application and container names
            string topic = application_name + "/" + container_name;
            // Publish the data to the MQTT topic
            mcClient.Publish(topic, Encoding.UTF8.GetBytes(eventMqt + ";" + data.Content));

       
        }
    }
}

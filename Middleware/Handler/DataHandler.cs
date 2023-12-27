using Middleware.Models;
using System;
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

        public static int PostToDatabase(Data data, string application_name, string container_name)
        {
            int idInserted = -1;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data.Content);

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                string insertCmd = "INSERT INTO Data VALUES (@content, @date, @parent)";
                SqlCommand command = new SqlCommand(insertCmd, connection);

                // Extracted content from XML
                string content = doc.SelectSingleNode("//content").InnerText;

                // Add parameters for the object's properties
                command.Parameters.AddWithValue("@content", content);
                command.Parameters.AddWithValue("@date", DateTime.Now);

                Models.Container container = ContainerHandler.GetContainerInDatabase(application_name, container_name);

                if (container == null)
                {
                    throw new Exception($"No container named {container_name} in application {application_name}");
                }

                command.Parameters.AddWithValue("@parent", container.Id);

                try
                {
                    // Open the database connection and execute the insert command
                    connection.Open();
                    int rowsInserted = command.ExecuteNonQuery();

                    if (rowsInserted != 1)
                    {
                        throw new Exception("Error inserting object into the database");
                    }

                    // Get the last inserted data record
                    Data newData = GetLastInsertedInDatabaseByContainer(container.Id);

                    if (newData == null)
                    {
                        throw new Exception("Can't find newly created data record in the database");
                    }
                    idInserted = newData.Id;
                }
                catch (SqlException ex)
                {
                    // Handle any errors that may have occurred
                    Console.WriteLine($"Error inserting object into the database: {ex.Message}");
                    throw new Exception(ex.Message);
                }
            }
            return idInserted;
        }

        public static Data GetLastInsertedInDatabaseByContainer(int container_id)
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

                // Find Container 
                Models.Container container = ContainerHandler.GetContainerInDatabase(application.Name, container_name);

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
        public static void DeleteFromDatabase(string application_name, string container_name, int data_id)
        {
            // Find the data
            Data obj = GetDataFromDatabase(application_name, container_name, data_id);
            if (obj == null)
            {
                throw new Exception("Data Not Found");
            }


            using (SqlConnection connection = new SqlConnection(connStr))
            {
                // Set up the command to delete object from the database
                string insertCommand = "DELETE FROM Data WHERE Id = @Id";
                SqlCommand command = new SqlCommand(insertCommand, connection);

                command.Parameters.AddWithValue("@Id", data_id);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        public static void PublishDataToMosquitto(string application_name, string module_name, Data data, string eventMqt)
        {
            String domain = "127.0.0.1";
            MqttClient mcClient = new MqttClient(IPAddress.Parse(domain));

            mcClient.Connect(Guid.NewGuid().ToString());
            if (!mcClient.IsConnected)
            {
                Console.WriteLine("Error connecting to message broker...");
                return;
            }

            string topic = application_name + "/" + module_name;
            mcClient.Publish(topic, Encoding.UTF8.GetBytes(eventMqt + ";" + data.Content));
        }
    }
}

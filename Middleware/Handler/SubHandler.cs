using Middleware.Models;
using System;
using System.Data.SqlClient;

namespace Middleware.Handler
{
    public class SubHandler
    {
        static string connectionString = Properties.Settings.Default.connStr;

        public static int PostToDatabase(string application_name, string container_name, Subscription subscription)
        {
            // Attributes to send to the DB
            string subscriptionName = subscription.Name.Replace(" ", "-");
            DateTime date = DateTime.Now;

            if (subscription.Event != "creation" && subscription.Event != "deletion" && subscription.Event != "creation and deletion")
            {
                throw new Exception("Event must be 'creation', 'deletion' or 'creation and deletion'");
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Remove Spaces from Name and Add "-"
                subscriptionName = subscriptionName.Replace(" ", "-");

                if (GetSubFromDatabase(application_name, container_name, subscriptionName) != null)
                {
                    string baseName = "subcription";
                    subscriptionName = $"{baseName}_{DateTime.Now.Ticks}".Replace(" ", "_");
                }
                string insertCmd = "INSERT INTO Subscription (name, creation_dt, parent, event, endpoint) VALUES  (@name, @date, @parent, @event, @endpoint)";

                using (SqlCommand command = new SqlCommand(insertCmd, connection))
                {
                    Container container = ContainerHandler.GetContainerInDatabase(application_name, container_name);
                    if (container == null)
                    {
                        throw new Exception($"Container '{container_name}' from Application '{application_name}' does not exist");
                    }

                    // Add the parameters for the object's name and value
                    command.Parameters.AddWithValue("@name", subscriptionName);
                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@parent", container.Id);
                    command.Parameters.AddWithValue("@event", subscription.Event);
                    command.Parameters.AddWithValue("@endpoint", subscription.Endpoint);

                    try
                    {
                        // Open the database connection and execute the insert command
                        connection.Open();
                        return command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        // Handle any errors that may have occurred
                        Console.WriteLine("Error inserting object into the database: " + ex.Message);
                        throw new Exception("Error inserting object into the database.", ex);
                    }
                }
            }
        }

        public static void DeleteFromDatabase(string application_name, string container_name, string subscription_name)
        {
            // Find the subscription
            Subscription subscription = GetSubFromDatabase(application_name, container_name, subscription_name);
            if (subscription == null)
            {
                throw new Exception("Subscription not found");
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Set up the command to delete object from the database
                string deleteCommand = "DELETE FROM Subscription WHERE Id = @id AND Parent = @parent";
                using (SqlCommand command = new SqlCommand(deleteCommand, connection))
                {
                    // Add the parameters for the object's Id and Parent
                    command.Parameters.AddWithValue("@id", subscription.Id);
                    command.Parameters.AddWithValue("@parent", subscription.Parent);

                    try
                    {
                        // Open the database connection and execute the delete command
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        // Handle any errors that may have occurred
                        Console.WriteLine("Error deleting object from the database: " + ex.Message);
                        throw new Exception("Error deleting object from the database.", ex);
                    }
                }
            }
        }

        public static Subscription GetSubFromDatabase(string application_name, string container_name, string subscription_name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Finds Container  
                Container container = ContainerHandler.GetContainerInDatabase(application_name, container_name);
                if (container == null)
                {
                    return null;
                }

                // Set up the command to search for the object by name
                string searchCommand = "SELECT * FROM Subscription WHERE Name = @Name and Parent = @Parent";
                using (SqlCommand command = new SqlCommand(searchCommand, connection))
                {
                    command.Parameters.AddWithValue("@Name", subscription_name);
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
                            return new Subscription
                            {
                                Id = (int)reader["id"],
                                Name = (string)reader["name"],
                                Creation_dt = (DateTime)reader["creation_dt"],
                                Parent = (int)reader["Parent"],
                                Event = (string)reader["Event"],
                                Endpoint = (string)reader["Endpoint"]
                            };
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
                        Console.WriteLine("Error retrieving object from the database: " + ex.Message);
                        throw new Exception("Error retrieving object from the database.", ex);
                    }
                }
            }
        }
    }
}

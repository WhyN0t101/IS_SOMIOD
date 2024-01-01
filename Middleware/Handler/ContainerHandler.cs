using Middleware.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;

namespace Middleware.Handler
{
    public class ContainerHandler
    {
        static string connectionString = Properties.Settings.Default.connStr;
        public static Container GetContainerInDatabase(string application_name, string container_name)
        {
            // Get Application from DB 
            Application applicationObj = AppHandler.GetApplicationFromDatabase(application_name);
            if (applicationObj == null)
            {
                throw new Exception("There is no application named " + application_name);
            }

            // Instance SQL Connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Set up the command to search for the object by name
                string searchCommand = "SELECT * FROM Container WHERE Name = @Name AND Parent = @Parent";
                SqlCommand command = new SqlCommand(searchCommand, connection);
                command.Parameters.AddWithValue("@Name", container_name);
                command.Parameters.AddWithValue("@Parent", applicationObj.Id);

                try
                {
                    // Open the database connection and execute the search command
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if the object was found
                        if (reader.Read())
                        {
                            Container container = new Container
                            {
                                Id = (int)reader["id"],
                                Name = (string)reader["name"],
                                Creation_dt = (DateTime)reader["creation_dt"],
                                Res_type = "container",
                                Parent = (int)reader["parent"]
                            };

                            // List to store Data objects
                            List<Data> dataArray = new List<Data>();

                            // Close the reader for Container
                            reader.Close();

                            // Set up the command to search for Data objects by parent
                            string searchDataCommand = "SELECT * FROM Data WHERE Parent = @ParentData";
                            SqlCommand commandData = new SqlCommand(searchDataCommand, connection);
                            commandData.Parameters.AddWithValue("@ParentData", container.Id);

                            // Execute the reader for Data objects
                            using (SqlDataReader readerData = commandData.ExecuteReader())
                            {
                                // Check if Data objects were found
                                while (readerData.Read())
                                {
                                    Data dataObj = new Data
                                    {
                                        Id = (int)readerData["id"],
                                        Content = (string)readerData["content"],
                                        Creation_dt = (DateTime)readerData["creation_dt"],
                                        Res_type = "data",
                                        Parent = (int)readerData["parent"]
                                    };

                                    dataArray.Add(dataObj);
                                }
                            }

                            // Assign the list of Data objects to the Container
                            container.Data = dataArray;

                            return container;
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
            }
        }

        public static IEnumerable<Container> GetAllContainers(string application_name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Container> containers = new List<Container>();
                Application applicationObj = AppHandler.GetApplicationFromDatabase(application_name);

                if (applicationObj == null)
                {
                    throw new Exception($"There is no application named {application_name}");
                }

                string containerSearchCommand = "SELECT * FROM Container WHERE Parent = @Parent";
                string dataSearchCommand = "SELECT * FROM Data WHERE Parent = @ParentData";

                try
                {
                    connection.Open();

                    // Retrieve Containers
                    using (SqlCommand containerCommand = new SqlCommand(containerSearchCommand, connection))
                    {
                        containerCommand.Parameters.AddWithValue("@Parent", applicationObj.Id);

                        using (SqlDataReader containerReader = containerCommand.ExecuteReader())
                        {
                            while (containerReader.Read())
                            {
                                Container newContainer = new Container
                                {
                                    Id = (int)containerReader["id"],
                                    Name = (string)containerReader["name"],
                                    Creation_dt = (DateTime)containerReader["creation_dt"],
                                    Res_type = "container",
                                    Parent = (int)containerReader["parent"]
                                };
                                containers.Add(newContainer);
                            }
                        }
                    }

                    // Retrieve Data for Each Container
                    foreach (Container container in containers)
                    {
                        List<Data> dataArray = new List<Data>();

                        using (SqlCommand dataCommand = new SqlCommand(dataSearchCommand, connection))
                        {
                            dataCommand.Parameters.AddWithValue("@ParentData", container.Id);

                            using (SqlDataReader dataReader = dataCommand.ExecuteReader())
                            {
                                while (dataReader.Read())
                                {
                                    Data dataObject = new Data
                                    {
                                        Id = (int)dataReader["id"],
                                        Content = (string)dataReader["content"],
                                        Creation_dt = (DateTime)dataReader["creation_dt"],
                                        Res_type = "data",
                                        Parent = (int)dataReader["parent"]
                                    };

                                    dataArray.Add(dataObject);
                                }
                            }
                        }

                        container.Data = dataArray;
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }

                return containers;
            }
        }

        public static IEnumerable<Container> GetAllContainersByParentIDInDatabase(int application_id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Container> containers = new List<Container>();


                string searchCommand = "SELECT * FROM Container WHERE Parent = @Parent";
                SqlCommand command = new SqlCommand(searchCommand, connection);
                command.Parameters.AddWithValue("@Parent", application_id);


                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Container container = new Container
                        {
                            Id = (int)reader["id"],
                            Name = (string)reader["name"],
                            Creation_dt = (DateTime)reader["creation_dt"],
                            Res_type = "data",
                            Parent = (int)reader["parent"]

                        };
                        containers.Add(container);
                    }
                    reader.Close();

                    foreach (Container container in containers)
                    {
                        List<Data> dataArray = new List<Data>();
                        // Set up the command to search for the object by name
                        string searchDataCommand = "SELECT * FROM Data WHERE Parent = @ParentData";
                        SqlCommand commandData = new SqlCommand(searchDataCommand, connection);
                        commandData.Parameters.AddWithValue("@ParentData", container.Id);

                        SqlDataReader readerData = commandData.ExecuteReader();

                        // Check if the object was found
                        while (readerData.Read())
                        {
                            Data DataObj = new Data
                            {
                                Id = (int)readerData["id"],
                                Content = (string)readerData["content"],
                                Creation_dt = (DateTime)readerData["creation_dt"],
                                Res_type = "data",
                                Parent = (int)readerData["parent"]
                            };

                            dataArray.Add(DataObj);
                        }
                        container.Data = dataArray;

                        readerData.Close();
                    }

                    connection.Close();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }

                return containers;
            }
        }
        public static Container PostToDatabase(Container mycontainer, string application_name)
        {
            // Attributes to send to the DB
            string newContainerName = mycontainer.Name;
            DateTime todaysDateAndTime = DateTime.Now;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                // Remove Spaces from Name and Add "-"
                newContainerName = newContainerName.Replace(" ", "-");

                if (GetContainerInDatabase(application_name, newContainerName) != null)
                {
                    throw new Exception("There are already exists a container named " + newContainerName + " in the application " + application_name);
                }


                // Set up the command to insert the object into the database
                string insertCommand = "INSERT INTO Container VALUES (@name, @date, @parent)";
                SqlCommand command = new SqlCommand(insertCommand, connection);

                command.Parameters.AddWithValue("@name", newContainerName);
                command.Parameters.AddWithValue("@date", todaysDateAndTime);

                Application applicationObj = AppHandler.GetApplicationFromDatabase(application_name);
                if (applicationObj == null)
                {
                    throw new Exception("Error finding application with name " + application_name);
                }

                command.Parameters.AddWithValue("@parent", applicationObj.Id);

                // Makes the connection to the Database
                try
                {
                    // Open the database connection and execute the insert command
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    // Handle any errors that may have occurred
                    Console.WriteLine("Error inserting object into database: " + ex.Message);
                    throw new Exception(ex.Message);

                }
            }

            Container obj = GetContainerInDatabase(application_name, newContainerName);
            obj.Res_type = "container";
            return obj;
        }
        //WIP
        public static Container PutToDatabase(string application_name, string container_name, Container updateContainer)
        {
            Container obj = GetContainerInDatabase(application_name, container_name);
            if (obj == null)
            {
                throw new Exception("Null Object");
            }

            Application applicationObj = AppHandler.GetApplicationFromDatabase(application_name);

            if (applicationObj == null)
            {
                throw new Exception("There is no application named  " + application_name);
            }

            string newContainerName = updateContainer.Name;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Set up the command to insert the object into the database
                string insertCommand = "UPDATE Container SET Name = @newName WHERE Name = @currentName AND Parent = @Parent";
                SqlCommand command = new SqlCommand(insertCommand, connection);

                // Replace any spaces to "_"
                newContainerName = newContainerName.Replace(" ", "_");

                // Add the parameters for the object's name and value
                command.Parameters.AddWithValue("@newName", newContainerName);
                command.Parameters.AddWithValue("@currentName", container_name);
                command.Parameters.AddWithValue("@Parent", applicationObj.Id);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }

             Container updatedObj = GetContainerInDatabase(application_name, newContainerName);
             updatedObj.Res_type = "container";
             return updatedObj;
           
        }
        public static void DeleteFromDatabase(string application_name, string container_name)
        {
            Container container = GetContainerInDatabase(application_name, container_name);

            if (container == null)
            {
                throw new Exception("Container with name " + container_name + " was not found!");
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string deleteData = "DELETE FROM Data WHERE Parent = @DataParentId";
                SqlCommand commandData = new SqlCommand(deleteData, connection);
                commandData.Parameters.AddWithValue("@DataParentId", container.Id);

                string deleteSub = "DELETE FROM Subscription WHERE Parent = @SubsParentId";
                SqlCommand commandSub = new SqlCommand(deleteSub, connection);
                commandSub.Parameters.AddWithValue("@SubsParentId", container.Id);

                // Set up the command to delete object from the database
                string insertCommand = "DELETE FROM Container WHERE Name = @name AND Parent = @Parent";
                SqlCommand command = new SqlCommand(insertCommand, connection);

                command.Parameters.AddWithValue("@name", container.Name);
                command.Parameters.AddWithValue("@Parent", container.Parent);

                try
                {
                    connection.Open();
                    commandData.ExecuteNonQuery();
                    commandSub.ExecuteNonQuery();
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

    }
}
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
            Application applicationObj = AppHandler.GetApplicationFromDatabase(application_name);
            if (applicationObj == null)
            {
                throw new Exception("There is no application named " + application_name);
            }

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
                    SqlDataReader reader = command.ExecuteReader();

                    // Check if the object was found
                    if (reader.Read())
                    {
                        Container container = new Container();
                        container.Id = Convert.ToInt32(reader["Id"]);
                        container.Name = reader["Name"].ToString();
                        container.Res_type = "module";
                        container.Creation_dt = Convert.ToDateTime(reader["Creation_dt"]);
                        container.Parent = Convert.ToInt32(reader["Parent"]);

                        List<Data> dataArray = new List<Data>();
                        reader.Close();

                        // Set up the command to search for the object by name
                        string searchDataCommand = "SELECT * FROM Data WHERE Parent = @ParentData";
                        SqlCommand commandData = new SqlCommand(searchDataCommand, connection);
                        commandData.Parameters.AddWithValue("@ParentData", container.Id);

                        SqlDataReader readerData = commandData.ExecuteReader();

                        // Check if the object was found
                        while (readerData.Read())
                        {

                            Data Dataobj = new Data();

                            Dataobj.Id = Convert.ToInt32(readerData["Id"]);
                            Dataobj.Content = readerData["Content"].ToString();
                            Dataobj.Res_type = "data";
                            Dataobj.Creation_dt = Convert.ToDateTime(readerData["Creation_dt"]);
                            Dataobj.Parent = Convert.ToInt32(readerData["Parent"]);

                            dataArray.Add(Dataobj);
                        }
                        readerData.Close();
                        container.Data = dataArray;
                        connection.Close();
                        return container;
                    }
                    else
                    {
                        connection.Close();
                        return null;
                        //throw new Exception("Error finding object "+  module_name);
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
                    throw new Exception("There is no application named  " + application_name);
                }
                string searchCommand = "SELECT * FROM Container WHERE Parent = @Parent";
                SqlCommand command = new SqlCommand(searchCommand, connection);
                command.Parameters.AddWithValue("@Parent", applicationObj.Id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Container newContainer = new Container
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Res_type = "module",
                            Creation_dt = Convert.ToDateTime(reader["Creation_dt"]),
                            Parent = Convert.ToInt32(reader["Parent"])
                        };
                        containers.Add(newContainer);
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

                            Data objData = new Data();

                            objData.Id = Convert.ToInt32(readerData["Id"]);
                            objData.Content = readerData["Content"].ToString();
                            objData.Res_type = "data";
                            objData.Creation_dt = Convert.ToDateTime(readerData["Creation_dt"]);
                            objData.Parent = Convert.ToInt32(readerData["Parent"]);

                            dataArray.Add(objData);
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
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Res_type = "module",
                            Creation_dt = Convert.ToDateTime(reader["Creation_dt"]),
                            Parent = Convert.ToInt32(reader["Parent"])
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

                            Data objData = new Data();

                            objData.Id = Convert.ToInt32(readerData["Id"]);
                            objData.Content = readerData["Content"].ToString();
                            objData.Res_type = "data";
                            objData.Creation_dt = Convert.ToDateTime(readerData["Creation_dt"]);
                            objData.Parent = Convert.ToInt32(readerData["Parent"]);

                            dataArray.Add(objData);
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
                string insertCommand = "INSERT INTO Containers VALUES (@name, @date, @parent)";
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
            Container obj = FindObjectInDatabase(application_name, module_name);
            if (obj == null)
            {
                throw new Exception("Null Object");
            }

            Application applicationObj = ApplicationHandler.FindObjectInDatabase(application_name);
            if (applicationObj == null)
            {
                throw new Exception("There is no application named  " + application_name);
            }

            string newModuleName = updatedModule.Name;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Set up the command to insert the object into the database
                string insertCommand = "UPDATE Modules SET Name = @newName WHERE Name = @currentName AND Parent = @Parent";
                SqlCommand command = new SqlCommand(insertCommand, connection);

                // Replace any spaces to "_"
                newModuleName = newModuleName.Replace(" ", "_");

                // Add the parameters for the object's name and value
                command.Parameters.AddWithValue("@newName", newModuleName);
                command.Parameters.AddWithValue("@currentName", module_name);
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

            Module updatedObj = FindObjectInDatabase(application_name, newModuleName);
            updatedObj.Res_type = "module";
            return updatedObj;
        }

    }
}
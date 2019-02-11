using MySql.Data.MySqlClient;
using System;

namespace ChatReader
{
    class Database
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public Database()
        {
            Initialize();
        }
        private void Initialize()
        {
            this.server = "localhost";
            this.database = "twitch";
            this.uid = "root";
            this.password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            this.database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void Insert(string channel, string username, string message)
        {
            if (message.Contains(@"\"))
            {
                message = "DEBUG: CONTAINS BACKSLASH";
            }
            string query = string.Format("INSERT INTO chat (channel,username, message) VALUES('" + channel + "','" + username + "', '{0}')", message.Replace(@"'", "''"));

            //open connection
            if (this.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    //Execute command
                    cmd.ExecuteNonQuery();
                    //close connection
                    this.CloseConnection();
                }
                catch (Exception)
                {
                    Console.Beep(500, 2500);
                    throw;

                }
            }
        }
    }
}
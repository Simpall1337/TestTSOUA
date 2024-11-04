using System;
using System.Data;
using System.Data.SQLite;
using Dapper;

namespace TestTSOUA.DB
{
    public sealed class Database
    {
        private static readonly Lazy<Database> instance = new Lazy<Database>(() => new Database());
        private readonly IDbConnection connection;

        private Database()
        {
            string connectionString = "Data Source=tasks_db.db;Version=3;";
            connection = new SQLiteConnection(connectionString);
        }

        public static Database Instance => instance.Value;

        public IDbConnection Connection => connection;
        public void OpenConnection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }

}

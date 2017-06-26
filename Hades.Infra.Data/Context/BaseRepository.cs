using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Hades.Infra.Data.Context
{
    public class BaseRepository
    {
        private readonly SqlConnection _minhaConexao;

        private string ConnectionString
            => "data source=.\\SQLEXPRESS;Integrated Security = SSPI; Initial Catalog = SMN_Hades";

        private readonly SqlConnection _connection;
        private SqlCommand Command { get; set; }

        public BaseRepository()
        {
            _connection = Connect();
        }

        private SqlConnection Connect()
        {
            var connection = new SqlConnection(ConnectionString);
            if (connection.State == ConnectionState.Broken)
            {
                connection.Close();
                connection.Open();
            }

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            return connection;
        }

        protected void ExecuteProcedure(Enum procedureName)
        {
            Command = new SqlCommand(procedureName.ToString(), _connection) { CommandType = CommandType.StoredProcedure };
        }

        protected void AddParameter(string parameter, object value)
        {
            Command.Parameters.Add(new SqlParameter(parameter, value ?? DBNull.Value));
        }

        protected void ExecuteNonQuery()
        {
            Command.ExecuteNonQuery();
        }

        protected SqlDataReader ExecuteReader()
        {
            return Command.ExecuteReader();
        }
    }
}

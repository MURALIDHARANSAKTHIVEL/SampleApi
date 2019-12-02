using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using ProjectName.DataAccess.Contracts;
using ProjectName.Shared.AppSettings;
using ProjectName.Shared.Utils;
using Microsoft.Extensions.Options;
using Oracle.ManagedDataAccess.Client;
using MySql.Data.MySqlClient;

namespace ProjectName.DataAccess.SqlDb.Implementations
{
    public abstract class SqlDataAccess : ISqlDataAccess
    {
        private DbConfig _dbConfiguration;
        public SqlDataAccess(IOptions<DbConfig> dbConfig)
        {
            _dbConfiguration = dbConfig.Value;
        }

        public string SqlConnectionString { get; set; }

       

        private SqlConnection GetConnection(int connectionId)
        {  //MySqlClient client = new MySqlClient();

            SqlConnectionString = _dbConfiguration.DefaultConnectionString;
            SqlConnection connection = new SqlConnection(SqlConnectionString);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            return connection;
        }
        private OracleConnection GetOracleConnection()
        {
            OracleConnection connection = new OracleConnection(_dbConfiguration.OracleConnectionString);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            return connection;
        }

        private DbCommand GetCommand(DbConnection connection, string commandText, CommandType commandType)
        {
            SqlCommand command = new SqlCommand(commandText, connection as SqlConnection);
            command.CommandType = commandType;
            command.CommandTimeout = 1000;
            return command;
        }
        private OracleCommand GetOracleCommand(DbConnection connection, string commandText, CommandType commandType)
        {
            OracleCommand command = new OracleCommand(commandText, connection as OracleConnection);
            command.CommandType = commandType;
            command.CommandTimeout = 1000;
            return command;
        }

        public int ExecuteNonQuery(int connectionId, string commandText, List<DbParameter> parameters, CommandType commandType = CommandType.Text)
        {
            int returnValue = -1;
            try
            {
                using (SqlConnection connection = this.GetConnection(connectionId))
                {
                    DbCommand cmd = this.GetCommand(connection, commandText, commandType);
                    UpdateDbNullValues(parameters);
                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    
                    }

                    returnValue = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnValue;
        }

        public object ExecuteScalar(int connectionId, string commandText, List<DbParameter> parameters)
        {
            object returnValue = null;

            try
            {
                using (SqlConnection connection = this.GetConnection(connectionId))
                {
                    DbCommand cmd = this.GetCommand(connection, commandText, CommandType.Text);
                    UpdateDbNullValues(parameters);
                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }

                    returnValue = cmd.ExecuteScalar();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnValue;
        }

        public DbDataReader GetDataReader(int connectionId, string commandText, List<DbParameter> parameters, CommandType commandType = CommandType.Text)
        {
            DbDataReader dataReader;

            try
            {
                SqlConnection connection = this.GetConnection(connectionId);

                DbCommand cmd = this.GetCommand(connection, commandText, commandType);
               
               UpdateDbNullValues(parameters);
                if (parameters != null && parameters.Count > 0)
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                }

                dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataReader;
        }

        public DataSet GetDataSet(int connectionId, string commandText, List<DbParameter> parameters = null, CommandType commandType = CommandType.Text)
        {

            try
            {
                using (SqlConnection connection = this.GetConnection(connectionId))
                {

                    using (var cmd = new SqlCommand(commandText, connection))
                    {
                        UpdateDbNullValues(parameters);
                        if (parameters != null && parameters.Count > 0)
                        {
                            cmd.Parameters.AddRange(parameters.ToArray());
                        }

                        using (var adapter = new SqlDataAdapter(cmd))
                        {

                            var dataset = new DataSet();
                            var resultTable = new DataTable();
                            adapter.Fill(dataset);

                            return dataset;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetDataTable(int connectionId, string commandText, List<DbParameter> parameters = null, CommandType commandType = CommandType.Text)
        {

            try
            {
                using (SqlConnection connection = this.GetConnection(connectionId))
                {

                    using (var cmd = new SqlCommand(commandText, connection))
                    {
                        UpdateDbNullValues(parameters);
                        using (var adapter = new SqlDataAdapter(cmd))
                        {

                            if (parameters != null && parameters.Count > 0)
                            {
                                adapter.SelectCommand.Parameters.AddRange(parameters.ToArray());
                            }

                            var dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            return dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void UpdateDbNullValues(IList<DbParameter> parameterValues)
        {
            if (parameterValues != null)
            {
                //Iterate through all the parameters and replace null values with DbNull.Value
                foreach (DbParameter sqlParameter in parameterValues)
                {
                    if ((sqlParameter.Value is string && string.IsNullOrEmpty(sqlParameter.Value.ToString())) || sqlParameter.Value == null)
                    {
                        sqlParameter.Value = DBNull.Value;
                    }
                }
            }
        }

        public OracleDataReader GetOracleDataReader(string commandText, List<DbParameter> parameters, CommandType commandType = CommandType.Text)
        {
            OracleDataReader dataReader;
            try
            {
                OracleConnection connection = GetOracleConnection();
                OracleCommand cmd = this.GetOracleCommand(connection, commandText, commandType);
                if (parameters.Count > 0)
                    cmd.Parameters.AddRange(parameters.ToArray());
                cmd.BindByName = true;
                dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataReader;
        }
        public int ExecuteOracleNonQuery(string commandText, List<DbParameter> parameters, CommandType commandType = CommandType.Text)
        {
            int returnValue = -1;

            try
            {
                using (OracleConnection connection = this.GetOracleConnection())
                {
                    OracleCommand cmd = this.GetOracleCommand(connection, commandText, commandType);
                    UpdateDbNullValues(parameters);
                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }

                    returnValue = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnValue;
        }
        public DataSet GetOracleDataSet(string commandText, List<DbParameter> parameters = null, CommandType commandType = CommandType.Text)
        {

            try
            {
                using (OracleConnection connection = this.GetOracleConnection())
                {

                    using (var cmd = this.GetOracleCommand(connection, commandText, commandType))
                    {
                        UpdateDbNullValues(parameters);
                        if (parameters != null && parameters.Count > 0)
                        {
                            cmd.Parameters.AddRange(parameters.ToArray());
                        }

                        using (var adapter = new OracleDataAdapter(cmd))
                        {

                            var dataset = new DataSet();
                            var resultTable = new DataTable();
                            adapter.Fill(dataset);

                            return dataset;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetOracleDataTable(string commandText, List<DbParameter> parameters = null, CommandType commandType = CommandType.Text)
        {

            try
            {
                using (OracleConnection connection = this.GetOracleConnection())
                {

                    using (var cmd = this.GetOracleCommand(connection, commandText, commandType))
                    {
                        UpdateDbNullValues(parameters);
                        using (var adapter = new OracleDataAdapter(cmd))
                        {

                            if (parameters != null && parameters.Count > 0)
                            {
                                adapter.SelectCommand.Parameters.AddRange(parameters.ToArray());
                            }

                            var dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            return dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
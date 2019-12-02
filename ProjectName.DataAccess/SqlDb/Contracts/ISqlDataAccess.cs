using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;

namespace ProjectName.DataAccess.Contracts
{


    public interface ISqlDataAccess
    {

        int ExecuteNonQuery(int connectionId,string commandText, List<DbParameter> parameters, CommandType commandType);
        object ExecuteScalar(int connectionId,string commandText, List<DbParameter> parameters);

        DbDataReader GetDataReader(int connectionId,string commandText, List<DbParameter> parameters, CommandType commandType);

        DataSet GetDataSet(int connectionId,string commandText, List<DbParameter> parameters, CommandType commandType);

        DataTable GetDataTable(int connectionId,string commandText, List<DbParameter> parameters, CommandType commandType);
        OracleDataReader GetOracleDataReader (string commandText, List<DbParameter> parameters, CommandType commandType = CommandType.Text);
        int ExecuteOracleNonQuery(string commandText, List<DbParameter> parameters, CommandType commandType = CommandType.Text);
        DataSet GetOracleDataSet(string commandText, List<DbParameter> parameters = null, CommandType commandType = CommandType.Text);
        DataTable GetOracleDataTable(string commandText, List<DbParameter> parameters = null, CommandType commandType = CommandType.Text);


    }
}
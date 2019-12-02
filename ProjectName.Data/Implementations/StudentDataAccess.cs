using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ProjectName.Contract.Data;
using ProjectName.Contract.Model.Request;
using ProjectName.DataAccess.SqlDb.Implementations;
using ProjectName.Shared.AppSettings;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using System;

namespace ProjectName.Data.Implementations
{
    public class StudentDataAccess : SqlDataAccess, IStudentDataAccess
    {
        public StudentDataAccess(IOptions<DbConfig> dbConfig) : base(dbConfig) { }

        public DbDataReader GetStudents()
        {
            string procedureName = "[dbo].[GetStudents]";
            return GetDataReader(1, procedureName, null, CommandType.StoredProcedure);
        }

        public DbDataReader GetStudentByKey(long studentKey)
        {
            string procedureName = "[dbo].[GetStudentByKey]";

            //            string query = "SELECT StudentKey, FirstName, LastName, [Standard] FROM[dbo].[Student] WITH(NOLOCK)"
            // + "WHERE StudentKey = " + studentKey;

            List<DbParameter> dbParameters = new List<DbParameter>();
            
            dbParameters.Add(new SqlParameter { ParameterName = "@StudentKey", Value = studentKey, SqlDbType = SqlDbType.BigInt });
            return GetDataReader(1, procedureName, dbParameters, CommandType.StoredProcedure);
        }

        public DbDataReader CreateStudent(StudentRequest sampleRequest)
        {
            string procedureName = "[dbo].[CreateStudent]";
            List<DbParameter> dbParameters = new List<DbParameter>();
            dbParameters.Add(new SqlParameter { ParameterName = "@FirstName", Value = sampleRequest.FirstName, SqlDbType = SqlDbType.VarChar });
            dbParameters.Add(new SqlParameter { ParameterName = "@LastName", Value = sampleRequest.LastName, SqlDbType = SqlDbType.VarChar });
            dbParameters.Add(new SqlParameter { ParameterName = "@Standard", Value = sampleRequest.Standard, SqlDbType = SqlDbType.VarChar });
            return GetDataReader(1, procedureName, dbParameters, CommandType.StoredProcedure);
        }

        public DbDataReader UpdateStudent(StudentRequest sampleRequest, long studentKey)
        {
            string procedureName = "[dbo].[UpdateStudent]";
            List<DbParameter> dbParameters = new List<DbParameter>();
            dbParameters.Add(new SqlParameter { ParameterName = "@StudentKey", Value = studentKey, SqlDbType = SqlDbType.BigInt });
            dbParameters.Add(new SqlParameter { ParameterName = "@FirstName", Value = sampleRequest.FirstName, SqlDbType = SqlDbType.VarChar });
            dbParameters.Add(new SqlParameter { ParameterName = "@LastName", Value = sampleRequest.LastName, SqlDbType = SqlDbType.VarChar });
            dbParameters.Add(new SqlParameter { ParameterName = "@Standard", Value = sampleRequest.Standard, SqlDbType = SqlDbType.VarChar });
            return GetDataReader(1, procedureName, dbParameters, CommandType.StoredProcedure);
        }

        public DbDataReader DeleteStudent(long studentKey)
        {
            string procedureName = "[dbo].[DeleteStudent]";
            List<DbParameter> dbParameters = new List<DbParameter>();
            dbParameters.Add(new SqlParameter { ParameterName = "@StudentKey", Value = studentKey, SqlDbType = SqlDbType.BigInt });
            return GetDataReader(1, procedureName, dbParameters, CommandType.StoredProcedure);
        }
    }
}
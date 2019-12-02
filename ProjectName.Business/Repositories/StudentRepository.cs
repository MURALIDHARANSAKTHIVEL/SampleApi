using System;
using System.Collections.Generic;
using System.Data.Common;
using ProjectName.Contract.Classes;
using ProjectName.Contract.Data;
using ProjectName.Contract.Model.Request;
using ProjectName.Contract.Model.Response;
using ProjectName.Contract.Repositories;
using ProjectName.Logging.Contracts;

namespace ProjectName.Business.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private IStudentDataAccess _studentDataAccess;
        private ILoggerManager _logger;
        public StudentRepository(IStudentDataAccess sampleDataAccess, ILoggerManager logger)
        {
            _studentDataAccess = sampleDataAccess;
            _logger = logger;
        }

        public List<StudentResponse> GetStudents()
        {
            List<StudentResponse> result = new List<StudentResponse>();
            using (DbDataReader dataReader = _studentDataAccess.GetStudents())
            {
                result = dataReader.ToCustomList<StudentResponse>();
                return result;
            }
        }

        public StudentResponse GetStudentByKey(long studentKey)
        {
            StudentResponse result = new StudentResponse();
            using (DbDataReader dataReader = _studentDataAccess.GetStudentByKey(studentKey))
            {
                result = dataReader.ToCustomEntity<StudentResponse>();
                return result;
            }
        }

        public StudentResponse CreateStudent(StudentRequest sampleRequest)
        {
            StudentResponse result = new StudentResponse();
            using (DbDataReader dataReader = _studentDataAccess.CreateStudent(sampleRequest))
            {
                result = dataReader.ToCustomEntity<StudentResponse>();
                return result;
            }
        }

        public StudentResponse UpdateStudent(StudentRequest sampleRequest, long studentKey)
        {
            StudentResponse result = new StudentResponse();
            using (DbDataReader dataReader = _studentDataAccess.UpdateStudent(sampleRequest, studentKey))
            {
                result = dataReader.ToCustomEntity<StudentResponse>();
                return result;
            }
        }

        public BaseResponse DeleteStudent(long studentKey)
        {
            BaseResponse result = new BaseResponse();
            using (DbDataReader dataReader = _studentDataAccess.DeleteStudent(studentKey))
            {
                if (dataReader.Read())
                {
                    result.IsSuccess = (bool)dataReader["IsStudentDeleted"];
                }
                return result;
            }
        }
    }
}
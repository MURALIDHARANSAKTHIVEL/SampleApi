using System.Data.Common;
using ProjectName.Contract.Model.Request;

namespace ProjectName.Contract.Data
{
    public interface IStudentDataAccess
    {
        DbDataReader GetStudents();
        DbDataReader GetStudentByKey(long studentKey);
        DbDataReader CreateStudent(StudentRequest sampleRequest);
        DbDataReader UpdateStudent(StudentRequest sampleRequest, long studentKey);
        DbDataReader DeleteStudent(long studentKey);
    }
}
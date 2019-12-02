using System.Collections.Generic;
using ProjectName.Contract.Model.Request;
using ProjectName.Contract.Model.Response;

namespace ProjectName.Contract.Repositories
{
    public interface IStudentRepository
    {
        List<StudentResponse> GetStudents();
        StudentResponse GetStudentByKey(long studentKey);
        StudentResponse CreateStudent(StudentRequest sampleRequest);
        StudentResponse UpdateStudent(StudentRequest sampleRequest, long studentKey);
        BaseResponse DeleteStudent(long studentKey);
    }
}
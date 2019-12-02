using System.Collections.Generic;
using ProjectName.Contract.Model.Request;
using ProjectName.Contract.Model.Response;

namespace ProjectName.Contract.Orchestrations
{
    public interface IStudentOrchestration
    {
        List<StudentResponse> GetStudents();
        StudentResponse GetStudentByKey(long studentKey);
        StudentResponse CreateStudent(StudentRequest sampleRequest);
        StudentResponse UpdateStudent(StudentRequest sampleRequest, long studentKey);
        BaseResponse DeleteStudent(long studentKey);
    }
}
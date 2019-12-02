using System.Collections.Generic;
using ProjectName.Contract.Model.Request;
using ProjectName.Contract.Model.Response;
using ProjectName.Contract.Orchestrations;
using ProjectName.Contract.Repositories;
using ProjectName.Logging.Contracts;

namespace ProjectName.Business.Orchestrations
{
    public class StudentOrchestration : IStudentOrchestration
    {
        private ILoggerManager _logger;
        private IStudentRepository _studentRepository;

        public StudentOrchestration(ILoggerManager logger, IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
            _logger = logger;
        }

        public List<StudentResponse> GetStudents()
        {
            return _studentRepository.GetStudents();
        }

        public StudentResponse GetStudentByKey(long studentKey)
        {
            return _studentRepository.GetStudentByKey(studentKey);
        }

        public StudentResponse CreateStudent(StudentRequest sampleRequest)
        {
            return _studentRepository.CreateStudent(sampleRequest);
        }

        public StudentResponse UpdateStudent(StudentRequest sampleRequest, long studentKey)
        {
            return _studentRepository.UpdateStudent(sampleRequest, studentKey);
        }

        public BaseResponse DeleteStudent(long studentKey)
        {
            return _studentRepository.DeleteStudent(studentKey);
        }
    }
}
using System;
using ProjectName.Contract.Model.Request;
using ProjectName.Contract.Orchestrations;
using ProjectName.Logging.Contracts;
using Microsoft.AspNetCore.Mvc;
using ProjectName.Gateway.Utils.Extensions;

namespace ProjectName.Gateway.Controllers
{
    [Route("api")]
    public class StudentController : ControllerBase
    {
        private ILoggerManager _logger;
        private IStudentOrchestration _studentOrchestration;

        public StudentController(ILoggerManager logger, IStudentOrchestration sampleOrchestration) : base()
        {
            _studentOrchestration = sampleOrchestration;
            _logger = logger;
        }

        [HttpGet]
        [Route("say-hello")]
        public IActionResult Hello()
        {
            try
            {
                return Ok("Hello from student controller");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("students")]
        public IActionResult GetStudents()
        {
            try
            {
                var response = _studentOrchestration.GetStudents();
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("students/{studentKey}")]
        public IActionResult GetStudentByKey(long studentKey)
        {
            try
            {
                var response = _studentOrchestration.GetStudentByKey(studentKey);
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("students")]
        public IActionResult CreateStudent([FromBody]StudentRequest sampleRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = _studentOrchestration.CreateStudent(sampleRequest);
                    return Ok(response);
                }
                var modelErrors = ModelState.AllErrors();
                return BadRequest(modelErrors);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("students/{studentKey}")]
        public IActionResult UpdateStudent([FromBody]StudentRequest sampleRequest, [FromRoute]long studentKey)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (sampleRequest.StudentKey == studentKey)
                    {
                        var response = _studentOrchestration.UpdateStudent(sampleRequest, studentKey);
                        return Ok(response);
                    }
                    return BadRequest("Student Key in the request body does not match with the StudentKey from the URL");
                }
                var modelErrors = ModelState.AllErrors();
                return BadRequest(modelErrors);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete]
        [Route("students/{studentKey}")]
        public IActionResult DeleteStudent([FromRoute]long studentKey)
        {
            try
            {
                var response = _studentOrchestration.DeleteStudent(studentKey);
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
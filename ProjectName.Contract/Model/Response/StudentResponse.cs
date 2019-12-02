
namespace ProjectName.Contract.Model.Response
{
    public class StudentResponse : BaseResponse
    {
        // Properties which we need to send as a response to the requested client
        public long StudentKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Standard { get; set; }
    }
}
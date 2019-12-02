using Newtonsoft.Json;

namespace ProjectName.Contract.Model.Response
{
    public class BaseResponse
    {
        [JsonIgnore]
        public string Message { get; set; } // Common properties for all the response models
        [JsonIgnore]
        public int Code { get; set; }

        // Property which we don't need as part of the response but we use in our application flow
        [JsonIgnore]
        public bool IsSuccess { get; set; }
    }
}
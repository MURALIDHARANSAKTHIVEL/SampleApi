namespace ProjectName.Contract.Error
{
    public class ModelStateError
    {
        public string Key { get; set; }
        public string Message { get; set; }

        public ModelStateError(string key, string message)
        {
            this.Key = key;
            this.Message = message;

        }
        public ModelStateError()
        {

        }
    }
}
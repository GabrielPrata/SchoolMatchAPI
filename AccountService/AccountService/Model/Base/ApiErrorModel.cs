namespace AccountService.Model.Base
{
    public sealed class ApiErrorModel
    {

        public ApiErrorModel()
        {
        }

        public ApiErrorModel(string message, string stackTrace)
        {

            Message = message;
            StackTrace = stackTrace;

        }

        public ApiErrorModel(string message)
        {

            Message = message;

        }


        public string Message { get; set; } = string.Empty;
        public string StackTrace { get; set; }

    }
}

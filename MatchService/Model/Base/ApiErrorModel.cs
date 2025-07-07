namespace MatchService.Model.Base
{
    public sealed class ApiErrorModel
    {

        public ApiErrorModel()
        {
        }
        public ApiErrorModel(string message, int statusCode)
        {

            Message = message;
            StatusCode = statusCode;

        }
        public ApiErrorModel(string message, string stackTrace)
        {

            Message = message;
            StackTrace = stackTrace;

        }

        public ApiErrorModel(string message, int statusCode, string stackTrace)
        {

            Message = message;
            StackTrace = stackTrace;
            StatusCode = statusCode;

        }

        public ApiErrorModel(string message)
        {

            Message = message;

        }


        public string Message { get; set; } = string.Empty;
        public string StackTrace { get; set; }
        public int StatusCode { get; set; }

    }
}

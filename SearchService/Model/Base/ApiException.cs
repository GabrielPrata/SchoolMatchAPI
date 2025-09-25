namespace SearchService.Model.Base
{
    public class ApiException : ArgumentException
    {
        public ApiErrorModel ErrorModel { get; }

        public ApiException(ApiErrorModel errorModel)
            : base(errorModel.Message)
        {
            this.ErrorModel = errorModel;
        }
    }
}

using System.Net;

namespace Core.Dtos.BaseControllerResponse
{
    public class ErrorDataResponse : CustomApiResponse
    {
        public ErrorDataResponse(List<string> errorMessages, HttpStatusCode httpStatusCode)
        {
            IsSuccessful = false;
            ErrorMessageList = errorMessages;
            HttpStatusCode = (int)httpStatusCode;
        }
    }
}

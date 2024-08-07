using System.Net;

namespace Core.Dtos.BaseControllerResponse
{
    public class SuccessDataResponse : CustomApiResponse
    {
        public SuccessDataResponse()
        {
            IsSuccessful = true;
        }

        public SuccessDataResponse(HttpStatusCode httpStatusCode)
        {
            HttpStatusCode = (int)httpStatusCode;
            IsSuccessful = true;
        }
    }
}

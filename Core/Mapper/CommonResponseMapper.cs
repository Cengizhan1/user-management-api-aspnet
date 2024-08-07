using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Core.Dtos.BaseControllerResponse;

namespace Core.Mapper
{
    public static class CommonResponseMapper
    {


        public static CustomApiResponse ToCustomApiResponse(bool isSuccessful, HttpStatusCode httpStatusCode = HttpStatusCode.OK, List<string> errorMessageList = null)
        {
            errorMessageList ??= new List<string>();

            return new CustomApiResponse
            {
                HttpStatusCode = (int)httpStatusCode,
                IsSuccessful = isSuccessful,

                ErrorMessageList = errorMessageList,
            };

        }

        public static CustomDataResponse<T> ToCustomDataResponse<T>(this T request, bool isSuccessful, HttpStatusCode httpStatusCode = HttpStatusCode.OK,
            List<string> errorMessageList = null)
        {
            errorMessageList ??= new List<string>();

            return new CustomDataResponse<T>
            {
                Data = request,
                DataList = new(),
                HttpStatusCode = (int)httpStatusCode,
                IsSuccessful = isSuccessful,
                ErrorMessageList = errorMessageList,
            };

        }
    }
}

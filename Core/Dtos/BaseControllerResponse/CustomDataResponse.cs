namespace Core.Dtos.BaseControllerResponse
{
    public class CustomDataResponse<T> : CustomApiResponse
    {
        public T Data { get; set; }
        public List<T> DataList { get; set; }
    }
}

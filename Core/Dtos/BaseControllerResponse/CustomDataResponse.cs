namespace Core.Dtos.BaseControllerResponse
{
    public class CustomDataResponse<T> : CustomApiResponse
    {
        public T Data { get; set; }
        public IEnumerable<T> DataList { get; set; }
    }
}

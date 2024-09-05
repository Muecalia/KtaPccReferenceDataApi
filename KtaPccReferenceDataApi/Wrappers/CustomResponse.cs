namespace TotalAgilityApi.Wrappers
{
    public class CustomResponse<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }

        public CustomResponse() { }

        public CustomResponse(T data, string message)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public CustomResponse(string message)
        {
            Succeeded = false;
            Message = message;
        }
    }
}

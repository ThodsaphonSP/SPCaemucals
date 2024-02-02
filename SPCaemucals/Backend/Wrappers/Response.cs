namespace SPCaemucals.Backend.Wrappers;

public class Response<T>
{
    public Response()
    {
    }

    public Response(T data)
    {
        Message = string.Empty;
        Errors = null;
        Data = data;
    }
    public T Data { get; set; }
    public string[] Errors { get; set; }
    public string Message { get; set; }
}

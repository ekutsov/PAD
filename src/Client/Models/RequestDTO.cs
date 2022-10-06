namespace PAD.Client.Models;

public class RequestDTO
{
    public string Path { get; set; }

    public HttpMethod Method { get; set; }

    public object Body { get; set; }
}
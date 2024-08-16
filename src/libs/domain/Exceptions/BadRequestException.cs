namespace NotionNotifications.Domain.Exceptions;

[Serializable]
public class BadRequestExceptionException : System.Exception
{
    public HttpResponseMessage Response { get; set; }
    public BadRequestExceptionException(HttpResponseMessage response) { 
        this.Response = response;
    }
    public BadRequestExceptionException(HttpResponseMessage response, string message) : base(message) {
        this.Response = response;
     }
    public BadRequestExceptionException(HttpResponseMessage response, string message, System.Exception inner) : base(message, inner) { 
        this.Response = response;
    }

    protected BadRequestExceptionException(
        HttpResponseMessage response,
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { 
            this.Response = response;
        }
}
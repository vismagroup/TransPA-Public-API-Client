using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace TransPA.OpenSource;

public class HttpObjectResultHelper
{
    public ObjectResult GetBadRequestResult(string detail)
    {
        return GetProblemDetailsObjectResult(HttpStatusCode.BadRequest, "Bad Request", detail);
    }
    
    private ObjectResult GetProblemDetailsObjectResult(HttpStatusCode statusCode, string title, string detail)
    {
        return new ObjectResult(GetProblemDetails(statusCode, title, detail))
        {
            StatusCode = (int)statusCode,
            ContentTypes = new MediaTypeCollection { new MediaTypeHeaderValue("application/problem+json") }
        };
    }
    
    private ProblemDetails GetProblemDetails(HttpStatusCode status, string title, string detail)
    {
        return new ProblemDetails
        {
            Title = title,
            Status = (int)status,
            Detail = detail
        };
    }
}
using System.Net;

namespace Blog.API.Models
{
    public class DomainException : Exception
    {
        public HttpStatusCode StatusCode;
        public List<string> Errors;

        public DomainException(List<string> errors, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            Errors = errors;
            StatusCode = statusCode;
        }

        public DomainException(string error, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            Errors = new List<string> { error };
            StatusCode = statusCode;
        }
    }
}

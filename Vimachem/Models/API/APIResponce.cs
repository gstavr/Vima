using System.Net;

namespace Vimachem.Models.API
{
    public class APIResponce
    {
        public HttpStatusCode? StatusCode { get; set; } = HttpStatusCode.OK;
        public bool IsSuccess { get; set; } = true;
        public List<string>? ErrorMessages { get; set; }
        public object? Result { get; set; }
    }
}

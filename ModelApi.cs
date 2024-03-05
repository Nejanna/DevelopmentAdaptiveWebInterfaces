
using System.Collections.Generic;
using System.Net;

namespace Lab3
{
    class ModelApi<T>
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<T> Information { get; set; }
    }
}

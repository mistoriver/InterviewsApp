using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewsApp.Core.Models
{
    public class Response
    {
        public string ErrorMessage { get; set; }

        public bool Ok => string.IsNullOrWhiteSpace(ErrorMessage);
        public Response (string errorMessage = null)
        {
            ErrorMessage = errorMessage;
        }
    }

    public class Response<TData> :Response
    {
        public TData ResponseData { get; private set; }

        public Response(TData entity, string errorMessage = null) :base (errorMessage)
        {
            ResponseData = entity;
        }
        public Response(string errorMessage) : base(errorMessage)
        {
            ResponseData = default(TData);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JeniesStory.Application.Dtos.Responses
{
    public class GenericResponse<T>
    {        
            public T Data { get; set; }

            public string Message { get; set; }

            public HttpStatusCode ResponseCode { get; set; }

            public bool Status { get; set; }


            public static GenericResponse<T> Fail(string errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            {
                return new GenericResponse<T> { Status = false, Message = errorMessage, ResponseCode = statusCode };
            }
            public static GenericResponse<T> Success(string successMessage, T data, HttpStatusCode statusCode = HttpStatusCode.OK)
            {
                return new GenericResponse<T> { Status = true, Message = successMessage, Data = data, ResponseCode = statusCode };
            }        
    }
}
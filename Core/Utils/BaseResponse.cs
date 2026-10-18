using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils;
public class BaseResponse<T>
{
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }

    public static BaseResponse<T> Success(string message = "Success")
    {
        return new BaseResponse<T> 
        { 
            StatusCode = HttpStatusCode.OK,
            IsSuccess = true,
            Message = message,
            Data = default
        };
    }

    public static BaseResponse<T> Success(T data, string message = "Success")
    {
        return new BaseResponse<T>
        {
            StatusCode = HttpStatusCode.OK,
            IsSuccess = true,
            Message = message,
            Data = data
        };
    }

   // public static BaseResponse<T> Fail(string message )
}

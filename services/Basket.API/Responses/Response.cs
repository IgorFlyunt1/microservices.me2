using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Responses;

public class ApiResponse<T>
{
    public T Data { get; set; }
    public bool Succeeded { get; set; }

    public ErrorResponse ErrorResponse { get; set; }

    public ApiResponse<T> Failed(ErrorResponse errorResponse)
    {
        return new ApiResponse<T>
        {
            Succeeded = false,
            ErrorResponse = errorResponse
        };
    }

    public ApiResponse<T> Success(T data)
    {
        return new ApiResponse<T>
        {
            Succeeded = true,
            Data = data
        };
    }
}
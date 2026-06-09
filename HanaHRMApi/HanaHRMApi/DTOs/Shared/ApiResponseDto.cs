namespace HanaHRMApi.DTOs.Shared;

public class ApiResponseDto<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = null!;
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }

    public static ApiResponseDto<T> SuccessResponse(T data, string message = "Operation completed successfully")
    {
        return new ApiResponseDto<T>
        {
            Success = true,
            Message = message,
            Data = data,
            Errors = null
        };
    }

    public static ApiResponseDto<T> Failure(string message, IEnumerable<string>? errors = null)
    {
        return new ApiResponseDto<T>
        {
            Success = false,
            Message = message,
            Data = default,
            Errors = errors?.ToList() ?? new List<string>()
        };
    }
}

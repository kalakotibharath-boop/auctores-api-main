namespace AuctoresOnline.API.Models.Common;

public record ServiceResult<T>(bool Success, T? Data, string? Message, int StatusCode = 200)
{
    public static ServiceResult<T> Ok(T data, string? message = null) => new(true, data, message);
    public static ServiceResult<T> Fail(string message, int statusCode = 400) => new(false, default, message, statusCode);
    public static ServiceResult<T> NotFound(string message) => new(false, default, message, 404);
    public static ServiceResult<T> Conflict(string message) => new(false, default, message, 409);
    public static ServiceResult<T> Unauthorized(string message) => new(false, default, message, 401);
}

public record ServiceResult(bool Success, string? Message, int StatusCode = 200)
{
    public static ServiceResult Ok(string? message = null) => new(true, message);
    public static ServiceResult Fail(string message, int statusCode = 400) => new(false, message, statusCode);
    public static ServiceResult NotFound(string message) => new(false, message, 404);
    public static ServiceResult Conflict(string message) => new(false, message, 409);
}

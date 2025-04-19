namespace AnimeMovies.Domain.Shared;

public class Result<T>
{
    public int StatusCode { get; set; }
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public Error? Error { get; set; }
    
    private Result(int statusCode, bool isSuccess, T data, Error? error)
    {
        StatusCode = statusCode;
        IsSuccess = isSuccess;
        Data = data;
        Error = error;
    }
    
    public static Result<T> Success(T data) => new (200, true, data, Error.None);

    public static Result<T?> Failure(Error error)
    {
        var statusCode = error.Message switch
        {
            "Validation Failed" => StatusCodes.BadRequest,
            "Not Found" => StatusCodes.NotFound,
            "Internal Server Error" => StatusCodes.InternalServerError
        };
        
        return new (statusCode, false, default, error);
    }
}
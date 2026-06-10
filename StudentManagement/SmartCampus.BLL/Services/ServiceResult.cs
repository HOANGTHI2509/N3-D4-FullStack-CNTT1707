namespace SmartCampus.BLL.Services;

public enum ServiceResultType
{
    Success,
    NotFound,
    Conflict
}

public class ServiceResult<T>
{
    private ServiceResult(ServiceResultType type, string message, T? data)
    {
        Type = type;
        Message = message;
        Data = data;
    }

    public ServiceResultType Type { get; }

    public string Message { get; }

    public T? Data { get; }

    public static ServiceResult<T> Success(T data, string message)
    {
        return new ServiceResult<T>(ServiceResultType.Success, message, data);
    }

    public static ServiceResult<T> NotFound(string message)
    {
        return new ServiceResult<T>(ServiceResultType.NotFound, message, default);
    }

    public static ServiceResult<T> Conflict(string message)
    {
        return new ServiceResult<T>(ServiceResultType.Conflict, message, default);
    }
}

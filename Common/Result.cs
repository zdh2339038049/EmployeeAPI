namespace EmployeeApi.Common
{
    public class Result
    {
        public int Code { get; set; }
        public string Message { get; set; } = string.Empty;
        public static Result Ok(string message = "成功")
        {
            return new Result { Code = 200, Message = message };
        }
        public static Result<T> Ok<T>(T data, string message = "操作成功")
        {
            return new Result<T> { Code = 200, Message = message, Data = data };
        }
        public static Result Fail(int code, string message)
        {
            return new Result { Code = code, Message = message };
        }
        public static Result<T> Fail<T>(int code, string message)
    => new() { Code = code, Message = message };

    }
    public class Result<T> 
    {
        public int Code { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    }
}

namespace HR.Common.Results
{
    public class ServiceResult<T> : BaseServiceResult<T>
    {
        public ServiceResult()
        { }

        public ServiceResult(IEnumerable<string> errors) : base(errors)
        { }

        public ServiceResult(string error) : base(error)
        { }

        public ServiceResult(bool isSuccess) : base(isSuccess)
        { }

        public ServiceResult(bool isSuccess, T result) : base(isSuccess, result)
        { }

        public ServiceResult(bool isSuccess, T result, IEnumerable<string> errors)
            : base(isSuccess, result, errors)
        { }

        public static ServiceResult<TResult> Change<TResult>(ServiceResult serviceResult)
            => new ServiceResult<TResult>(serviceResult.IsSuccess, (TResult)serviceResult.Result, serviceResult.Errors);
    }

    public class ServiceResult : BaseServiceResult<object>
    {
        public ServiceResult()
        { }

        public ServiceResult(IEnumerable<string> errors) : base(errors)
        { }

        public ServiceResult(string error) : base(error)
        { }

        public ServiceResult(bool isSuccess) : base(isSuccess)
        { }

        public ServiceResult(bool isSuccess, object result) : base(isSuccess, result)
        { }

        public ServiceResult(bool isSuccess, object result, IEnumerable<string> errors)
            : base(isSuccess, result, errors)
        { }

        public static ServiceResult Change<TResult>(ServiceResult<TResult> serviceResult)
            => new ServiceResult(serviceResult.IsSuccess, serviceResult.Result, serviceResult.Errors);
    }
}
namespace HR.Common.Results
{
    public abstract class BaseServiceResult<T>
    {
        public bool IsSuccess { get; set; }
        public T Result { get; set; }
        public IEnumerable<string> Errors { get; set; }

        protected BaseServiceResult() => Errors = new List<string>();

        protected BaseServiceResult(bool isSuccess) : this(isSuccess, default) { }

        protected BaseServiceResult(bool isSuccess, T result) : this(isSuccess, result, null) { }

        protected BaseServiceResult(string error) : this(false, default, new[] { error }) { }

        protected BaseServiceResult(IEnumerable<string> errors) : this(false, default, errors) { }

        protected BaseServiceResult(bool isSuccess, T result, IEnumerable<string> errors) : this()
        {
            IsSuccess = isSuccess;
            Result = result;
            Errors = errors ?? new List<string>();
        }
    }
}
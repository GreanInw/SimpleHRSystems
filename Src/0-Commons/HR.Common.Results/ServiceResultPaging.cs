namespace HR.Common.Results
{
    public class ServiceResultPaging<T> : ServiceResult<IEnumerable<T>>
    {
        public int Limit { get; set; }
        public int PageNumber { get; set; }

        public ServiceResultPaging() { }

        public ServiceResultPaging(IEnumerable<string> errors) : base(errors) { }

        public ServiceResultPaging(string error) : base(error) { }

        public ServiceResultPaging(bool isSuccess) : base(isSuccess) { }

        public ServiceResultPaging(bool isSuccess, IEnumerable<T> result) : base(isSuccess, result) { }

        public ServiceResultPaging(bool isSuccess, IEnumerable<T> result, IEnumerable<string> errors)
            : base(isSuccess, result, errors) { }
    }
}
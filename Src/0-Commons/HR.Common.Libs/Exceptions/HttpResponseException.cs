namespace HR.Common.Libs.Exceptions
{
    /// <summary>
    /// Http response eception
    /// </summary>
    public class HttpResponseException : Exception
    {
        /// <summary>
        /// Status
        /// </summary>
        public int Status { get; set; } = 500;
        /// <summary>
        /// Value of exception
        /// </summary>
        public object Value { get; set; }
    }
}

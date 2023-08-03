using HR.Common.Libs.Jsons;
using HR.Common.Results;
using System.Text.Json.Serialization;

namespace HR.Common.Libs.Helpers
{
    public class CommonHelpers
    {
        /// <summary>
        /// Get next page number 
        /// </summary>
        /// <param name="totalRow">The total row of result.</param>
        /// <param name="limit">The limit row</param>
        /// <param name="pageNumber">The current page number for get next page number.</param>
        /// <returns></returns>
        public static int GetNextPageNumber(int totalRow, int limit, int pageNumber)
        {
            return totalRow < limit ? -1 : pageNumber + 1;
        }

        /// <summary>
        /// Get type of variable.
        /// </summary>
        /// <param name="sourceType">The source type.</param>
        /// <returns>
        /// Return <see cref="Type"/>
        /// </returns>
        public static Type GetVariableType(Type sourceType)
            => sourceType.IsArray ? sourceType.GetElementType()
                : (sourceType.GenericTypeArguments.Length == 0
                    ? sourceType : sourceType.GenericTypeArguments[0]);

        /// <summary>
        /// Compare type of <paramref name="value"/> and <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type for compare.</typeparam>
        /// <param name="value">The value for compare.</param>
        /// <returns></returns>
        public static bool IsOfType<T>(object value)
            => value is T;
    }
}

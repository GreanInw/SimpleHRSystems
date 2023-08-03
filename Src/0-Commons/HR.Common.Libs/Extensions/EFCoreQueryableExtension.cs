using System.Linq.Expressions;

namespace HR.Common.Libs.Extensions
{
    public static class EFCoreQueryableExtension
    {
        /// <summary>
        /// Get data with page and page size
        /// </summary>
        /// <typeparam name="T">The type of entity</typeparam>
        /// <param name="list">Source data</param>
        /// <param name="limit">The limit of rows.</param>
        /// <param name="pageNumber">The page number for get next data.</param>
        /// <returns></returns>
        public static IQueryable<T> Paging<T>(this IQueryable<T> list, int limit, int pageNumber)
        {
            // A page size cannot be less than or equal to 0.
            if (limit < 1)
            {
                limit = 50;  // Set to a default value.
            }
            // Clamp a page number because when it's converted into a page index, an index cannot be less than 0.
            if (pageNumber < 1)
            {
                pageNumber = 1;  // Set to a default value.
            }
            var pageIndex = pageNumber - 1;
            var skip = limit * pageIndex;
            return list.Skip(skip).Take(limit);
        }

        /// <summary>
        /// Order data with descending and ascending.
        /// </summary>
        /// <typeparam name="T">Type of data</typeparam>
        /// <typeparam name="TKey">Key for order</typeparam>
        /// <param name="list">Source data</param>
        /// <param name="orderExpression">Expression for order by.</param>
        /// <param name="isDescendingOrder">If true is descending, else ascending.</param>
        /// <returns></returns>
        public static IQueryable<T> Order<T, TKey>(this IQueryable<T> list, Expression<Func<T, TKey>> orderExpression
            , bool isDescendingOrder = false)
            => isDescendingOrder ? list.OrderByDescending(orderExpression) : list.OrderBy(orderExpression);
    }
}

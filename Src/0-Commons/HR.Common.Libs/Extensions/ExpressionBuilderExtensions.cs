using System.Linq.Expressions;

namespace HR.Common.Libs.Extensions
{
    public static class ExpressionBuilderExtensions
    {
        /// <summary>
        /// Set true of type.
        /// </summary>
        /// <typeparam name="T">Type for build expression</typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        /// <summary>
        /// Set false of type
        /// </summary>
        /// <typeparam name="T">Type for build expression</typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        /// <summary>
        /// Build 'Or' operation with expression.
        /// </summary>
        /// <typeparam name="T">Type of expression</typeparam>
        /// <param name="expr1">Expression for build 'Or' operation</param>
        /// <param name="expr2">expression for build 'Or' operation</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        /// <summary>
        /// Build 'And' operation with expression.
        /// </summary>
        /// <typeparam name="T">Type of expression</typeparam>
        /// <param name="expr1">Expression for build 'And' operation</param>
        /// <param name="expr2">expression for build 'And' operation</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }
}

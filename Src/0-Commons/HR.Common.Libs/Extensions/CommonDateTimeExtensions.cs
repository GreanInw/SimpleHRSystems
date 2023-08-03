using HR.Common.Constants;

namespace HR.Common.Libs.Extensions
{
    public static class CommonDateTimeExtensions
    {
        /// <summary>
        /// Convert <see cref="DateTime"/> to <see cref="string"/> format : yyyy-MM-dd HH:mm:ss.fffff. 
        /// Use culture : <see cref="System.Globalization.CultureInfo"/> of en-US.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToUSFullDateTimeString(this DateTime date)
            => date.ToString("yyyy-MM-dd HH:mm:ss.fffff", DefaultDataConstants.USCulture);

        /// <summary>
        /// Convert <see cref="DateTime"/> to <see cref="string"/> format : yyyy-MM-dd.
        /// Use culture : <see cref="System.Globalization.CultureInfo"/> of en-US.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToUSDateString(this DateTime date)
            => date.ToString("yyyy-MM-dd", DefaultDataConstants.USCulture);

        /// <summary>
        /// Convert <see cref="DateTime"/> to <see cref="string"/> format : yyyy-MM-dd.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToDateString(this DateTime date)
            => date.ToString("yyyy-MM-dd");

        /// <summary>
        /// Convert <see cref="DateTime"/> from request. 
        /// <see cref="DateTime"/> from request <see cref="DateTime.Year"/> -543
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime ToRawUSDateTimeFromRequest(this DateTime date)
            => DateTime.Parse(date.ToDateString(), DefaultDataConstants.USCulture);

    }
}

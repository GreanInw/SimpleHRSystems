using HR.Common.Libs.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace HR.Common.Libs.Extensions
{
    public static class CommonExtensions
    {
        /// <summary>
        /// Validate object is null. If true is null, false not null
        /// </summary>
        /// <param name="value">Object for validate is null</param>
        /// <returns>true, false</returns>
        public static bool IsNullable(this object value)
        {
            return value == null;
        }

        /// <summary>
        /// Validate string is null or empty. If true is null/emtpy. false is not null/not empty
        /// </summary>
        /// <param name="value">string value</param>
        /// <returns>true, false</returns>
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Validate guid is empty. If true is empty, false not empty.
        /// Empty guid value : '00000000-0000-0000-0000-000000000000'
        /// </summary>
        /// <param name="value">Guid data</param>
        /// <returns>true, false</returns>
        public static bool IsEmpty(this Guid value)
        {
            return Guid.Empty == value;
        }

        /// <summary>
        /// Convert string to Guid.
        /// </summary>
        /// <param name="value">string of guid</param>
        /// <returns></returns>
        public static Guid ToGuid(this string value)
        {
            if (value.IsEmpty())
            {
                throw new ArgumentNullException(nameof(value));
            }
            return Guid.Parse(value);
        }

        /// <summary>
        /// Convert string to guid.
        /// </summary>
        /// <param name="value">Value for convert to Guid</param>
        /// <returns></returns>
        public static Guid? ToTryGuid(this string value)
        {
            Guid newId;
            bool isConvert = Guid.TryParse(value, out newId);
            return isConvert ? newId : new Guid?();
        }

        /// <summary>
        /// Get name from <see cref="JsonPropertyNameAttribute"/> with property name.
        /// </summary>
        /// <typeparam name="T">Class</typeparam>
        /// <param name="source">Class</param>
        /// <param name="propertyName">Property name</param>
        /// <returns></returns>
        public static string GetNameFromJsonPropertyNameAttribute(this object source, string propertyName)
        {
            if (propertyName.IsEmpty())
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            var propertyInfo = source.GetType().GetProperties().FirstOrDefault(f => f.Name == propertyName);
            if (propertyInfo.IsNullable())
            {
                return propertyName;
            }

            var customAttr = propertyInfo.GetCustomAttribute<JsonPropertyNameAttribute>();
            return customAttr?.Name;
        }

        /// <summary>
        /// Get name from <see cref="JsonPropertyNameAttribute"/> with property info.
        /// </summary>
        /// <typeparam name="T">Class</typeparam>
        /// <param name="entity">Class</param>
        /// <param name="propertyInfo">Property info</param>
        /// <returns></returns>
        public static string GetNameFromJsonPropertyNameAttribute(this object source, PropertyInfo propertyInfo)
            => GetNameFromJsonPropertyNameAttribute(source, propertyInfo.Name);

        /// <summary>
        /// Convert string to enum case-sensitive
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="value">Value for convert to enum</param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value) where T : struct
            => value.ToEnum<T>(false);

        /// <summary>
        /// Convert object to enum case-sensitive
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="value">Value for convert to enum</param>
        /// <returns></returns>
        public static T ToEnum<T>(this object value) where T : struct
            => value.ToString().ToEnum<T>(false);

        /// <summary>
        /// Convert string to enum case-insensitive.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="value">Value for convert to enum</param>
        /// <param name="ignoreCase">true to ignore case; false to regard case.</param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value, bool ignoreCase) where T : struct
        {
            if (value.IsEmpty())
            {
                throw new ArgumentNullException(nameof(value));
            }
            return Enum.Parse<T>(value, ignoreCase);
        }

        /// <summary>
        /// Convert object to enum case-insensitive.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="value">Value for convert to enum</param>
        /// <param name="ignoreCase">true to ignore case; false to regard case.</param>
        /// <returns></returns>
        public static T ToEnum<T>(this object value, bool ignoreCase) where T : struct
            => value.ToString().ToEnum<T>(ignoreCase);

        /// <summary>
        /// Convert string to enum case-sensitive. Return value is nullable.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="value">Value for convert to enum</param>
        /// <returns></returns>
        public static T? ToTryEnum<T>(this string value) where T : struct
        {
            return value.ToTryEnum<T>(false);
        }

        /// <summary>
        /// Convert object to enum case-sensitive. Return value is nullable.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="value">Value for convert to enum</param>
        /// <returns></returns>
        public static T? ToTryEnum<T>(this object value) where T : struct
            => value.ToString().ToTryEnum<T>();

        /// <summary>
        /// Convert string to enum case-insensitive. Return value is nullable.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="value">Value for convert to enum</param>
        /// <param name="ignoreCase">true to ignore case; false to regard case.</param>
        /// <returns></returns>
        public static T? ToTryEnum<T>(this string value, bool ignoreCase) where T : struct
        {
            T newValue;
            bool isConvert = Enum.TryParse<T>(value, ignoreCase, out newValue);
            return isConvert ? newValue : new T?();
        }

        /// <summary>
        /// Convert object to enum case-insensitive. Return value is nullable.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="value">Value for convert to enum</param>
        /// <param name="ignoreCase">true to ignore case; false to regard case.</param>
        /// <returns></returns>
        public static T? ToTryEnum<T>(this object value, bool ignoreCase) where T : struct
            => value.ToString().ToTryEnum<T>(ignoreCase);

        /// <summary>
        /// Replace strign value to snake case
        /// </summary>
        /// <param name="source">Source for replace</param>
        /// <returns></returns>
        public static string ToSnakeCase(this string source)
            => Regex.Replace(source, @"(\w)([A-Z])", "$1_$2").ToLower();

        /// <summary>
        /// Encode plain text to base64
        /// </summary>
        /// <param name="value">Plain text for encode</param>
        /// <returns></returns>
        public static string ToBase64Encode(this string value)
            => Convert.ToBase64String(Encoding.UTF8.GetBytes(value));

        /// <summary>
        /// Decode plain text to base64
        /// </summary>
        /// <param name="value">Format of base64</param>
        /// <returns></returns>
        public static string ToBase64Decode(this string value)
            => Encoding.UTF8.GetString(Convert.FromBase64String(value));

        /// <summary>
        /// Validate environment for worker on local. If true is local, false is not local.
        /// </summary>
        /// <param name="hostEnvironment">IHostEnvironment</param>
        /// <returns></returns>
        public static bool IsLocal(this IHostEnvironment hostEnvironment)
            => hostEnvironment.EnvironmentName.ToLower() == "local";

        /// <summary>
        /// Validation type is collection. 
        /// Support type : <see cref="IEnumerable"/>, <see cref="ICollection"/>, <see cref="IList"/>, 
        /// <see cref="IEnumerable{T}"/>, <see cref="ICollection{T}"/>, <see cref="IList{T}"/>
        /// </summary>
        /// <param name="type">The type for validation.</param>
        /// <returns></returns>
        public static bool IsCollectionType(this Type type)
        {
            var collectionTypes = new[]
            {
                typeof(ICollection<>), typeof(IList<>), typeof(IEnumerable<>),
                typeof(ICollection), typeof(IList), typeof(IEnumerable)
            };
            return type.GetInterfaces().Any(t => collectionTypes.Contains(t));
        }

        /// <summary>
        /// Convert object to <seealso cref="DataTable"/>.
        /// </summary>
        /// <typeparam name="T">The type of data.</typeparam>
        /// <param name="data">The data for convert to DataTable.</param>
        /// <param name="tableName">The name of table.</param>
        /// <returns>
        /// Return data type is <see cref="DataTable"/>
        /// </returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static DataTable ToDataTable<T>(this IEnumerable<T> data, string tableName = "") where T : class
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var properties = typeof(T).GetProperties();
            var results = new DataTable(tableName ?? typeof(T).Name);

            //Create columns.
            results.Columns.AddRange(properties.Select(s => new DataColumn(s.Name, typeof(object))).ToArray());

            foreach (var row in data)
            {
                var newRow = results.NewRow();
                foreach (var property in properties)
                {
                    newRow[property.Name] = property.GetValue(row);
                }
                results.Rows.Add(newRow);
            }

            return results;
        }

        /// <summary>
        /// Get name of <see cref="JsonPropertyNameAttribute"/> or
        /// <see cref="FromFormAttribute"/> or <see cref="FromQueryAttribute"/>.
        /// </summary>
        /// <typeparam name="TAttribute">
        /// Attribute type of <see cref="JsonPropertyNameAttribute"/> or 
        /// <see cref="FromFormAttribute"/> or <see cref="FromQueryAttribute"/>.
        /// </typeparam>
        /// <param name="propertyInfo">The property info for get name</param>
        /// <returns></returns>
        public static string GetNameFromContentTypeAttribute<TAttribute>(this PropertyInfo propertyInfo)
            where TAttribute : Attribute
        {
            var attr = propertyInfo.GetCustomAttribute<TAttribute>();
            if (attr.IsNullable())
            {
                return string.Empty;
            }

            if (attr is JsonPropertyNameAttribute jsonPropertyName)
            {
                return jsonPropertyName.Name;
            }
            else if (attr is FromFormAttribute formData)
            {
                return formData.Name;
            }
            else if (attr is FromQueryAttribute query)
            {
                return query.Name;
            }
            else if (attr is Newtonsoft.Json.JsonPropertyAttribute jsonProperty)
            {
                return jsonProperty.PropertyName;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Convert object to <see cref="IDictionary{TKey, TValue}"/>
        /// </summary>
        /// <param name="source">The source data</param>
        /// <returns></returns>
        public static IDictionary<string, object> ToDictionary(this object source)
            => source.ToDictionary<object>();

        /// <summary>
        /// Convert object to <see cref="IDictionary{TKey, TValue}"/>
        /// </summary>
        /// <typeparam name="T">Type for convert</typeparam>
        /// <param name="source">The source data</param>
        /// <returns></returns>
        public static IDictionary<string, T> ToDictionary<T>(this object source)
        {
            if (source.IsNullable())
            {
                throw new NullReferenceException("Unable to convert anonymous object to a dictionary. The source anonymous object is null.");
            }

            var dictionary = new Dictionary<string, T>();
            foreach (var property in source.GetType().GetProperties())
            {
                object value = property.GetValue(source);
                if (CommonHelpers.IsOfType<T>(value))
                {
                    dictionary.Add(property.Name, (T)value);
                }
            }
            return dictionary;
        }

        /// <summary>
        /// Convert object to <see cref="IDictionary{TKey, TValue}"/> 
        /// and get name from <see cref="JsonPropertyNameAttribute"/> or
        /// <see cref="FromFormAttribute"/> or <see cref="FromQueryAttribute"/>.
        /// </summary>
        /// <typeparam name="TReturn">Type of value</typeparam>
        /// <typeparam name="TAttribute">
        /// Attribute type of <see cref="JsonPropertyNameAttribute"/> or 
        /// <see cref="FromFormAttribute"/> or <see cref="FromQueryAttribute"/>.
        /// </typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IDictionary<string, TReturn> ToDictionary<TReturn, TAttribute>(this object source)
            where TAttribute : Attribute
        {
            if (source.IsNullable())
            {
                throw new NullReferenceException("Unable to convert anonymous object to a dictionary. The source anonymous object is null.");
            }
            var realTypes = new Type[]
            {
                typeof(int), typeof(long), typeof(short), typeof(decimal), typeof(float), typeof(double), typeof(bool),
                typeof(int?), typeof(long?), typeof(short?), typeof(decimal?), typeof(float?), typeof(double?), typeof(bool?)
            };

            var dictionary = new Dictionary<string, TReturn>();
            foreach (var property in source.GetType().GetProperties())
            {
                object valueProperty = property.GetValue(source);
                object newValue = null;

                string attrName = property.GetNameFromContentTypeAttribute<TAttribute>();
                if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                {
                    newValue = valueProperty.IsNullable() ? null : Convert.ToDateTime(valueProperty).ToUSFullDateTimeString();
                }
                else if (realTypes.Any(t => t == property.PropertyType))
                {
                    newValue = valueProperty.ToString();
                }
                else
                {
                    newValue = (TReturn)valueProperty;
                }
                dictionary.Add(attrName.IsEmpty() ? property.Name : attrName, (TReturn)newValue);
            }
            return dictionary;
        }

        /// <summary>
        /// Validation <paramref name="data"/> is null or empty(count = 0).
        /// If <see langword="true" /> is null or empty, 
        /// but if <see langword="false" /> is not null and not empty.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="data">The data for validation.</param>
        /// <returns></returns>
        public static bool IsNullableOrEmpty<T>(this IEnumerable<T> data)
        {
            if (data.IsNullable())
            {
                return true;
            }

            return !data.Any();
        }
    }
}

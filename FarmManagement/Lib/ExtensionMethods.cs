using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using AutoMapper;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Reflection;

namespace FarmManagement.Lib
{
    public static class ExtensionMethods
    {
        public static string RemoveSpecialChars(this string requestedString)
        {
            if (string.IsNullOrEmpty(requestedString))
                return requestedString;

            var symbols = new[] { ".", "/", "!", "@", "$", "%", "^", "&amp;", "*", "'", "\"", ";", "_", "(", ")", ":", "|", "[", "]" };
            return symbols.Aggregate(requestedString, (current, symbol) => current.Contains(symbol) ? current.Replace(symbol, String.Empty) : current);
        }

        public static string RemoveNewLineChars(this string requestedString)
        {
            if (string.IsNullOrEmpty(requestedString))
                return requestedString;

            return requestedString.Replace('\r', ' ').Replace('\n', ' ');
        }

        public static List<DateTime> GetDatesOfMonth(Int32 year, Int32 month)
        {
            return Enumerable.Range(1, DateTime.DaysInMonth(year, month)).Select(day => new DateTime(year, month, day)).ToList();
        }

        public static SelectList ToSelectList<TEnum>(bool isAddDefaultValue = true)
        {
            var values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Select(x => new { Name = x.GetDescription(), Id = "" + Convert.ToInt32(x) }).ToList();

            if (isAddDefaultValue)
            {
                values.Add(new { Name = "Select", Id = "" });
            }

            values = values.OrderBy(x => x.Id).ToList();

            return new SelectList(values, "Id", "Name");
        }

        public static string ParseIntegerToEnum<T>(this string value)
        {
            if (value == null || value == "")
            {
                return null;
            }

            return ((T)Enum.ToObject(typeof(T), Convert.ToInt32(value))).ToString();
        }

        public static T? ParseEnum<T>(this string value) where T : struct
        {
            if (value == null || value == string.Empty)
            {
                return null;
            }
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static String ToNullIfEmptyEnum<T>(this object value)
        {
            if (value == null)
            {
                return null;
            }
            else
            {
                return ((T)value).ToString();
            }
        }

        public static string ToEmptyStringIsNull(this string value)
        {
            return value ?? string.Empty;
        }

        public static Int32 ToInt32(this string value, Int32 defaultValue = 0)
        {
            var returnValue = defaultValue;
            if (!string.IsNullOrEmpty(value))
            {
                returnValue = Convert.ToInt32(value);
            }

            return returnValue;
        }

        public static string GetDescription(this object value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attribute = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static DateTime GetDefaultDate(this DateTime date)
        {
            return date == DateTime.MinValue ? DateTime.Now : date;
        }

        public static void EnusreDirectoryExist(this string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static TDest ToType<TSource, TDest>(this TSource obj) where TDest : class
        {
            if (obj == null)
                return null;

            Mapper.CreateMap<TSource, TDest>();
            return Mapper.Map<TSource, TDest>(obj);
        }

        public static string ToModelErrors(this IEnumerable<ModelError> modelErrors)
        {
            var errors = string.Empty;
            foreach (var modelError in modelErrors)
            {
                errors += string.Format("{0}<br/>", modelError.ErrorMessage);
            }
            return errors;
        }

        public static string ReadAllHtmlText(this string fileName)
        {
            string templateFolder = HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["ContentEmailBase"]);
            string completeFilePath = Path.Combine(templateFolder, fileName);

            if (!File.Exists(completeFilePath))
            {
                throw new Exception("File path doesn't exist.");
            }

            return File.ReadAllText(completeFilePath);
        }

        public static string RemoveHtmlTag(this string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        public static bool IsValidEmailAddress(this string email)
        {
            return Regex.IsMatch(email, @"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$");
        }

        public static string EncodeQueryString(this string queryString)
        {
            if (string.IsNullOrEmpty(queryString))
            {
                return string.Empty;
            }

            var newQueryString = string.Empty;
            var nameValues = HttpUtility.ParseQueryString(queryString);
            foreach (var key in nameValues.AllKeys)
            {
                newQueryString += string.Format("&{0}={1}", key, HttpUtility.UrlEncode(nameValues[key]));
            }

            newQueryString = newQueryString.Substring(1);
            return "?" + newQueryString;
        }

        public static string UrlEncode(this string value)
        {
            return HttpUtility.UrlEncode(value);
        }

        public static DataTable ToDataTable<T>(this List<T> iList)
        {
            DataTable dataTable = new DataTable();
            PropertyDescriptorCollection propertyDescriptorCollection = TypeDescriptor.GetProperties(typeof(T));
            for (int i = 0; i < propertyDescriptorCollection.Count; i++)
            {
                PropertyDescriptor propertyDescriptor = propertyDescriptorCollection[i];
                Type type = propertyDescriptor.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    type = Nullable.GetUnderlyingType(type);

                dataTable.Columns.Add(propertyDescriptor.Name, type);
            }
            object[] values = new object[propertyDescriptorCollection.Count];
            foreach (T iListItem in iList)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = propertyDescriptorCollection[i].GetValue(iListItem);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        public static string GetFileUploadPath(string fileUploadPath, string Id)
        {
            return Path.Combine(HttpContext.Current.Server.MapPath(String.Format("{0}/{1}/{2}", Constant.FileUploadPath, fileUploadPath, Id)));
        }
        public static void DeleteUploadFile(this string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TeamVoice.Api.Attributes;

namespace TeamVoice.Api.Interfaces
{
    public class IModel
    {
    }

    public static class IModelExtensions
    {
        public static object GetValue(this IModel Model, string FieldName)
        {
            return Model.GetType().GetProperty(FieldName).GetValue(Model);
        }

        public static Type GetFieldType(this IModel Model, string FieldName)
        {
            return Model.GetType().GetProperty(FieldName).PropertyType;
        }

        public static Type GetUnderlyingFieldType(this IModel Model, string FieldName)
        {
            var type = GetFieldType(Model, FieldName);
            return Nullable.GetUnderlyingType(type) ?? type;
        }

        public static bool IsFieldNullable(this IModel Model, string FieldName)
        {
            var utype = GetUnderlyingFieldType(Model, FieldName);
            return (utype != GetFieldType(Model, FieldName))
                || utype == typeof(string);
        }

        public static List<ModelField> GetFields(this IModel Model)
        {
            return Services.GetFields(Model.GetType().Name);
        }

        public static string GetService(this IModel Model)
        {
            var ca = (ApiServiceAttribute)Attribute.GetCustomAttribute(Model.GetType(), typeof(ApiServiceAttribute));
            if (ca == null || string.IsNullOrWhiteSpace(ca.Name)) return "System";
            else return ca.Name.Trim();
        }

        public static bool HasItem(this IModel Model)
        {
            var ca = (ApiItemAttribute)Attribute.GetCustomAttribute(Model.GetType(), typeof(ApiItemAttribute));
            if (ca == null || string.IsNullOrWhiteSpace(ca.Url)) return false;
            else return true;
        }

        public static bool HasList(this IModel Model)
        {
            var ca = (ApiListAttribute)Attribute.GetCustomAttribute(Model.GetType(), typeof(ApiListAttribute));
            if (ca == null || string.IsNullOrWhiteSpace(ca.Url)) return false;
            else return true;
        }

        public static string GetItemEndPoint(this IModel Model)
        {
            return Model.GetType().Name;
        }

        public static string GetListEndPoint(this IModel Model)
        {
            var ca = (ApiListAttribute)Attribute.GetCustomAttribute(Model.GetType(), typeof(ApiListAttribute));
            if (ca == null || string.IsNullOrWhiteSpace(ca.EndPoint)) return Model.GetType().Name + "s";
            else return ca.EndPoint.Trim();
        }

        public static string GetItemUrl(this IModel Model)
        {
            if (!HasItem(Model)) return null;
            var ca = (ApiItemAttribute)Attribute.GetCustomAttribute(Model.GetType(), typeof(ApiItemAttribute));
            if (ca == null || string.IsNullOrWhiteSpace(ca.Url)) return null;
            else return (Controller.BaseAddress + Controller.ApiPath + ca.Url.Trim());
        }

        public static string GetListUrl(this IModel Model)
        {
            if (!HasItem(Model)) return null;
            var ca = (ApiListAttribute)Attribute.GetCustomAttribute(Model.GetType(), typeof(ApiListAttribute));
            if (ca == null || string.IsNullOrWhiteSpace(ca.Url)) return Model.GetType().Name;
            else return (Controller.BaseAddress + Controller.ApiPath + ca.Url.Trim());
        }

        public static string MakeUrlExample(this IModel Model, string Url, out object[] Arguments)
        {
            var args = new List<Tuple<object, int>>();
            string url = Url;
            var regex = new Regex("{(?<PK>.*?)}", RegexOptions.Compiled);
            foreach (Match pk in regex.Matches(Url))
            {
                string field = pk.Groups["PK"].Value;
                int order;
                object arg = GetFieldExampleValue(Model, field, out order);
                if (arg == null) continue;
                args.Add(new Tuple<object, int>(arg, order));
                url = url.Replace("{" + field + "}", arg.ToString().Trim());
            }
            Arguments = args.OrderBy(a => a.Item2).Select(a => a.Item1).ToArray();
            return url;
        }

        public static object GetFieldExampleValue(this IModel Model, string FieldName, out int Order)
        {
            Order = 1;
            var ca = Model.GetType().GetProperty(FieldName)
                .GetCustomAttributes(typeof(ApiDocumentationExampleAttribute), true)
                .FirstOrDefault() as ApiDocumentationExampleAttribute;
            if (ca == null || ca.Value == null) return null;
            Order = ca.Order;
            return ca.Value.ToString().Trim();
        }

        private static string GetRandomCode(string Value, Type type)
        {
            if (type == typeof(Int32) || type == typeof(Int64))
            {
                string hash = Math.Abs((Value ?? "").GetHashCode()).ToString();
                return hash;
            }
            else if (type == typeof(string))
            {
                string hash = Math.Abs((Value ?? "").GetHashCode()).ToString();
                return Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(hash))
                    .Replace("/", "").Replace("+", "").Replace("=", "").Substring(0, 8).ToUpper();
            }
            else return "{" + Value + "}";
        }

        public static string GetModelDescription(this IModel Model)
        {
            if (!HasItem(Model)) return null;
            var ca = (DescriptionAttribute)Attribute.GetCustomAttribute(Model.GetType(), typeof(DescriptionAttribute));
            if (ca == null || string.IsNullOrWhiteSpace(ca.Description)) return "";
            else return ca.Description.Trim();
        }
    }
}

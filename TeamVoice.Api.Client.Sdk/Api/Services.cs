using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TeamVoice.Api.Attributes;
using TeamVoice.Api.Interfaces;
using TeamVoice.Api.Models;

namespace TeamVoice.Api
{
    public static class Services
    {
        public static Environments Environment = Environments.Production;
        
        public static List<string> GetModelNames()
        {
            var list = new List<string>();
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => (t != typeof(IModel)) && typeof(IModel).IsAssignableFrom(t)).ToList())
            {
                list.Add(type.Name);
            }
            return list;
        }

        public static string GetModelName(Type ModelType)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => (t != typeof(IModel)) && typeof(IModel).IsAssignableFrom(t));
            var type = types.FirstOrDefault(t => t == ModelType);
            if (type == null)
                foreach (var t in types)
                    if (t.GetCustomAttributes(false).Any(a => a is ApiListAttribute && ((ApiListAttribute)a).EndPoint == ModelType.Name))
                        return t.Name;
            if (type == null) throw new InvalidOperationException("Cannot find a "
                    + ModelType.Name + " class inheriting from IModel.");
            return type.Name;
        }

        public static Type GetModelType(string ModelName)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => (t != typeof(IModel)) && typeof(IModel).IsAssignableFrom(t));
            var type = types.FirstOrDefault(t => t.Name == ModelName);
            if (type == null)
                foreach (var t in types)
                    if (t.GetCustomAttributes(false).Any(a => a is ApiListAttribute && ((ApiListAttribute)a).EndPoint == ModelName))
                        return t;
            if (type == null) throw new InvalidOperationException("Cannot find a "
                    + ModelName + " class inheriting from IModel.");
            return type;
        }

        public static List<ModelField> GetFields(string ModelName)
        {
            var list = new List<ModelField>();
            var pis = GetModelType(ModelName).GetProperties();
            foreach (var pi in pis)
            {
                var fkey = GetForeignKey(ModelName, pi.Name);
                var field = new ModelField(ModelName, pi.Name, pi.PropertyType, fkey);
                ApiForeignKeyAttribute fk = pi.GetCustomAttributes(true).FirstOrDefault(a => a is ApiForeignKeyAttribute) as ApiForeignKeyAttribute;
                if (fk != null) field.SetCaption(fk.Model, fk.Key);
                list.Add(field);
            }
            return list;
        }

        private static ModelField GetForeignKey(string ModelName, string FieldName)
        {
            var pi = GetModelType(ModelName).GetProperties().FirstOrDefault(p => p.Name == FieldName);
            if (pi != null)
            {
                ApiForeignKeyAttribute fk = pi.GetCustomAttributes(true).FirstOrDefault(a => a is ApiForeignKeyAttribute) as ApiForeignKeyAttribute;
                if (fk != null) return GetPrimaryKeys(fk.Model).FirstOrDefault(k => k.Name == fk.Key);
            }
            return null;
        }

        public static List<ModelField> GetPrimaryKeys(string ModelName)
        {
            var fields = new SortedDictionary<int, ModelField>();
            var type = GetModelType(ModelName);
            var pis = type.GetProperties();
            foreach (var pi in pis)
            {
                foreach (ApiPrimaryKeyAttribute pk in pi.GetCustomAttributes(true).Where(a => a is ApiPrimaryKeyAttribute))
                {
                    string name = pi.Name;
                    int order = pk.Order;
                    if (fields.Keys.Contains(order))
                        throw new InvalidOperationException("The "
                            + type.Name + " class has more than one property "
                            + "with the [ApiPrimaryKey] attribute set to Order " + order + ".");
                    var fkey = GetForeignKey(ModelName, pi.Name);
                    var field = new ModelField(ModelName, pi.Name, pi.PropertyType, fkey);
                    fields.Add(order, field);
                    foreach (ApiForeignKeyAttribute fk in pi.GetCustomAttributes(true).Where(a => a is ApiForeignKeyAttribute))
                        field.SetCaption(fk.Model, fk.Key);
                }
            }
            return fields.Select(n => n.Value).ToList();
        }
    }
}

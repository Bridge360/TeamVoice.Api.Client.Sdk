using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamVoice.Api
{
    public class ModelField
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public ModelField ForeignKey { get; set; }
        public string ModelName { get; set; }
        private string model;
        private string key;

        public ModelField(string ModelName, string Name, Type Type, ModelField ForeignKey)
        {
            this.ModelName = ModelName;
            this.Name = Name;
            this.Type = Type;
            this.ForeignKey = ForeignKey;
        }

        public bool IsNullable
        {
            get
            {
                return (UnderlyingType != Type) 
                    || UnderlyingType == typeof(string);
            }
        }

        public Type UnderlyingType
        {
            get
            {
                return Nullable.GetUnderlyingType(Type) ?? Type;
            }
        }

        public void SetCaption(string ModelName, string Key)
        {
            model = (ModelName ?? "").Trim();
            key = (Key ?? "").Trim();
        }

        public string Caption
        {
            get
            {
                if (string.IsNullOrEmpty(model) || string.IsNullOrEmpty(key)) return Name;
                if (model + key == Name) return model + "." + key;
                else if (Name.EndsWith(model + key)) return Name.Substring(0, Name.Length - (model + key).Length) + "." + model + "." + key;
                else return model + "." + key;
            }
        }

        public bool HasForeignKey
        {
            get
            {
                return ForeignKey != null;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamVoice.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    internal class ApiForeignKeyAttribute : Attribute
    {
        public string Model { get; private set; }
        public string Key { get; private set; }

        public ApiForeignKeyAttribute(string Model, string Key)
        {
            this.Model = Model;
            this.Key = Key;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamVoice.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    internal class ApiDocumentationExampleAttribute : Attribute
    {
        public object Value { get; private set; }
        public int Order { get; set; }

        public ApiDocumentationExampleAttribute(object Value)
        {
            this.Value = Value;
            Order = 1;
        }
    }
}

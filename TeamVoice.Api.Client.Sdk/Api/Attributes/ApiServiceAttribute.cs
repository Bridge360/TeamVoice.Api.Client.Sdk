using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamVoice.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    internal class ApiServiceAttribute : Attribute
    {
        public string Name { get; set; }

        public ApiServiceAttribute(string Name)
        {
            this.Name = Name;
        }
    }
}

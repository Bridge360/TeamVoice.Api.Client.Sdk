using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamVoice.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    internal class ApiItemAttribute : Attribute
    {
        public string Url { get; set; }

        public ApiItemAttribute(string Url)
        {
            this.Url = Url;
        }
    }
}

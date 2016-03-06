using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamVoice.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    internal class ApiListAttribute : Attribute
    {
        public string EndPoint { get; set; }
        public string Url { get; set; }

        public ApiListAttribute(string EndPoint, string Url)
        {
            this.EndPoint = EndPoint;
            this.Url = Url;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamVoice.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    internal class ApiPrimaryKeyAttribute : Attribute
    {
        public int Order { get; private set; }
        
        public ApiPrimaryKeyAttribute()
        {
            Order = 1;
        }

        public ApiPrimaryKeyAttribute(int Order)
        {
            this.Order = Order;
        }
    }
}

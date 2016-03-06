using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamVoice.Api.Interfaces;

namespace TeamVoice.Api
{
    public class Results<T> where T : IModel, new()
    {
        public List<T> Values { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }

        public Results()
        {
            Values = new List<T>();
            Success = true;
            Error = "";
        }
    }
}

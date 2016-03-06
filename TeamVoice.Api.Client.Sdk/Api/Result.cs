using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamVoice.Api.Interfaces;

namespace TeamVoice.Api
{
    public class Result<T> where T : IModel, new()
    {
        public T Value { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }

        public Result()
        {
            Success = true;
            Error = "";
        }
    }
}

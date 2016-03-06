using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamVoice.Api.Attributes;
using TeamVoice.Api.Interfaces;

namespace TeamVoice.Api.Models
{
    [ApiService("Member")]
    [ApiItem("Members/Award/{ID}")]
    [ApiList("Awards", "Members/Awards")]
    [Description("Awards are designed to promote engagement within TeamVoice. Members gain award Achievements while using TeamVoice.")]
    public class Award : IModel
    {
        [ApiPrimaryKey]
        [ApiDocumentationExample(9)]
        public int ID { get; set; }
    }
}

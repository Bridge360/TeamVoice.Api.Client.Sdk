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
    [ApiItem("Member/Role/{ID}")]
    [ApiList("Roles", "Member/Roles")]
    [Description("Roles determine the activities a Member can do in TeamVoice, e.g. user, team leader, administrator.")]
    public class Role : IModel
    {
        [ApiPrimaryKey]
        [ApiDocumentationExample(32)]
        public int ID { get; set; }
        public string Description { get; set; }
        public string Usage { get; set; }
    }
}

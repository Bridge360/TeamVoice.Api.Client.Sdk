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
    [ApiItem("Member/Status/{ID}")]
    [ApiList("MemberStatuses", "Member/Statuses")]
    [Description("Members exist within TeamVoice in a variety of states, e.g. active, obsolete, invited, pending invite, invite declined, etc.")]
    public class MemberStatus : IModel
    {
        [ApiPrimaryKey]
        [ApiDocumentationExample(2)]
        public int ID { get; set; }
        public string Description { get; set; }
    }
}

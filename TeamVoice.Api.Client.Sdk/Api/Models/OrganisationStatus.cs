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
    [ApiService("Organisation")]
    [ApiItem("Organisation/Status/{ID}")]
    [ApiList("OrganisationStatuses", "Organisation/Statuses")]
    [Description("Organisations exist within TeamVoice in a variety of states, e.g. unconfirmed, active, suspended, deleted, cancelled, etc.")]
    public class OrganisationStatus : IModel
    {
        [ApiPrimaryKey]
        [ApiDocumentationExample(2)]
        public int ID { get; set; }
        public string Description { get; set; }
    }
}

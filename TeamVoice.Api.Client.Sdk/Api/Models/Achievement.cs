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
    [ApiItem("Member/{MemberID}/Achievement/{AwardID}")]
    [ApiList("Achievements", "Member/Achievements")]
    [Description("Members achieve a variety of awards while using TeamVoice. Awards are designed to promote engagement with TeamVoice itself.")]
    public class Achievement : IModel
    {
        [ApiPrimaryKey(1)]
        [ApiForeignKey("Award", "ID")]
        [ApiDocumentationExample(1, Order = 1)]
        public int AwardID { get; set; }
        [ApiPrimaryKey(2)]
        [ApiForeignKey("Member", "ID")]
        [ApiDocumentationExample(38, Order = 2)]
        public int MemberID { get; set; }
        public DateTime AwardedOn { get; set; }
        [ApiForeignKey("Organisation", "ID")]
        public int OrganisationID { get; set; }
    }
}

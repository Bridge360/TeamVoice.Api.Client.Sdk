using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TeamVoice.Api.Attributes;
using TeamVoice.Api.Interfaces;

namespace TeamVoice.Api.Models
{
    [ApiService("Team")]
    [ApiItem("Team/{ID}")]
    [ApiList("Teams", "Teams")]
    [Description("Teams are functional groupings of Members with a team leader (who may or not also "
        + "be a member of the team). Since Members can be added or removed from Teams on an ongoing "
        + "basis, they add flexibility when specifying QuestionSet answerers and report recipients.")]
    public class Team : IModel
    {
        [ApiPrimaryKey]
        [ApiDocumentationExample(45)]
        public int ID { get; set; }
        public string Description { get; set; }
        [ApiForeignKey("Member", "ID")]
        public int TeamLeaderID { get; set; }
        [ApiForeignKey("Organisation", "ID")]
        public int OrganisationID { get; set; }
    }
}

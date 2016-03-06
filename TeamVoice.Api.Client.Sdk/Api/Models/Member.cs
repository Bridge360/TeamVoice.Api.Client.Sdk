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
    [ApiItem("Member/{ID}")]
    [ApiList("Members", "Members")]
    [Description("Members are TeamVoice users, created during registration or by invitation. "
        + "Because TeamVoice is a member-centric application, they are both the mechanism "
        + "you use to log in, and the people who ask and answer questions.")]
    public class Member : IModel
    {
        [ApiPrimaryKey]
        [Description("Primary key.")]
        [ApiDocumentationExample(38)]
        public int ID { get; set; }
        [Description("The email address used to sign up or be invited with.")]
        public string Email { get; set; }
        [Description("The style the member is known by (e.g. Mr, Ms, etc).")]
        public string Title { get; set; }
        [Description("The member's first name.")]
        public string Forename { get; set; }
        [Description("The member's last name.")]
        public string Surname { get; set; }
        [Description("An optional name used to informally address the member within TeamVoice.")]
        public string Nickname { get; set; }
        [Description("The title, surname and last name.")]
        public string FullName { get; set; }
        [Description("The surname and last name.")]
        public string FullNameWithoutTitle { get; set; }
        [ApiForeignKey("MemberStatus", "ID")]
        [Description("Whether the member is active, obsolete, invited, etc.")]
        public int MemberStatusID { get; set; }
        [ApiForeignKey("Role", "ID")]
        [Description("The role determines the activities a member can do in TeamVoice.")]
        public int RoleID { get; set; }
        [ApiForeignKey("QuestionSet", "ID")]
        [Description("The member may optionally have been invited to respond to a particular question set.")]
        public int? InitialQuestionSetID { get; set; }
        [ApiForeignKey("Organisation", "ID")]
        [Description("The organisation this member belongs to. Some members such as consultants can view "  
            + "other organisations, but they always belong to a single organisation of their own.")]
        public int OrganisationID { get; set; }
    }
}

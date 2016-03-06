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
    [ApiService("QuestionSet")]
    [ApiItem("QuestionSet/{ID}")]
    [ApiList("QuestionSets", "QuestionSets")]
    [Description("QuestionSets are collections of Questions, configured to be asked "
        + "and answered periocally to a group of Members within an Organisation.")]
    public class QuestionSet : IModel
    {
        [ApiPrimaryKey]
        [ApiDocumentationExample(7)]
        public int ID { get; set; }
    }
}

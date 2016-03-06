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
    [ApiItem("Organisation/{ID}")]
    [ApiList("Organisations", "Organisations")]
    [Description("Organisations are the companies and business entities in TeamVoice. "
        + "The engagement process centers around Members asking and answering questions "
        + "for other Members within an Organisation.")]
    public class Organisation : IModel
    {
        [ApiPrimaryKey]
        [ApiDocumentationExample(1)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Domain { get; set; }
        [ApiForeignKey("OrganisationStatus", "ID")]
        public int OrganisationStatusID { get; set; }
        public DateTime SignupDate { get; set; }
        public DateTime? NextInvoiceDate { get; set; }
        public DateTime? PreviousInvoiceDate { get; set; }
        public string TwitterUser { get; set; }
        public bool ExemptFromBilling { get; set; }
        public int UserCount { get; set; }
        [ApiForeignKey("Organisation", "ID")]
        public int? ConsultantOrganisationID { get; set; }
    }
}

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
    [ApiItem("Organisation/Address/{ID}")]
    [ApiList("Addresses", "Organisation/Addresses")]
    [Description("Organisations have a series of contact addresses associated with them, of different Address Types.")]
    public class Address : IModel
    {
        [ApiPrimaryKey]
        [ApiDocumentationExample(46)]
        public int ID { get; set; }
        [ApiForeignKey("AddressType", "ID")]
        public int AddressTypeID { get; set; }
        public string ContactName { get; set; }
        public string OrganisationName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Usage { get; set; }
        [ApiForeignKey("Organisation", "ID")]
        public int OrganisationID { get; set; }
        [ApiForeignKey("Member", "ID")]
        public int MemberID { get; set; }
    }
}

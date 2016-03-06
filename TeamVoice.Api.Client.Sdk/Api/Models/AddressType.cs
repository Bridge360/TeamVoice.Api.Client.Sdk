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
    [ApiItem("Organisation/Address/Type/{ID}")]
    [ApiList("AddressTypes", "Organisation/Address/Types")]
    [Description("Addresses can be of different types, e.g. invoice address, mailing address, etc.")]
    public class AddressType : IModel
    {
        [ApiPrimaryKey]
        [ApiDocumentationExample(1)]
        public int ID { get; set; }
        public string Description { get; set; }
    }
}

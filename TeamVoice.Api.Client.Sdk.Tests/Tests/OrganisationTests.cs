using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeamVoice.Api.Models;
using TeamVoice.Api.Client.Sdk.Tests.Classes;

namespace TeamVoice.Api.Client.Sdk.Tests
{
    public partial class API
    {
        [TestMethod, TestCategory("API")]
        public void ApiGetOrganisations()
        {
            GeneralThreadAffineContext.Run(async () =>
            {
                var controller = new Controller<Organisation>(Helpers.Credentials);
                var result = await controller.GetListAsync();
                Assert.AreEqual("", result.Error);
                Assert.IsTrue(result.Success);
                Assert.AreEqual("Bridge³", result.Values[0].DisplayName);
            });
        }

        [TestMethod, TestCategory("API")]
        public void ApiGetOrganisation()
        {
            GeneralThreadAffineContext.Run(async () =>
            {
                var controller = new Controller<Organisation>(Helpers.Credentials);
                var result = await controller.GetItemAsync(1);
                Assert.AreEqual("", result.Error);
                Assert.IsTrue(result.Success);
                Assert.AreEqual("Bridge³", result.Value.DisplayName);
            });
        }

        [TestMethod, TestCategory("API")]
        public void ApiGetOrganisationStatuses()
        {
            GeneralThreadAffineContext.Run(async () =>
            {
                var controller = new Controller<OrganisationStatus>(Helpers.Credentials);
                var result = await controller.GetListAsync();
                Assert.AreEqual("", result.Error);
                Assert.IsTrue(result.Success);
                Assert.AreEqual("Unconfirmed", result.Values[0].Description);
            });
        }

        [TestMethod, TestCategory("API")]
        public void ApiGetOrganisationStatus()
        {
            GeneralThreadAffineContext.Run(async () =>
            {
                var controller = new Controller<OrganisationStatus>(Helpers.Credentials);
                var result = await controller.GetItemAsync(3);
                Assert.AreEqual("", result.Error);
                Assert.IsTrue(result.Success);
                Assert.AreEqual("Deleted", result.Value.Description);
            });
        }

        [TestMethod, TestCategory("API")]
        public void ApiGetOrganisationAddresses()
        {
            GeneralThreadAffineContext.Run(async () =>
            {
                var controller = new Controller<Address>(Helpers.Credentials);
                var result = await controller.GetListAsync();
                Assert.AreEqual("", result.Error);
                Assert.IsTrue(result.Success);
                Assert.AreEqual("1 Parabola Avenue", result.Values[0].Address1);
            });
        }

        [TestMethod, TestCategory("API")]
        public void ApiGetOrganisationAddress()
        {
            GeneralThreadAffineContext.Run(async () =>
            {
                var controller = new Controller<Address>(Helpers.Credentials);
                var result = await controller.GetItemAsync(46);
                Assert.AreEqual("", result.Error);
                Assert.IsTrue(result.Success);
                Assert.AreEqual("Hereford", result.Value.City);
            });
        }

        [TestMethod, TestCategory("API")]
        public void ApiGetOrganisationAddressTypes()
        {
            GeneralThreadAffineContext.Run(async () =>
            {
                var controller = new Controller<AddressType>(Helpers.Credentials);
                var result = await controller.GetListAsync();
                Assert.AreEqual("", result.Error);
                Assert.IsTrue(result.Success);
                Assert.AreEqual("Invoice Address", result.Values[0].Description);
            });
        }

        [TestMethod, TestCategory("API")]
        public void ApiGetOrganisationAddressType()
        {
            GeneralThreadAffineContext.Run(async () =>
            {
                var controller = new Controller<AddressType>(Helpers.Credentials);
                var result = await controller.GetItemAsync(1);
                Assert.AreEqual("", result.Error);
                Assert.IsTrue(result.Success);
                Assert.AreEqual("Mailing Address", result.Value.Description);
            });
        }
    }
}
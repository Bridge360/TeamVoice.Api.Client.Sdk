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
        public void ApiGetMembers()
        {
            GeneralThreadAffineContext.Run(async () =>
            {
                var controller = new Controller<Member>(Helpers.Credentials);
                var result = await controller.GetListAsync();
                Assert.AreEqual("", result.Error);
                Assert.IsTrue(result.Success);
                Assert.AreEqual(145, result.Values[0].ID);
            });
        }

        [TestMethod, TestCategory("API")]
        public void ApiGetMember()
        {
            GeneralThreadAffineContext.Run(async () =>
            {
                var controller = new Controller<Member>(Helpers.Credentials);
                var result = await controller.GetItemAsync(38);
                Assert.AreEqual("", result.Error);
                Assert.IsTrue(result.Success);
                Assert.AreEqual("robin.guest@bridge3.co.uk", result.Value.Email);
            });
        }

        [TestMethod, TestCategory("API")]
        public void ApiGetMemberStatuses()
        {
            GeneralThreadAffineContext.Run(async () =>
            {
                var controller = new Controller<MemberStatus>(Helpers.Credentials);
                var result = await controller.GetListAsync();
                Assert.AreEqual("", result.Error);
                Assert.IsTrue(result.Success);
                Assert.AreEqual("Active", result.Values[0].Description);
            });
        }

        [TestMethod, TestCategory("API")]
        public void ApiGetMemberStatus()
        {
            GeneralThreadAffineContext.Run(async () =>
            {
                var controller = new Controller<MemberStatus>(Helpers.Credentials);
                var result = await controller.GetItemAsync(4);
                Assert.AreEqual("", result.Error);
                Assert.IsTrue(result.Success);
                Assert.AreEqual("Pending Invite", result.Value.Description);
            });
        }

        [TestMethod, TestCategory("API")]
        public void ApiGetMemberRoles()
        {
            GeneralThreadAffineContext.Run(async () =>
            {
                var controller = new Controller<Role>(Helpers.Credentials);
                var result = await controller.GetListAsync();
                Assert.AreEqual("", result.Error);
                Assert.IsTrue(result.Success);
                Assert.AreEqual("User", result.Values[0].Description);
            });
        }

        [TestMethod, TestCategory("API")]
        public void ApiGetMemberRole()
        {
            GeneralThreadAffineContext.Run(async () =>
            {
                var controller = new Controller<Role>(Helpers.Credentials);
                var result = await controller.GetItemAsync(8);
                Assert.AreEqual("", result.Error);
                Assert.IsTrue(result.Success);
                Assert.AreEqual("Manager", result.Value.Description);
            });
        }
    }
}

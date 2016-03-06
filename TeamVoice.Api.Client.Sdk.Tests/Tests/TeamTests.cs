using System;
using TeamVoice.Api.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TeamVoice.Api.Client.Sdk.Tests.Classes;

namespace TeamVoice.Api.Client.Sdk.Tests
{
    public partial class API
    {
        [TestMethod, TestCategory("Teams")]
        public void ApiGetTeams()
        {
            GeneralThreadAffineContext.Run(async () =>
            {
                var controller = new Controller<Team>(Helpers.Credentials);
                var result = await controller.GetListAsync();
                Assert.AreEqual("", result.Error);
                Assert.IsTrue(result.Success);
                Assert.AreEqual("Directors", result.Values[0].Description);
            });            
        }

        [TestMethod, TestCategory("API")]
        public void ApiGetTeam()
        {
            GeneralThreadAffineContext.Run(async () =>
            {
                var controller = new Controller<Team>(Helpers.Credentials);
                var result = await controller.GetItemAsync(45);
                Assert.AreEqual("", result.Error);
                Assert.IsTrue(result.Success);
                Assert.AreEqual("Directors", result.Value.Description);
            });
        }
    }
}

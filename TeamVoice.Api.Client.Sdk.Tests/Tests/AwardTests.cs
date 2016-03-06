using System;
using TeamVoice.Api.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TeamVoice.Api.Client.Sdk.Tests.Classes;

namespace TeamVoice.Api.Client.Sdk.Tests
{
    public partial class API
    {
      
        [TestMethod, TestCategory("API")]
        public void ApiGetAchievements()
        {
            GeneralThreadAffineContext.Run(async () =>
            {
                var controller = new Controller<Achievement>(Helpers.Credentials);
                var result = await controller.GetListAsync();
                Assert.AreEqual("", result.Error);
                Assert.IsTrue(result.Success);
                Assert.AreEqual(1, result.Values[0].AwardID);
            });
        }

        [TestMethod, TestCategory("API")]
        public void ApiGetAchievement()
        {
            GeneralThreadAffineContext.Run(async () =>
            {
                var controller = new Controller<Achievement>(Helpers.Credentials);
                var result = await controller.GetItemAsync(3, 53);
                Assert.AreEqual("", result.Error);
                Assert.IsTrue(result.Success);
                Assert.AreEqual(3, result.Value.AwardID);
            });
        }
    }
}

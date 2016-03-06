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
        public void ControllerAccountKey()
        {
            var val = Helpers.Credentials.GetAccountKey("TEAMVOICE");
            Assert.AreEqual(Helpers.ACCOUNTKEY, val);
        }

        [TestMethod, TestCategory("API")]
        public void ControllerAppKey()
        {
            var cred = Helpers.Credentials;
            var val = Helpers.Credentials.GetAppKey("TEAMVOICE");
            Assert.AreEqual(Helpers.APPKEY, val);
        }

        [TestMethod, TestCategory("API")]
        public void ControllerModelType()
        {
            var controller = new Controller<Team>(Helpers.Credentials);
            var type = controller.ModelType;
            Assert.AreEqual(typeof(Team), type);
        }

        [TestMethod, TestCategory("API")]
        public void ControllerItemEndPoint()
        {
            string endpoint = new Controller<Team>(Helpers.Credentials).ItemUrl;
            Assert.AreEqual(Controller.BaseAddress + "api/v1/Team/{ID}", endpoint);
        }

        [TestMethod, TestCategory("API")]
        public void ControllerListEndPoint()
        {
            string endpoint = new Controller<Team>(Helpers.Credentials).ListUrl;
            Assert.AreEqual(Controller.BaseAddress + "api/v1/Teams", endpoint);
        }
    }
}

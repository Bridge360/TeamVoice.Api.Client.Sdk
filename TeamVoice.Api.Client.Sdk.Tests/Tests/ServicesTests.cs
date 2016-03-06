using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeamVoice.Api.Interfaces;
using TeamVoice.Api.Models;
using TeamVoice.Api.Client.Sdk.Tests.Classes;

namespace TeamVoice.Api.Client.Sdk.Tests
{
    [TestClass]
    public partial class API
    {
        [TestMethod, TestCategory("API")]
        public void ServicesGetModelNames()
        {
            var models = TeamVoice.Api.Services.GetModelNames();
            Assert.IsTrue(models.Count > 0, "models.Count > 0");
            int team = models.Count(m => m == "Team");
            Assert.AreEqual(1, team, "team");
        }

        [TestMethod, TestCategory("API")]
        public void ServicesGetModelName()
        {
            string name = TeamVoice.Api.Services.GetModelName(typeof(Team));
            Assert.AreEqual("Team", name);
        }

        [TestMethod, TestCategory("API")]
        public void ServicesGetModelType()
        {
            var type = TeamVoice.Api.Services.GetModelType("Team");
            Assert.AreEqual(typeof(Team), type);
        }

        [TestMethod, TestCategory("API")]
        public void ServicesGetFields()
        {
            var fields = TeamVoice.Api.Services.GetFields("Team");
            Assert.AreEqual(4, fields.Count, "fields.Count");
            Assert.AreEqual("ID", fields[0].Name, "fields[0].Name");
            Assert.AreEqual(typeof(int), fields[0].Type, "fields[0].Type");
            Assert.AreEqual(typeof(int), fields[0].UnderlyingType, "fields[0].UnderlyingType");
            Assert.AreEqual(false, fields[0].IsNullable, "fields[0].IsNullable");
            Assert.AreEqual("Description", fields[1].Name, "fields[1].Name");
            Assert.AreEqual(typeof(string), fields[1].Type, "fields[1].Type");
            Assert.AreEqual(typeof(string), fields[1].UnderlyingType, "fields[1].UnderlyingType");
            Assert.AreEqual(true, fields[1].IsNullable, "fields[1].UnderlyingType");
        }

        [TestMethod, TestCategory("API")]
        public void ServicesGetPrimaryKeys()
        {
            var keys = TeamVoice.Api.Services.GetPrimaryKeys("Team");
            Assert.AreEqual(1, keys.Count, "keys.Count");
            Assert.AreEqual("ID", keys[0].Name, "fields[0].Name");
            Assert.AreEqual(typeof(int), keys[0].Type, "fields[0].Type");
            Assert.AreEqual(typeof(int), keys[0].UnderlyingType, "fields[0].UnderlyingType");
        }

        [TestMethod, TestCategory("API")]
        public void ServicesFillSpreadsheet()
        {
            string file;
            using (var xlsx = Helpers.CreateSpreadsheet())
            {
                file = xlsx.File.FullName;
                foreach (string table in Services.GetModelNames())
                    Helpers.CreateWorksheet(xlsx, table);
                Debug.WriteLine("Saving " + file);
                xlsx.Save();
            }
            #if OPEN_SPREADSHEET
            Debug.WriteLine("Opening " + file);
            Helpers.ShellExecute(IntPtr.Zero, "open", file, "", "", Helpers.ShowCommands.SW_NORMAL);
            #endif
        }
    }
}

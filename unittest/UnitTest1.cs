using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace unittest
{
    public class UnitTest1
    {
        [Fact]
        public void ReadFromString()
        {
            var json = @"{'Name':'DRE','Members':[{'Name':'Senthil','AssignedRole':{'Name':'Dev'}},{'Name':'viraj','AssignedRole':{'Name':'ProductOwner'}},{'Name':'suku','AssignedRole':{'Name':'Dev'}}],'SupportedSystems':['FPP','QPP'], 'IsActive' : true}";
            var project = JToken.Parse(json);
            var members = project["Members"];

            Assert.IsType<JObject>(project);
            Assert.IsType<JArray>(members);
            Assert.IsType<JValue>(members[0]["Name"]); //reading from Array

            //Print all properies
            var lst = new List<string>();
            foreach (var prop in project.Children<JProperty>())
            {
                if (prop.Value.GetType() == typeof(JValue))
                    lst.Add(prop.Name);
            }

            Assert.Equal(new List<string> { "Name", "IsActive" }, lst);

        }

        [Fact]
        public void QueryWithLinq()
        {
            var json = @"{'Name':'DRE','Members':[{'Name':'Senthil','AssignedRole':{'Name':'Dev'}},{'Name':'viraj','AssignedRole':{'Name':'ProductOwner'}},{'Name':'suku','AssignedRole':{'Name':'Dev'}}],'SupportedSystems':['FPP','QPP'], 'IsActive' : true}";
            var project = JToken.Parse(json);
            var members = project["Members"];

            //find the dev names
            var result = project["Members"].Where(m =>
            {
                return m["AssignedRole"]["Name"].ToString() == "Dev";
            });


            Assert.Equal(2, result.Count());

            Assert.Equal("Senthil", (string)project.SelectToken("Members[0].Name"));
            Assert.Equal("Dev", (string) project.SelectToken("Members[0].AssignedRole.Name"));
        }


        [Fact]
        public void SerializeFromObject()
        {
            Project p = new Project("DRE");
            var dev = new Role("Dev");
            var po = new Role("ProductOwner");
            p.Members.Add(new Member("Senthil", dev));
            p.Members.Add(new Member("viraj", po));
            p.Members.Add(new Member("suku", dev));

            p.SupportedSystems.Add("FPP");
            p.SupportedSystems.Add("QPP");

            var json = JsonConvert.SerializeObject(p);
            var p1 = JsonConvert.DeserializeObject<Project>(json);

            Assert.Equal(p.Name, p1.Name);

        }
    }
}

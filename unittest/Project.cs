using System.Collections.Generic;

namespace unittest
{
    public class Project
    {
        public string Name {get; set;}

        public List<Member> Members {get; set; } = new List<Member>();

        public List<string> SupportedSystems {get; set;} = new List<string>();

        public Project(string name)
        {
            this.Name = name;
        }
        
    }

    public class Role
    {
          public string Name {get; set;}

          public Role (string name)
          {
                this.Name = name;
          }
    }

    public class Member
    {
        public string Name {get; set;}
        public Role AssignedRole {get; set; } 

        public Member(string name, Role role)
        {
            this.Name = name;
            this.AssignedRole = role;
        }


    }
}
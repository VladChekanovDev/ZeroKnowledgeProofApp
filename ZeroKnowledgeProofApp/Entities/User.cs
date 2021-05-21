using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ZeroKnowledgeProofApp.Entities
{
    class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string N { get; set; }
        public string V0 { get; set; }

        public User()
        {

        }

        public User(string firstName, string lastName, string login, string n, string v0)
        {
            FirstName = firstName;
            LastName = lastName;
            Login = login;
            N = n;
            V0 = v0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ZeroKnowledgeProofApp.Entities
{
    class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public BigInteger N { get; set; }
        public BigInteger V { get; set; }

        public User()
        {

        }
    }
}

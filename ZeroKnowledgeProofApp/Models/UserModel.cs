using System;
using System.Collections.Generic;
using System.Text;
using ZeroKnowledgeProofApp.Entities;
using ZeroKnowledgeProofApp.Other;

namespace ZeroKnowledgeProofApp.Models
{
    class UserModel
    {
        public void Add(User user)
        {
            using (var db = new ZeroKnowledgeProofDbContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool IsUserExists(string login)
        {
            using (var db = new ZeroKnowledgeProofDbContext())
            {
                return db.Users.FirstOrDefault(u => u.Login == login) != null;
            }
        }

        public User FindUser(string login)
        {
            using (var db = new ZeroKnowledgeProofDbContext())
            {
                return db.Users.FirstOrDefault(u => u.Login == login);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using ZeroKnowledgeProofApp.Entities;

namespace ZeroKnowledgeProofApp.Other
{
    static class CurrentUserInfo
    {
        public static User CurrentUser { get; set; }
        public static BigInteger S;
        public static bool IsAuthenticated = true;
    }
}

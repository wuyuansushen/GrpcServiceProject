using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrpcServiceProject.Models;

namespace GrpcServiceProject.Data
{
    public static class DbInitialize
    {
        public static void Initialize(AuthContext context)
        {
            var rootUser = new User() {
                ID = 1,
                userName="root",
                passwd="1234567890"
            };

            context.Users.Add(rootUser);
            context.SaveChanges();
        }
    }
}

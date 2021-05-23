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

            var normalUser = new User()
            {
                ID = 2,
                userName = "normal",
                passwd = "0987654321"
            };

            var testUser = new User()
            {
                ID = 3,
                userName = "test",
                passwd = "test1234567890"
            };

            var anonymousUser = new User()
            {
                ID = 4,
                userName = "anonymous",
                passwd="0987654321"
            };

            context.Users.Add(rootUser);
            context.Users.Add(normalUser);
            context.Users.Add(testUser);
            context.Users.Add(anonymousUser);
            context.SaveChanges();
        }
    }
}

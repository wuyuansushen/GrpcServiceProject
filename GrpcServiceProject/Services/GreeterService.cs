using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace GrpcServiceProject
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly IDbContextFactory<Models.AuthContext> _contextFactory;
        public GreeterService(ILogger<GreeterService> logger,IDbContextFactory<Models.AuthContext> contextFactory)
        {
            _logger = logger;
            _contextFactory = contextFactory;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            var httpContext = context.GetHttpContext();
            var XFF = (((httpContext.Request.Headers)["X-Forwarded-For"]).ToString().Split(@","))[0];

            string secret;
            using (var dbcontext = _contextFactory.CreateDbContext())
            {
                //var firUsr = dbcontext.Users.FindAsync(1);
                _logger.LogInformation($"{XFF}");
                //var secretResul = firUsr.GetAwaiter().GetResult();
                //secret = secretResul.passwd;

                var linqout =
                    from item in dbcontext.Users
                    where item.ID == 1
                    select item;
                secret= (linqout.FirstOrDefault()).passwd;
                
            }
            //_logger.LogInformation($"{XFF}");
            return Task.FromResult(new HelloReply()
            {
                Message = $"{XFF}\nDb: {secret}"
            });
        }

    }
}

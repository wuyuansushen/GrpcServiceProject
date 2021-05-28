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
    public class GetIPService : IPService.IPServiceBase
    {
        private readonly ILogger<GetIPService> _logger;
        private readonly IDbContextFactory<Models.AuthContext> _contextFactory;
        public GetIPService(ILogger<GetIPService> logger,IDbContextFactory<Models.AuthContext> contextFactory)
        {
            _logger = logger;
            _contextFactory = contextFactory;
        }

        public override Task<IPReflection> GetIP(UserID request, ServerCallContext context)
        {
            var httpContext = context.GetHttpContext();
            var XFF = (((httpContext.Request.Headers)["X-Forwarded-For"]).ToString().Split(@","))[0];


            #region EF
            string secret ="";
            using (var dbcontext = _contextFactory.CreateDbContext())
            {
                //var firUsr = dbcontext.Users.FindAsync(1);
                _logger.LogInformation($"{XFF}");
                //var secretResul = firUsr.GetAwaiter().GetResult();
                //secret = secretResul.passwd;

                /*var linqSource =
                    from item in dbcontext.Users
                    select item;
                var linqout =
                   from groupItem in linqSource.AsEnumerable()
                   group groupItem by groupItem.passwd;
                */

                var linqout =
                    from item in dbcontext.Users.AsEnumerable()
                    group item by item.passwd into itemOut
                    orderby itemOut.Key ascending
                    select itemOut;
                
                foreach(var iGroup in linqout)
                {
                    secret += $"{iGroup.Key}\n";
                    foreach(var i in iGroup)
                    {
                        secret += $"{i.ID} {i.userName} {i.passwd}\n";
                        //secret += $"{i}\n";
                    }
                    secret += $"\n";
                    
                }

               
            }
            #endregion
            //_logger.LogInformation($"{XFF}");
            return Task.FromResult(new IPReflection()
            {
                IP = $"{XFF}\n"
            });
        }

    }
}

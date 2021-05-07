using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServiceProject
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            var httpContext = context.GetHttpContext();
            var XFF = (((httpContext.Request.Headers)["X-Forwarded-For"]).ToString().Split(@","))[0];

            _logger.LogInformation($"{XFF}");
            return Task.FromResult(new HelloReply()
            {
                Message = $"{XFF}"
            });
        }
    }
}

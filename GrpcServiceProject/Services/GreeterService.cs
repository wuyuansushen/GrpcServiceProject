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
            Random r1 = new Random();
            var ranNum = r1.Next(1, 10000);
            _logger.LogInformation((new EventId(6000,"Random")),$"Random is {ranNum} {DateTime.Now}");

            return Task.FromResult(new HelloReply()
            {
                Message = ranNum+ "\nblank-command-line\n" + request.Name
            });
        }
    }
}

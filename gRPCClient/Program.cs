using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using Grpc.Net.Client;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The port number(5001) must match the port of the gRPC server.
            var i= GetIP("https://grpc.fiveelementgod.xyz:443",1);
            await i;
        }

        static async Task GetIP(string urlIn, int id)
        {
            using var channel = GrpcChannel.ForAddress(urlIn);
            var client = new IPService.IPServiceClient(channel);
            var reply = await client.GetIPAsync(new UserID { ID = id }) ;
            Console.WriteLine("IP: "+reply.IP);
        }
    }
}
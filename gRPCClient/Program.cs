using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The port number(5001) must match the port of the gRPC server.
            //using var channel = GrpcChannel.ForAddress("https://grpc.fiveelementgod.xyz:443");
            //using var channel = GrpcChannel.ForAddress("https://127.0.0.1:5001");
            using var channel = GrpcChannel.ForAddress("http://127.0.0.1:6000");
            var client =  new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                              new HelloRequest { Name = "Gayhub" });
            Console.WriteLine("IP: "+reply.Message);
        }
    }
}
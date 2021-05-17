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
            var i= Greet("https://grpc.fiveelementgod.xyz:443");
            while(!i.IsCompleted)
            {
                Console.WriteLine("...");
                Thread.Sleep(1000);
            }
            await i;
        }

        static async Task Greet(string urlIn)
        {
            await Greet(urlIn, "GayHub");
        }
        static async Task Greet(string urlIn, string requestName)
        {
            using var channel = GrpcChannel.ForAddress(urlIn);
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(new HelloRequest { Name = requestName });
            Console.WriteLine("IP: " + reply.Message);
        }
    }
}
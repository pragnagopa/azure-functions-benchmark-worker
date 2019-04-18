using CommandLine;
using Grpc.Core;
using Microsoft.Azure.WebJobs.Script.Grpc.Messages;
using System;
using System.Threading.Tasks;

namespace Microsoft.Azure.Functions.BenchmarkWorker
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkerArguments arguments = null;
            Parser.Default.ParseArguments<WorkerArguments>(args)
                .WithParsed(ops => arguments = ops)
                .WithNotParsed(err => Environment.Exit(1));
            Channel channel = new Channel(arguments.Host, arguments.Port, ChannelCredentials.Insecure);
            var client = new FunctionRpcClient(new FunctionRpc.FunctionRpcClient(channel), arguments.WorkerId);
            Task rpcStream = client.RpcStream();
            Console.WriteLine("Hello World!");
        }
    }
}

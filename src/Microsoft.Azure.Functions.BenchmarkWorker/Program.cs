using CommandLine;
using Grpc.Core;
using Microsoft.Azure.WebJobs.Script.Grpc.Messages;
using System;
using System.Threading.Tasks;

namespace Microsoft.Azure.Functions.BenchmarkWorker
{
    class Program
    {
        public async static Task Main(string[] args)
        {
            WorkerArguments arguments = null;
            Parser.Default.ParseArguments<WorkerArguments>(args)
                .WithParsed(ops => arguments = ops)
                .WithNotParsed(err => Environment.Exit(1));
            Channel channel = new Channel(arguments.Host, arguments.Port, ChannelCredentials.Insecure);
            var client = new FunctionRpcClient(new FunctionRpc.FunctionRpcClient(channel), arguments.WorkerId);
            var t = client.RpcStream();
            var readerTask = client.RpcStreamReader();
            await Task.WhenAll(t, readerTask);
            Console.WriteLine("Hello World!");
        }
    }
}

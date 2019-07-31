using System;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.Json;
using AmazonAPIGatewayProxyFunction = Amazon.Lambda.AspNetCoreServer.APIGatewayProxyFunction;

namespace Neo.Amazon.Lambda.CustomRuntime
{
    public abstract class APIGatewayProxyFunction : AmazonAPIGatewayProxyFunction, IRunableFunction
    {
        public async Task Run()
        {
            using (var handlerWrapper = HandlerWrapper.GetHandlerWrapper(FunctionHandler(this), new JsonSerializer()))
            {
                using (var bootstrap = new LambdaBootstrap(handlerWrapper))
                {
                    await bootstrap.RunAsync();
                }
            }
        }

        private static Func<APIGatewayProxyRequest, ILambdaContext, Task<APIGatewayProxyResponse>> FunctionHandler(APIGatewayProxyFunction lambda) => 
            lambda.FunctionHandlerAsync;
    }
}
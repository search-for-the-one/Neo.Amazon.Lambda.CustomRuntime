using System;
using System.Threading.Tasks;
using Amazon.Lambda.ApplicationLoadBalancerEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.Json;
using AmazonApplicationLoadBalancerFunction = Amazon.Lambda.AspNetCoreServer.ApplicationLoadBalancerFunction;

namespace Neo.Amazon.Lambda.CustomRuntime
{
    public abstract class ApplicationLoadBalancerFunction : AmazonApplicationLoadBalancerFunction, IRunableFunction
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

        private static Func<ApplicationLoadBalancerRequest, ILambdaContext, Task<ApplicationLoadBalancerResponse>> FunctionHandler(ApplicationLoadBalancerFunction self) => 
            self.FunctionHandlerAsync;
    }
}
using System;

namespace Neo.Amazon.Lambda.CustomRuntime
{
    public static class LambdaRuntime
    {
        public static bool IsRunning => !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("AWS_LAMBDA_RUNTIME_API"));
    }
}
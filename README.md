﻿# Neo.Amazon.Lambda.CustomRuntime

This library allows you to easily create an AWS lambda with custom runtime with .net core.

## Usage

Your `LambdaEntryPoint` class should derive from `Neo.Amazon.Lambda.CustomRuntime.ApplicationLoadBalancerFunction` if you're using ALB, or `Neo.Amazon.Lambda.CustomRuntime.APIGatewayProxyFunction` if you're using API Gateway for your lambda.

e.g.
```
    public class LambdaEntryPoint : Neo.Amazon.Lambda.CustomRuntime.ApplicationLoadBalancerFunction
    {
        // ...
    }
```

Then change your `LocalEntryPoint` class as follows.

```
    /// <summary>
    /// The Main function can be used to run the ASP.NET Core application locally using the Kestrel webserver.
    /// </summary>
    public class LocalEntryPoint
    {
        public static async Task Main(string[] args)
        {
            if (LambdaRuntime.IsRunning)
                await new LambdaEntryPoint().Run();
            else
                await BuildWebHost(args).RunAsync();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
```

## Bootstrap File

Additionally, you will need to create a file called `bootstrap` in your dotnet project, and under File Properties, change Copy to Output Directory to `Copy if newer`.

`boostrap` file (file name is case sensitive and replace `{executable name of your project}`):
```
#!/bin/sh
# This is the script that the Lambda host calls to start the custom runtime.

/var/task/{executable name of your project}
```

In your `aws-lambda-tools-defaults.json` file, you'll need to add the following lines.

```
    "function-handler"     : "not_required",
    "msbuild-parameters"   : "--self-contained true",
```

After publishing your lambda, you'll need to change the Runtime settings of your lambda to `Custom Lambda`.

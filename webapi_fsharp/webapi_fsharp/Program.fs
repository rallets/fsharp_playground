namespace webapi_fsharp

open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging

open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Hosting
open DataContext

module Program =
    let exitCode = 0

    let CreateHostBuilder args =
        Host
            .CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(fun webBuilder -> webBuilder.UseStartup<Startup>() |> ignore)

    [<EntryPoint>]
    let main args =
        let host = CreateHostBuilder(args).Build()
        use scope = host.Services.CreateScope()
        let services = scope.ServiceProvider

        let context =
            services.GetRequiredService<ItemsRepository>()

        Initialize(context)

        host.Run()

        exitCode

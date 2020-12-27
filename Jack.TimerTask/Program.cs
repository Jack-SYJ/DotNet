using Autofac;
using Autofac.Extensions.DependencyInjection;
using Jack.TimerTask.Container;
using Jack.TimerTask.Extentions;
using Jack.TimerTask.Jobs;
using Jack.TimerTask.JobScheduler;
using Jack.TimerTask.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;

namespace Jack.TimerTask
{
    internal class ServiceContainerFactory : IServiceProviderFactory<ContainerBuilder>
    {
        private readonly ContainerBuilder containerBuilder;

        public ServiceContainerFactory(ContainerBuilder containerBuilder)
        {
            this.containerBuilder = containerBuilder;
        }
        public ContainerBuilder CreateBuilder(IServiceCollection services)
        {
            containerBuilder.Populate(services);
            return containerBuilder;
        }

        public IServiceProvider CreateServiceProvider(ContainerBuilder containerBuilder)
        {
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
          
            var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (!new List<string>() { "dev", "test", "sbx", "qas", "live" }.Contains(enviroment))
            {
                throw new ArgumentException("环境变量 ASPNETCORE_ENVIRONMENT 必须是 dev/test/sbx/qas/live");
            }
            Environment.SetEnvironmentVariable("Environment", enviroment);
            CreateHostBuilder(args).UseEnvironment(enviroment).Build().Run();
           
        }

        static readonly ContainerBuilder containerBuilder = new ContainerBuilder();

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            return new HostBuilder()
                 .ConfigureHostConfiguration(configHost =>
                 {
                     configHost.SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFileEx("Config/appsettings.json", optional: true)
                            .AddJsonFileEx("Config/scheduler.json", optional: true)
                            .AddEnvironmentVariables()
                          .AddCommandLine(args);
                 })
                 .ConfigureServices((hostContext, services) =>
                 { 
                     services.AddHttpContextAccessor()
                         .AddAutofac()
                         .AddHttpUserCenterService()
                   
                         .Configure<JobConfig>(hostContext.Configuration.GetSection("CornJobScheduler"))
                         .AddHostedService<CornJobSchedulerHostedService>()
                         .AddSingleton<ICornJobScheduler, CornJobScheduler>()
                         .AddHttpClient()
                         .Configure<AppSettingConfiguration>(hostContext.Configuration.GetSection("AppSettings"));
                     
                 })
                  .ConfigureLogging(logging =>
                  {
                      logging.ClearProviders();
                      logging.AddLog4Net("Config/log4net.xml", true);

#if DEBUG
                      logging.AddConsole();
#endif
                  })
                  .UseServiceProviderFactory<ContainerBuilder>(new ServiceContainerFactory(containerBuilder))
                  .ConfigureContainer<ContainerBuilder>((container) =>
                  {
                      container.RegisterModule();
                  })
                 .UseConsoleLifetime();
        }

       
    }
}


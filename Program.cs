using System;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using AspectCore.Extensions.DependencyInjection;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using System.Threading;
using Microsoft.Extensions.Primitives;

namespace C_
{
    class Program
    {

        static void Main(string[] args)
        {
            MemoryCache cache = new MemoryCache(new MemoryCacheOptions()
            {
                //SizeLimit = 100
            });

            CancellationTokenSource tokenSource = new CancellationTokenSource();

            var option = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(10)
            };

            option.RegisterPostEvictionCallback((key, value, reason, obj) =>
            {
                Console.WriteLine(reason);
            });

            option.AddExpirationToken(new CancellationChangeToken(tokenSource.Token));

            cache.Set("Key", "Value", option);

            //while (true)
            //{
            //    cache.Get("Key");

            //}

            tokenSource.CancelAfter(2000);

            Console.Read();

            //for (int i = 0; i < 1000; i++)
            //{
            //    cache.Set<string>(i.ToString(), i.ToString(), new MemoryCacheEntryOptions()
            //    {
            //        Size = 1
            //        //Size = i.ToString().Length
            //    });
            //    Console.WriteLine(cache.Count);
            //}

            //cache.Compact(0.2);
            //Console.WriteLine(cache.Count);
        }

    }

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        ServiceCollection services = new ServiceCollection();

    //        services.ConfigureDynamicProxy();

    //        services.AddTransient<IMysql, Mysql>();

    //        var provider = services.BuildDynamicProxyProvider();

    //        var mysql = provider.GetService<IMysql>();

    //        var msg = mysql.GetData(10);
    //        Console.WriteLine(msg);

    //        msg = mysql.GetData(10);
    //        Console.WriteLine(msg);
    //        Console.Read();

    //    }



    //}

    //public class MyLogInterceptorAttribute : AbstractInterceptorAttribute
    //{
    //    public override Task Invoke(AspectContext context, AspectDelegate next)
    //    {
    //        Console.WriteLine("Begin to write the log...");
    //        var task = next(context);
    //        Console.WriteLine("End to write the log...");
    //        return task;
    //    }
    //}

    //public class MyCacheInterceptorAttribute : AbstractInterceptorAttribute
    //{
    //    private Dictionary<string, string> cacheDic = new Dictionary<string, string>();

    //    public override Task Invoke(AspectContext context, AspectDelegate next)
    //    {
    //        var cacheKey = string.Join(",", context.Parameters);

    //        if (cacheDic.ContainsKey(cacheKey))
    //        {
    //            context.ReturnValue = cacheDic[cacheKey].ToString();

    //            return Task.CompletedTask;
    //        }

    //        var task = next(context);

    //        var cacheValue = context.ReturnValue.ToString();

    //        cacheDic.Add(cacheKey, "From cache:" + cacheValue);

    //        return task;
    //    }
    //}

    //public interface IMysql
    //{
    //    string GetData(int id);
    //}

    //public class Mysql : IMysql
    //{
    //    //[MyLogInterceptor]
    //    [MyCacheInterceptor]
    //    public string GetData(int id)
    //    {
    //        string msg = $"already got the data which id={id}";
    //        //Console.WriteLine(msg);
    //        return msg;
    //    }
    //}






    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        ServiceCollection services = new ServiceCollection();

    //        services.AddScoped<IFly, Pig>();
    //        services.AddLogging(c => c.AddConsole(opt => opt.LogToStandardErrorThreshold = LogLevel.Debug));

    //        var serviceProvider = services.BuildServiceProvider();
    //        var scope1 = serviceProvider.CreateScope();
    //        var fly1 = scope1.ServiceProvider.GetService<IFly>();

    //        var scope2 = serviceProvider.CreateScope();
    //        var fly2 = scope2.ServiceProvider.GetService<IFly>();


    //        // var fly = serviceProvider.GetService<IFly>();


    //        // fly.Fly();
    //    }
    //}

    //public interface IFly
    //{
    //    void Fly();
    //}

    //public class Pig : IFly
    //{
    //    ILogger<Pig> logger;

    //    public Pig(ILoggerFactory loggerFactory)
    //    {
    //        logger = loggerFactory.CreateLogger<Pig>();
    //        Console.WriteLine("pig can fly when there is a wind....");
    //    }

    //    public void Fly()
    //    {
    //        logger.LogDebug("logger info: logger get!!");
    //        Console.WriteLine("pig can fly when there is a wind....");
    //    }
    //}
}

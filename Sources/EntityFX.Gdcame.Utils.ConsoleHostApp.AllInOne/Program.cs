﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http;
using System.Web.Http.Cors;
using EntityFX.Gdcame.Application.WebApi.Providers;
using EntityFX.Gdcame.Presentation.Web.Api.Providers;
using EntityFX.Gdcame.Utils.Common;
using EntityFX.Gdcame.Utils.ConsoleHostApp.AllInOneCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Microsoft.Owin.Builder;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Owin.Security.AesDataProtectorProvider;
using EntityFX.Gdcame.Utils.ConsoleHostApp.Starter;
using Unity.WebApi;


namespace EntityFX.Gdcame.Utils.ConsoleHostApp.AllInOne
{

    internal class Program
    {
        private static void Main()
        {
            CoreStartup.AppConfiguration = new AppConfiguration()
            {
                MongoConnectionString = ConfigurationManager.AppSettings["MongoConnectionString"],
                RepositoryProvider = ConfigurationManager.AppSettings["RepositoryProvider"],
                WebApiPort = int.Parse(ConfigurationManager.AppSettings["WebApiPort"]),
                SignalRPort = int.Parse(ConfigurationManager.AppSettings["WebApiPort"]),
            };

            var webApiStartOptions = new StartOptions
            {
                Port = CoreStartup.AppConfiguration.WebApiPort,
            };



            if (RuntimeHelper.IsRunningOnMono())
            {
                //webApiStartOptions.ServerFactory = "Nowin";
                webApiStartOptions.Urls.Add(string.Format("http://+:{0}", webApiStartOptions.Port));
                if (Environment.OSVersion.Platform != PlatformID.Unix)
                {
                    webApiStartOptions.Urls.Add(string.Format("http://127.0.0.1:{0}", webApiStartOptions.Port));
                }
            } else
            {
                webApiStartOptions.Urls.Add(string.Format("http://+:{0}", webApiStartOptions.Port));
            }

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<CoreStartup>()
                .UseUrls(webApiStartOptions.Urls.ToArray())
                .Build();

            host.Run();
                

            // Start OWIN host 
            //var httpWebApi = WebApp.Start<WebApiStartup>(webApiStartOptions);

            Console.WriteLine(RuntimeHelper.GetRuntimeInfo());
            Console.WriteLine("SignalR server running on {0}", CoreStartup.AppConfiguration.WebApiPort);
            Console.WriteLine("Web server running on {0}", webApiStartOptions.Port);
            Console.WriteLine("Repository provider: {0}", CoreStartup.AppConfiguration.RepositoryProvider);
            Console.ReadLine();
        }
    }
}
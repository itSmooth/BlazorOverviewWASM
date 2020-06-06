using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BlazorOverviewWASM.Shared.Services;
using BlazorOverviewWASM.Client.Services;
using System.Net.Http.Headers;

namespace BlazorOverviewWASM.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            // 進行DI 容器註冊
            //builder.Services.AddScoped<IMyNoteService, MyNoteService>();
            //使用IHttpClientFactory 來註冊IMyNoteService 服務
            builder.Services.AddHttpClient<IMyNoteService, MyNoteWebAPIService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:5001/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
            await builder.Build().RunAsync();
        }
    }
}

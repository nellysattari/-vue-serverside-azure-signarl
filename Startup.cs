// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Azure.SignalR.Samples.ChatRoom
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder =>
            {
                builder.AllowAnyMethod()
                
                       .AllowAnyHeader()
                         .WithOrigins("https://clientside.z8.web.core.windows.net", "http://localhost:8080")
                       .AllowCredentials();

            }));


            services.AddSignalR().AddAzureSignalR("Endpoint=https://biddingsignalrservice.service.signalr.net;AccessKey=aECKrYf1lgbhGFjs5=;Version=1.0;");
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
            app.UseCors("CorsPolicy");
            app.UseAzureSignalR(routes =>
            {
                routes.MapHub<Chat>("/chat");
            });
        }
    }
}

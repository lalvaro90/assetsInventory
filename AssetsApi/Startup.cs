using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetsApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AssetsApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IServiceCollection _services { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _services = services.AddDbContext<AssetsContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("Allow",
                builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            services.AddRouting(r => r.SuppressCheckForUnhandledSecurityMetadata = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<AssetsContext>();
            context.Database.EnsureCreated();
            context.Database.Migrate();

            app.Use(async delegate (HttpContext httpContext, Func<Task> next)
            {
                var headers = httpContext.Request.Headers;
                if (headers.Keys.Contains("token"))
                {
                    var tokenValue = headers["token"].FirstOrDefault();
                    Token tk = context.Tokens.FirstOrDefault(t => t.Content == tokenValue);
                    if (tk != null)
                    {

                        if (tk.Expire > DateTime.Now)
                        {
                            await next.Invoke();
                        }
                        else
                        {
                            httpContext.Response.StatusCode = 401;
                            await httpContext.Response.WriteAsync("Unautorized Request");
                        }
                    }
                    else if (httpContext.Request.Path.Value.Contains("users") && httpContext.Request.Method == HttpMethods.Post)
                    {
                        await next.Invoke();
                    }
                    else
                    {
                        httpContext.Response.StatusCode = 401;
                        await httpContext.Response.WriteAsync("Unautorized Request");
                    }

                }
                else if (httpContext.Request.Path.Value.Contains("users") && httpContext.Request.Method == HttpMethods.Post)
                {
                    await next.Invoke();
                }
                else
                {
                    httpContext.Response.StatusCode = 401;
                    await httpContext.Response.WriteAsync("Unautorized Request");
                }
                //await next.Invoke();
            });

            app.Use((context, next) =>
            {
                context.Items["__CorsMiddlewareInvoked"] = true;
                return next();
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
